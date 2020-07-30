using Exiled.API.Enums;
using Exiled.API.Extensions;
using Exiled.API.Features;
using Exiled.Events.EventArgs;
using Grenades;
using Hints;
using MEC;
using Mirror;
using Respawning;
using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Better079
{
    public class PluginEvents
    {
        private Better079Plugin plugin;
        internal static float a2cooldown = 0f;

        public PluginEvents(Better079Plugin better079Plugin)
        {
            this.plugin = better079Plugin;
        }

        internal void RoundStart()
        {
            a2cooldown = 0f;
        }

        internal void PlayerSpawn(SpawningEventArgs ev)
        {
            if (ev.Player.Role == RoleType.Scp079)
            {
                ev.Player.ReferenceHub.hints.Show(new TextHint(Better079Plugin.instance.Config.b079_spawn_msg, new HintParameter[] { new StringHintParameter("") }, new HintEffect[]
                {
                    HintEffectPresets.TrailingPulseAlpha(0.5f, 1f, 0.5f, 2f, 0f, 3)
                }, 10f));
                //ev.Player.Broadcast(10, plugin.SpawnMsg, false);
            }
        }

        public static List<Camera079> GetSCPCameras()
        {
            var list = GameObject.FindObjectsOfType<Camera079>();
            List<Camera079> cams = new List<Camera079>();
            foreach (var ply in PlayerManager.players)
            {
                if (ply.GetComponent<ReferenceHub>().characterClassManager.CurRole.team == Team.SCP && ply.GetComponent<ReferenceHub>().characterClassManager.NetworkCurClass != RoleType.Scp079)
                {
                    foreach (var cam in list)
                    {
                        if (Vector3.Distance(cam.transform.position, ply.transform.position) <= Better079Plugin.instance.Config.b079_a1_dist)
                        {
                            cams.Add(cam);
                        }
                    }
                }
            }
            return cams;
        }

        // https://github.com/galaxy119/EXILED/blob/master/EXILED_Main/Extensions/Player.cs
        public static Room SCP079Room(ReferenceHub player)
        {
            Vector3 playerPos = player.scp079PlayerScript.currentCamera.transform.position;
            Vector3 end = playerPos - new Vector3(0f, 10f, 0f);
            bool flag = Physics.Linecast(playerPos, end, out RaycastHit raycastHit, -84058629);

            if (!flag || raycastHit.transform == null)
                return null;

            Transform transform = raycastHit.transform;

            while (transform.parent != null && transform.parent.parent != null)
                transform = transform.parent;

            foreach (Room room in Map.Rooms)
                if (room.Position == transform.position)
                    return room;

            return new Room(transform.name, transform, transform.position);
        }

        /*internal void ConsoleCmd(ConsoleCommandEvent ev)
        {
            if (ev.Player.GetRole() == RoleType.Scp079)
            {
                string[] args = ev.Command.Split(' ');
                if (args[0].Equals(plugin.CommandPrefix))
                {
                    if (args.Length == 2)
                    {
                        if (args[1].ToLower().Equals("help") || args[1].ToLower().Equals("commands") || args[1].ToLower().Equals("?"))
                        {
                            ev.ReturnMessage = plugin.HelpMsgTitle + "\n" +
                                "\"." + plugin.CommandPrefix + " a1\" - " + plugin.HelpMsgA1 + " - " + plugin.A1Power + "+ AP - Tier " + (plugin.A1Tier + 1) + "+\n" +
                                "\"." + plugin.CommandPrefix + " a2\" - " + plugin.HelpMsgA2 + " - " + plugin.A2Power + "+ AP - Tier " + (plugin.A2Tier + 1) + "+\n" +
                                "\"." + plugin.CommandPrefix + " a3\" - " + plugin.HelpMsgA3 + " - " + plugin.A3Power + "+ AP - Tier " + (plugin.A3Tier + 1) + "+\n" +
                                "\"." + plugin.CommandPrefix + " a4\" - " + plugin.HelpMsgA4 + " - " + plugin.A4Power + "+ AP - Tier " + (plugin.A4Tier + 1) + "+\n";
                            return;
                        }

                        if (args[1].ToLower().Equals("a1"))
                        {
                            if (ev.Player.scp079PlayerScript.NetworkcurLvl < plugin.A1Tier)
                            {
                                ev.ReturnMessage = plugin.TierRequiredMsg.Replace("$tier", "" + (plugin.A1Tier + 1));
                                return;
                            }
                            if (ev.Player.scp079PlayerScript.NetworkcurMana >= plugin.A1Power)
                            {
                                ev.Player.scp079PlayerScript.NetworkcurMana -= plugin.A1Power;
                            }
                            else
                            {
                                ev.ReturnMessage = plugin.NoPowerMsg;
                                return;
                            }
                            var cams = GetSCPCameras();
                            if (cams.Count > 0)
                            {
                                Camera079 cam = cams[UnityEngine.Random.Range(0, cams.Count)];
                                ev.Player.scp079PlayerScript.CmdSwitchCamera(cam.cameraId, false);
                                ev.ReturnMessage = plugin.RunA1Msg;
                                return;
                            }
                            else
                            {
                                ev.ReturnMessage = plugin.FailA1Msg;
                                ev.Player.scp079PlayerScript.NetworkcurMana += plugin.A1Power;
                                return;
                            }
                        }

                        if (args[1].ToLower().Equals("a2"))
                        {
                            if (ev.Player.scp079PlayerScript.NetworkcurLvl < plugin.A2Tier)
                            {
                                ev.ReturnMessage = plugin.TierRequiredMsg.Replace("$tier", "" + (plugin.A2Tier + 1));
                                return;
                            }
                            if (ev.Player.scp079PlayerScript.NetworkcurMana >= plugin.A2Power)
                            {
                                ev.Player.scp079PlayerScript.NetworkcurMana -= plugin.A2Power;
                            }
                            else
                            {
                                ev.ReturnMessage = plugin.NoPowerMsg;
                                return;
                            }
                            if (Time.timeSinceLevelLoad - a2cooldown < plugin.A2Cooldown)
                            {
                                ev.Player.scp079PlayerScript.NetworkcurMana += plugin.A2Power;
                                ev.ReturnMessage = plugin.FailA2Msg;
                                return;
                            }
                            Room room = SCP079Room(ev.Player);
                            if (room == null)
                            {
                                ev.Player.scp079PlayerScript.NetworkcurMana += plugin.A2Power;
                                ev.ReturnMessage = plugin.FailA2Msg;
                                return;
                            }
                            if (room.Zone == ZoneType.Surface)
                            {
                                ev.Player.scp079PlayerScript.NetworkcurMana += plugin.A2Power;
                                ev.ReturnMessage = plugin.FailA2Msg;
                                return;
                            }
                            foreach (var item in plugin.A2BlacklistRooms)
                            {
                                if (room.Name.ToLower().Contains(item.ToLower()))
                                {
                                    ev.Player.scp079PlayerScript.NetworkcurMana += plugin.A2Power;
                                    ev.ReturnMessage = plugin.FailA2Msg;
                                    return;
                                }
                            }
                            Timing.RunCoroutine(GasRoom(room, ev.Player));
                            ev.ReturnMessage = plugin.RunA2Msg;
                            return;
                        }

                        if (args[1].ToLower().Equals("a3"))
                        {
                            if (ev.Player.scp079PlayerScript.NetworkcurLvl < plugin.A3Tier)
                            {
                                ev.ReturnMessage = plugin.TierRequiredMsg.Replace("$tier", "" + (plugin.A3Tier + 1));
                                return;
                            }
                            if (ev.Player.scp079PlayerScript.NetworkcurMana >= plugin.A3Power)
                            {
                                ev.Player.scp079PlayerScript.NetworkcurMana -= plugin.A3Power;
                            }
                            else
                            {
                                ev.ReturnMessage = plugin.NoPowerMsg;
                                return;
                            }
                            Generator079.Generators[0].RpcCustomOverchargeForOurBeautifulModCreators(plugin.A3Timer, false);
                            ev.ReturnMessage = plugin.RunA3Msg;
                            return;
                        }

                        if (args[1].ToLower().Equals("a4"))
                        {
                            if (ev.Player.scp079PlayerScript.NetworkcurLvl < plugin.A4Tier)
                            {
                                ev.ReturnMessage = plugin.TierRequiredMsg.Replace("$tier", "" + (plugin.A4Tier + 1));
                                return;
                            }
                            if (ev.Player.scp079PlayerScript.NetworkcurMana >= plugin.A4Power)
                            {
                                ev.Player.scp079PlayerScript.NetworkcurMana -= plugin.A4Power;
                            }
                            else
                            {
                                ev.ReturnMessage = plugin.NoPowerMsg;
                                return;
                            }
                            var pos = ev.Player.scp079PlayerScript.currentCamera.transform.position;
                            GrenadeManager gm = ev.Player.GetComponent<GrenadeManager>();
                            GrenadeSettings settings = gm.availableGrenades.FirstOrDefault(g => g.inventoryID == ItemType.GrenadeFlash);
                            FlashGrenade flash = GameObject.Instantiate(settings.grenadeInstance).GetComponent<FlashGrenade>();
                            flash.fuseDuration = 0.5f;
                            flash.InitData(gm, Vector3.zero, Vector3.zero, 1f);
                            flash.transform.position = pos;
                            NetworkServer.Spawn(flash.gameObject);
                            ev.ReturnMessage = plugin.RunA4Msg;
                            return;
                        }
                        ev.ReturnMessage = plugin.HelpMsg.Replace("$prefix", "" + plugin.CommandPrefix);
                        return;
                    }
                    ev.ReturnMessage = plugin.HelpMsg.Replace("$prefix", "" + plugin.CommandPrefix);
                    return;
                }
            }
        }*/

        public static IEnumerator<float> GasRoom(Room room, ReferenceHub scp)
        {
            a2cooldown = Time.timeSinceLevelLoad;
            if (!Better079Plugin.instance.Config.b079_a2_disable_cassie)
            {
                string str = ".g4 ";
                for (int i = Better079Plugin.instance.Config.b079_a2_timer; i > 0f; i--)
                {
                    str += ". .g4 ";
                }
                RespawnEffectsController.PlayCassieAnnouncement(str, false, false);
            }
            List<Door> doors = Map.Doors.Where((d) => Vector3.Distance(d.transform.position, room.Position) <= 20f).ToList();
            foreach (var item in doors)
            {
                item.NetworkisOpen = true;
                item.Networklocked = true;
            }
            for (int i = Better079Plugin.instance.Config.b079_a2_timer; i > 0f; i--)
            {
                foreach (var ply in PlayerManager.players)
                {
                    var player = new Player(ply);
                    if (player.Team != Team.SCP && player.CurrentRoom != null && player.CurrentRoom.Transform == room.Transform)
                    {
                        player.ReferenceHub.hints.Show(new TextHint(Better079Plugin.instance.Config.b079_msg_a2_warn.Replace("$seconds", "" + i), new HintParameter[] { new StringHintParameter("") }, new HintEffect[]
                        {
                            HintEffectPresets.TrailingPulseAlpha(0.5f, 1f, 0.5f, 2f, 0f, 3)
                        }, 1f));
                        //player.ClearBroadcasts();
                        //player.Broadcast(1, plugin.A2WarnMsg.Replace("$seconds", "" + i), true);
                        //PlayerManager.localPlayer.GetComponent<MTFRespawn>().RpcPlayCustomAnnouncement(".g3", false, false);
                    }
                }
                yield return Timing.WaitForSeconds(1f);
            }
            foreach (var item in doors)
            {
                item.NetworkisOpen = false;
                item.Networklocked = true;
            }
            foreach (var ply in PlayerManager.players)
            {
                var player = new Player(ply);
                if (player.Team != Team.SCP && player.CurrentRoom != null && player.CurrentRoom.Transform == room.Transform)
                {
                    player.ReferenceHub.hints.Show(new TextHint(Better079Plugin.instance.Config.b079_msg_a2_active, new HintParameter[] { new StringHintParameter("") }, new HintEffect[]
                    {
                        HintEffectPresets.TrailingPulseAlpha(0.5f, 1f, 0.5f, 2f, 0f, 3)
                    }, 5f));
                }
            }
            for (int i = 0; i < Better079Plugin.instance.Config.b079_a2_gas_timer * 2; i++)
            {
                foreach (var ply in PlayerManager.players)
                {
                    var player = new Player(ply);
                    if (player.Team != Team.SCP && player.Role != RoleType.Spectator && player.CurrentRoom != null && player.CurrentRoom.Transform == room.Transform)
                    {
                        player.ReferenceHub.playerStats.HurtPlayer(new PlayerStats.HitInfo(10f, "WORLD", DamageTypes.Decont, 0), player.ReferenceHub.gameObject);
                        if (player.Role == RoleType.Spectator)
                        {
                            scp.scp079PlayerScript.AddExperience(Better079Plugin.instance.Config.b079_a2_exp);
                        }
                    }
                }
                yield return Timing.WaitForSeconds(0.5f);
            }
            foreach (var item in doors)
            {
                item.Networklocked = false;
            }
        }
    }
}