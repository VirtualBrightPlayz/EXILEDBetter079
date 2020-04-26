using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EXILED;
using EXILED.Extensions;

namespace Better079
{
    public class Better079Plugin : Plugin
    {
        public override string getName => "Better079";
        public PluginEvents PLEV;
        public float A1Power;
        public float A2Power;
        public float A3Power;
        public float A4Power;
        public int A1Tier;
        public int A2Tier;
        public int A3Tier;
        public int A4Tier;
        public float A3Timer;
        public int A2Timer;
        public int A2TimerGas;
        public float A2Exp;
        public float A1MinDist;
        public List<string> A2BlacklistRooms;
        public bool A2ScpDmg;

        public string CommandPrefix;
        public string HelpMsgTitle;
        public string HelpMsgA1;
        public string HelpMsgA2;
        public string HelpMsgA3;
        public string HelpMsgA4;

        public string TierRequiredMsg;
        public string NoPowerMsg;
        public string HelpMsg;

        public string RunA1Msg;
        public string FailA1Msg;
        public string RunA2Msg;
        public string FailA2Msg;
        public string RunA3Msg;
        public string RunA4Msg;

        public string A2WarnMsg;
        public string A2ActiveMsg;
        public string SpawnMsg;

        public override void OnDisable()
        {
            if (!Config.GetBool("b079_enable"))
                return;
            Events.RoundStartEvent -= PLEV.RoundStart;
            Events.PlayerSpawnEvent -= PLEV.PlayerSpawn;
            Events.ConsoleCommandEvent -= PLEV.ConsoleCmd;
            PLEV = null;
        }

        public override void OnEnable()
        {
            if (!Config.GetBool("b079_enable"))
                return;
            A1MinDist = Config.GetFloat("b079_a1_dist", 15f);
            A1Power = Config.GetFloat("b079_a1_power", 15f);
            A1Tier = Config.GetInt("b079_a1_tier", 0);

            A4Power = Config.GetFloat("b079_a4_power", 40f);
            A4Tier = Config.GetInt("b079_a4_tier", 1);

            A3Power = Config.GetFloat("b079_a3_power", 100f);
            A3Tier = Config.GetInt("b079_a3_tier", 1);
            A3Timer = Config.GetFloat("b079_a3_timer", 30f);

            A2Power = Config.GetFloat("b079_a2_power", 75f);
            A2Tier = Config.GetInt("b079_a2_tier", 2);
            A2Timer = Config.GetInt("b079_a2_timer", 5);
            A2TimerGas = Config.GetInt("b079_a2_gas_timer", 10);
            A2Exp = Config.GetFloat("b079_a2_exp", 35f);
            A2BlacklistRooms = Config.GetStringList("b079_a2_blacklisted_rooms");
            if (A2BlacklistRooms == null)
            {
                A2BlacklistRooms = new List<string>();
            }
            A2ScpDmg = Config.GetBool("b079_a2_scpdmg", false);


            CommandPrefix = Config.GetString("b079_prefix", "079").Replace(' ', '_');
            HelpMsgTitle = Config.GetString("b079_help_title", "Abilities/Commands:");
            HelpMsgA1 = Config.GetString("b079_help_a1", "Teleport to your SCP friends");
            HelpMsgA2 = Config.GetString("b079_help_a2", "Activate a memetic in the current room (only on humans)");
            HelpMsgA3 = Config.GetString("b079_help_a3", "Shutdown Zone Lighting");
            HelpMsgA4 = Config.GetString("b079_help_a4", "Camera Flash (blinds others)");
            
            TierRequiredMsg = Config.GetString("b079_msg_tier_required", "Tier $tier+ Required");
            NoPowerMsg = Config.GetString("b079_msg_no_power", "Not enough power.");
            HelpMsg = Config.GetString("b079_msg_help_cmd_fail", "Invalid. Type \".$prefix ?\" for help.");

            FailA1Msg = Config.GetString("b079_msg_a1_fail", "No SCPs found.");
            RunA1Msg = Config.GetString("b079_msg_a1_run", "Teleporting...");

            FailA2Msg = Config.GetString("b079_msg_a2_fail", "Memetics don't work here!");
            RunA2Msg = Config.GetString("b079_msg_a2_run", "Activating...");

            RunA3Msg = Config.GetString("b079_msg_a3_run", "Overcharging...");
            RunA4Msg = Config.GetString("b079_msg_a4_run", "Flashing...");

            A2WarnMsg = Config.GetString("b079_msg_a2_warn", "<color=#ff0000>MEMETIC KILL AGENT will activate in this room in $seconds seconds.</color>");
            A2ActiveMsg = Config.GetString("b079_msg_a2_active", "<color=#ff0000>MEMETIC KILL AGENT ACTIVATED.</color>");
            SpawnMsg = Config.GetString("b079_spawn_msg", "<color=#00ff00>[Better079] Type \".079 help\" in the console for more abilities.</color>");


            PLEV = new PluginEvents(this);
            Events.RoundStartEvent += PLEV.RoundStart;
            Events.PlayerSpawnEvent += PLEV.PlayerSpawn;
            Events.ConsoleCommandEvent += PLEV.ConsoleCmd;
        }

        public override void OnReload()
        {
        }
    }
}
