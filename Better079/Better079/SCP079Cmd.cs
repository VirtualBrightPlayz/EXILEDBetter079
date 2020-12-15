using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CommandSystem;
using Exiled.API.Enums;
using Exiled.API.Features;
using Grenades;
using MEC;
using Mirror;
using RemoteAdmin;
using UnityEngine;

namespace Better079
{
    [CommandHandler(typeof(ClientCommandHandler))]
    public class SCP079Cmd : ICommand
    {
        public string Command => "079";

        public string[] Aliases => new string[] { Better079Plugin.instance.Config.b079_prefix };
        //public string[] Aliases => new string[0];

        public string Description => "SCP-079";

        public bool Execute(ArraySegment<string> arguments, ICommandSender sender, out string response)
        {
            if (sender is PlayerCommandSender)
            {
                var plr = sender as PlayerCommandSender;
                if (plr.ReferenceHub.characterClassManager.NetworkCurClass == RoleType.Scp079)
                {
                    var plugin = Better079Plugin.instance;
                    var args = arguments.Array;
                    {
                        if (args.Length == 2)
                        {
                            if (args[1].ToLower().Equals("help") || args[1].ToLower().Equals("commands") || args[1].ToLower().Equals("?"))
                            {
                                response = plugin.Config.b079_help_title + "\n" +
                                    "\"." + plugin.Config.b079_prefix + " a1\" - " + plugin.Config.b079_help_a1 + " - " + plugin.Config.b079_a1_power + "+ AP - Tier " + (plugin.Config.b079_a1_tier + 1) + "+\n" +
                                    "\"." + plugin.Config.b079_prefix + " a2\" - " + plugin.Config.b079_help_a2 + " - " + plugin.Config.b079_a2_power + "+ AP - Tier " + (plugin.Config.b079_a2_tier + 1) + "+\n" +
                                    "\"." + plugin.Config.b079_prefix + " a3\" - " + plugin.Config.b079_help_a3 + " - " + plugin.Config.b079_a3_power + "+ AP - Tier " + (plugin.Config.b079_a3_tier + 1) + "+\n" +
                                    "\"." + plugin.Config.b079_prefix + " a4\" - " + plugin.Config.b079_help_a4 + " - " + plugin.Config.b079_a4_power + "+ AP - Tier " + (plugin.Config.b079_a4_tier + 1) + "+\n";
                                return true;
                            }

                            if (args[1].ToLower().Equals("a1"))
                            {
                                if (plr.ReferenceHub.scp079PlayerScript.NetworkcurLvl < plugin.Config.b079_a1_tier)
                                {
                                    response = plugin.Config.b079_msg_tier_required.Replace("$tier", "" + (plugin.Config.b079_a1_tier + 1));
                                    return true;
                                }
                                if (plr.ReferenceHub.scp079PlayerScript.NetworkcurMana >= plugin.Config.b079_a1_power)
                                {
                                    plr.ReferenceHub.scp079PlayerScript.NetworkcurMana -= plugin.Config.b079_a1_power;
                                }
                                else
                                {
                                    response = plugin.Config.b079_msg_no_power;
                                    return true;
                                }
                                var cams = PluginEvents.GetSCPCameras();
                                if (cams.Count > 0)
                                {
                                    Camera079 cam = cams[UnityEngine.Random.Range(0, cams.Count)];
                                    plr.ReferenceHub.scp079PlayerScript.CmdSwitchCamera(cam.cameraId, false);
                                    response = plugin.Config.b079_msg_a1_run;
                                    return true;
                                }
                                else
                                {
                                    plr.ReferenceHub.scp079PlayerScript.NetworkcurMana += plugin.Config.b079_a1_power;
                                    response = plugin.Config.b079_msg_a1_fail;
                                    return true;
                                }
                            }

                            if (args[1].ToLower().Equals("a2"))
                            {
                                if (plr.ReferenceHub.scp079PlayerScript.NetworkcurLvl < plugin.Config.b079_a2_tier)
                                {
                                    response = plugin.Config.b079_msg_tier_required.Replace("$tier", "" + (plugin.Config.b079_a2_tier + 1));
                                    return true;
                                }
                                if (plr.ReferenceHub.scp079PlayerScript.NetworkcurMana >= plugin.Config.b079_a2_power)
                                {
                                    plr.ReferenceHub.scp079PlayerScript.NetworkcurMana -= plugin.Config.b079_a2_power;
                                }
                                else
                                {
                                    response = plugin.Config.b079_msg_no_power;
                                    return true;
                                }
                                if (Time.timeSinceLevelLoad - PluginEvents.a2cooldown < plugin.Config.b079_a2_cooldown)
                                {
                                    plr.ReferenceHub.scp079PlayerScript.NetworkcurMana += plugin.Config.b079_a2_power;
                                    response = plugin.Config.b079_msg_a2_fail;
                                    return true;
                                }
                                Room room = PluginEvents.SCP079Room(plr.ReferenceHub);
                                if (room == null)
                                {
                                    plr.ReferenceHub.scp079PlayerScript.NetworkcurMana += plugin.Config.b079_a2_power;
                                    response = plugin.Config.b079_msg_a2_fail;
                                    return true;
                                }
                                if (room.Zone == ZoneType.Surface)
                                {
                                    plr.ReferenceHub.scp079PlayerScript.NetworkcurMana += plugin.Config.b079_a2_power;
                                    response = plugin.Config.b079_msg_a2_fail;
                                    return true;
                                }
                                foreach (var item in plugin.Config.b079_a2_blacklisted_rooms)
                                {
                                    if (room.Name.ToLower().Contains(item.ToLower()))
                                    {
                                        plr.ReferenceHub.scp079PlayerScript.NetworkcurMana += plugin.Config.b079_a2_power;
                                        response = plugin.Config.b079_msg_a2_fail;
                                        return true;
                                    }
                                }
                                Timing.RunCoroutine(PluginEvents.GasRoom(room, plr.ReferenceHub));
                                response = plugin.Config.b079_msg_a2_run;
                                return true;
                            }

                            if (args[1].ToLower().Equals("a3"))
                            {
                                if (plr.ReferenceHub.scp079PlayerScript.NetworkcurLvl < plugin.Config.b079_a3_tier)
                                {
                                    response = plugin.Config.b079_msg_tier_required.Replace("$tier", "" + (plugin.Config.b079_a3_tier + 1));
                                    return true;
                                }
                                if (plr.ReferenceHub.scp079PlayerScript.NetworkcurMana >= plugin.Config.b079_a3_power)
                                {
                                    plr.ReferenceHub.scp079PlayerScript.NetworkcurMana -= plugin.Config.b079_a3_power;
                                }
                                else
                                {
                                    response = plugin.Config.b079_msg_no_power;
                                    return true;
                                }
                                Generator079.Generators[0].ServerOvercharge(plugin.Config.b079_a3_timer, false);
                                response = plugin.Config.b079_msg_a3_run;
                                return true;
                            }

                            if (args[1].ToLower().Equals("a4"))
                            {
                                if (plr.ReferenceHub.scp079PlayerScript.NetworkcurLvl < plugin.Config.b079_a4_tier)
                                {
                                    response = plugin.Config.b079_msg_tier_required.Replace("$tier", "" + (plugin.Config.b079_a4_tier + 1));
                                    return true;
                                }
                                if (plr.ReferenceHub.scp079PlayerScript.NetworkcurMana >= plugin.Config.b079_a4_power)
                                {
                                    plr.ReferenceHub.scp079PlayerScript.NetworkcurMana -= plugin.Config.b079_a4_power;
                                }
                                else
                                {
                                    response = plugin.Config.b079_msg_no_power;
                                    return true;
                                }
                                var pos = plr.ReferenceHub.scp079PlayerScript.currentCamera.transform.position;
                                GrenadeManager gm = plr.ReferenceHub.GetComponent<GrenadeManager>();
                                GrenadeSettings settings = gm.availableGrenades.FirstOrDefault(g => g.inventoryID == ItemType.GrenadeFlash);
                                FlashGrenade flash = GameObject.Instantiate(settings.grenadeInstance).GetComponent<FlashGrenade>();
                                flash.fuseDuration = 0.5f;
                                flash.InitData(gm, Vector3.zero, Vector3.zero, 1f);
                                flash.transform.position = pos;
                                NetworkServer.Spawn(flash.gameObject);
                                response = plugin.Config.b079_msg_a4_run;
                                return true;
                            }
                            response = plugin.Config.b079_msg_help_cmd_fail.Replace("$prefix", "" + plugin.Config.b079_prefix);
                            return true;
                        }
                        response = plugin.Config.b079_msg_help_cmd_fail.Replace("$prefix", "" + plugin.Config.b079_prefix);
                        return true;
                    }
                }
            }
            response = Better079Plugin.instance.Config.b079_msg_help_cmd_fail.Replace("$prefix", "" + Better079Plugin.instance.Config.b079_prefix);
            return false;
        }
    }
}
