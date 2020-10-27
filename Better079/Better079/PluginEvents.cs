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
using System.Runtime.InteropServices.WindowsRuntime;
using UnityEngine;

namespace Better079 {
    public class PluginEvents {
        private Better079Plugin plugin;
        internal static float a2cooldown = 0f;
        internal static float a4cooldown = 0f;

        public PluginEvents(Better079Plugin better079Plugin) {
            this.plugin = better079Plugin;
        }

        internal void RoundStart() {
            a2cooldown = 0f;
            a4cooldown = 0f;
        }

        internal void PlayerSpawn(SpawningEventArgs ev) {
            if(ev.Player.Role == RoleType.Scp079) {
                ev.Player.ReferenceHub.hints.Show(new TextHint(Better079Plugin.instance.Config.b079_spawn_msg, new HintParameter[] { new StringHintParameter("") }, new HintEffect[]
                {
                    HintEffectPresets.TrailingPulseAlpha(0.5f, 1f, 0.5f, 2f, 0f, 3)
                }, 10f));
         
            }
        }

        public static List<Camera079> GetSCPCameras() {
            var list = GameObject.FindObjectsOfType<Camera079>();
            List<Camera079> cams = new List<Camera079>();
            foreach(var ply in PlayerManager.players) {
                if(ply.GetComponent<ReferenceHub>().characterClassManager.CurRole.team == Team.SCP && ply.GetComponent<ReferenceHub>().characterClassManager.NetworkCurClass != RoleType.Scp079) {
                    foreach(var cam in list) {
                        if(Vector3.Distance(cam.transform.position, ply.transform.position) <= Better079Plugin.instance.Config.b079_a1_dist) {
                            cams.Add(cam);
                        }
                    }
                }
            }
            return cams;
        }

        // https://github.com/galaxy119/EXILED/blob/master/EXILED_Main/Extensions/Player.cs
        

        public void ConsoleCmd(SendingConsoleCommandEventArgs ev) {
            if(ev.Player.Role.Equals(RoleType.Scp079)) {

                if(ev.Name.Equals(plugin.Config.b079_prefix)) {
                    ev.Allow = false;
                    if(ev.Arguments.IsEmpty()) {
                        ev.ReturnMessage = plugin.Config.b079_help_title + "\n" +
                            "\"." + plugin.Config.b079_prefix + " a1\" - " + plugin.Config.b079_help_a1 + " - " + plugin.Config.b079_a1_power + "+ AP - Tier " + (plugin.Config.b079_a1_tier + 1) + "+\n" +
                            "\"." + plugin.Config.b079_prefix + " a2\" - " + plugin.Config.b079_help_a2 + " - " + plugin.Config.b079_a2_power + "+ AP - Tier " + (plugin.Config.b079_a2_tier + 1) + "+\n" +
                            "\"." + plugin.Config.b079_prefix + " a3\" - " + plugin.Config.b079_help_a3 + " - " + plugin.Config.b079_a3_power + "+ AP - Tier " + (plugin.Config.b079_a3_tier + 1) + "+\n" +
                            "\"." + plugin.Config.b079_prefix + " a4\" - " + plugin.Config.b079_help_a4 + " - " + plugin.Config.b079_a4_power + "+ AP - Tier " + (plugin.Config.b079_a4_tier + 1) + "+\n";
                        return;
                    }
                    if(ev.Arguments.Count >= 1) {
                        if(ev.Arguments[0].ToLower().Equals("help") || ev.Arguments[0].ToLower().Equals("commands") || ev.Arguments[0].ToLower().Equals("?")) {
                            ev.ReturnMessage = plugin.Config.b079_help_title + "\n" +
                                "\"." + plugin.Config.b079_prefix + " a1\" - " + plugin.Config.b079_help_a1 + " - " + plugin.Config.b079_a1_power + "+ AP - Tier " + (plugin.Config.b079_a1_tier + 1) + "+\n" +
                                "\"." + plugin.Config.b079_prefix + " a2\" - " + plugin.Config.b079_help_a2 + " - " + plugin.Config.b079_a2_power + "+ AP - Tier " + (plugin.Config.b079_a2_tier + 1) + "+\n" +
                                "\"." + plugin.Config.b079_prefix + " a3\" - " + plugin.Config.b079_help_a3 + " - " + plugin.Config.b079_a3_power + "+ AP - Tier " + (plugin.Config.b079_a3_tier + 1) + "+\n" +
                                "\"." + plugin.Config.b079_prefix + " a4\" - " + plugin.Config.b079_help_a4 + " - " + plugin.Config.b079_a4_power + "+ AP - Tier " + (plugin.Config.b079_a4_tier + 1) + "+\n";
                            return;
                        }

                        if(ev.Arguments[0].ToLower().Equals("a1")) {
                            if(ev.Player.ReferenceHub.scp079PlayerScript.NetworkcurLvl < plugin.Config.b079_a1_tier) {
                                ev.ReturnMessage = plugin.Config.b079_msg_tier_required.Replace("$tier", "" + (plugin.Config.b079_a1_tier + 1));
                                return;
                            }
                            if(ev.Player.ReferenceHub.scp079PlayerScript.NetworkcurMana >= plugin.Config.b079_a1_power) {
                                ev.Player.ReferenceHub.scp079PlayerScript.NetworkcurMana -= plugin.Config.b079_a1_power;
                            } else {
                                ev.ReturnMessage = plugin.Config.b079_msg_no_power;
                                return;
                            }
                            var cams = GetSCPCameras();
                            if(cams.Count > 0) {
                                Camera079 cam = cams[UnityEngine.Random.Range(0, cams.Count)];
                                ev.Player.ReferenceHub.scp079PlayerScript.CmdSwitchCamera(cam.cameraId, false);
                                ev.ReturnMessage = plugin.Config.b079_msg_a1_run;
                                return;
                            } else {
                                ev.ReturnMessage = plugin.Config.b079_msg_a1_fail;
                                ev.Player.ReferenceHub.scp079PlayerScript.NetworkcurMana += plugin.Config.b079_a1_power;
                                return;
                            }
                        }

                        if(ev.Arguments[0].ToLower().Equals("a2")) {
                            if(ev.Player.ReferenceHub.scp079PlayerScript.NetworkcurLvl < plugin.Config.b079_a2_tier) {
                                ev.ReturnMessage = plugin.Config.b079_msg_tier_required.Replace("$tier", "" + (plugin.Config.b079_a2_tier + 1));
                                return;
                            }
                            if(ev.Player.ReferenceHub.scp079PlayerScript.NetworkcurMana >= plugin.Config.b079_a2_power) {
                                ev.Player.ReferenceHub.scp079PlayerScript.NetworkcurMana -= plugin.Config.b079_a2_power;
                            } else {
                                ev.ReturnMessage = plugin.Config.b079_msg_no_power;
                                return;
                            }
                            if(Time.timeSinceLevelLoad - a2cooldown < plugin.Config.b079_a2_cooldown) {
                                ev.Player.ReferenceHub.scp079PlayerScript.NetworkcurMana += plugin.Config.b079_a2_power;
                                ev.ReturnMessage = plugin.Config.b079_msg_a2_fail;
                                return;
                            }
                            Room room = ev.Player.CurrentRoom;
                            if(room == null) {
                                ev.Player.ReferenceHub.scp079PlayerScript.NetworkcurMana += plugin.Config.b079_a2_power;
                                ev.ReturnMessage = plugin.Config.b079_msg_a2_fail;
                                return;
                            }
                            if(room.Zone == ZoneType.Surface) {
                                ev.Player.ReferenceHub.scp079PlayerScript.NetworkcurMana += plugin.Config.b079_a2_power;
                                ev.ReturnMessage = plugin.Config.b079_msg_a2_fail;
                                return;
                            }
                            foreach(var item in plugin.Config.b079_a2_blacklisted_rooms) {
                                if(room.Name.ToLower().Contains(item.ToLower())) {
                                    ev.Player.ReferenceHub.scp079PlayerScript.NetworkcurMana += plugin.Config.b079_a2_power;
                                    ev.ReturnMessage = plugin.Config.b079_msg_a2_fail;
                                    return;
                                }
                            }
                            Timing.RunCoroutine(GasRoom(room, ev.Player.ReferenceHub));
                            ev.ReturnMessage = plugin.Config.b079_msg_a2_run;
                            return;
                        }

                        if(ev.Arguments[0].ToLower().Equals("a3")) {
                            if(ev.Player.ReferenceHub.scp079PlayerScript.NetworkcurLvl < plugin.Config.b079_a3_tier) {
                                ev.ReturnMessage = plugin.Config.b079_msg_tier_required.Replace("$tier", "" + (plugin.Config.b079_a3_tier + 1));
                                return;
                            }
                            if(ev.Player.ReferenceHub.scp079PlayerScript.NetworkcurMana >= plugin.Config.b079_a3_power) {
                                ev.Player.ReferenceHub.scp079PlayerScript.NetworkcurMana -= plugin.Config.b079_a3_power;
                            } else {
                                ev.ReturnMessage = plugin.Config.b079_msg_no_power;
                                return;
                            }
                            Generator079.Generators[0].ServerOvercharge(plugin.Config.b079_a3_timer, false);
                            ev.ReturnMessage = plugin.Config.b079_msg_a3_run;
                            return;
                        }

                        if(ev.Arguments[0].ToLower().Equals("a4")) {
                            if(ev.Player.ReferenceHub.scp079PlayerScript.NetworkcurLvl < plugin.Config.b079_a4_tier) {
                                ev.ReturnMessage = plugin.Config.b079_msg_tier_required.Replace("$tier", "" + (plugin.Config.b079_a4_tier + 1));
                                return;
                            }
                            if(a4cooldown >= Time.time) {
                                ev.ReturnMessage = $"<color=red>This ability is in CoolDown ({(int) (a4cooldown - Time.time)})</color>";
                                return;
                            }
                            if(ev.Player.ReferenceHub.scp079PlayerScript.NetworkcurMana >= plugin.Config.b079_a4_power) {
                                ev.Player.ReferenceHub.scp079PlayerScript.NetworkcurMana -= plugin.Config.b079_a4_power;
                            } else {
                                ev.ReturnMessage = plugin.Config.b079_msg_no_power;
                                return;
                            }
                            a4cooldown = Time.time + 5f;
                            var pos = ev.Player.ReferenceHub.scp079PlayerScript.currentCamera.transform.position;
                            GrenadeManager gm = ev.Player.ReferenceHub.GetComponent<GrenadeManager>();
                            GrenadeSettings settings = gm.availableGrenades.FirstOrDefault(g => g.inventoryID == ItemType.GrenadeFlash);
                            FlashGrenade flash = GameObject.Instantiate(settings.grenadeInstance).GetComponent<FlashGrenade>();
                            flash.fuseDuration = 0.5f;
                            flash.InitData(gm, Vector3.zero, Vector3.zero, 1f);
                            flash.transform.position = pos;
                            NetworkServer.Spawn(flash.gameObject);
                            ev.ReturnMessage = plugin.Config.b079_msg_a4_run;
                            return;
                        }
                        ev.ReturnMessage = plugin.Config.b079_msg_help_cmd_fail.Replace("$prefix", "" + plugin.Config.b079_prefix);
                        return;
                    }
                    ev.ReturnMessage = plugin.Config.b079_msg_help_cmd_fail.Replace("$prefix", "" + plugin.Config.b079_prefix);
                    return;
                }
            }
        }

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
        public void ExpGain(GainingExperienceEventArgs ev)
        {
            if (plugin.Config.xpboost) return;

            foreach (ExpGainType gainType in plugin.Config.experienceGain.Keys)
                if (gainType == ev.GainType)
                    ev.Amount += plugin.Config.experienceGain[gainType];
        }
    }
}