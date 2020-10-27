using Exiled.API.Interfaces;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Better079
{
    public class Config : IConfig
    {
        [Description("The plugin is enabled or not")]
        public bool IsEnabled { get; set; } = true;

        [Description("The command prefix")]
        public string b079_prefix { get; set; } = "s079";
        [Description("Help texts")]
        public string b079_help_title { get; set; } = "Abilities/Commands:";
        public string b079_help_a1 { get; set; } = "Teleport to your SCP friends";
        public string b079_help_a2 { get; set; } = "Activate a memetic in the current room (only on humans)";
        public string b079_help_a3 { get; set; } = "Shutdown Zone Lighting";
        public string b079_help_a4 { get; set; } = "Camera Flash (blinds others)";

        [Description("Error msg")]
        public string b079_msg_tier_required { get; set; } = "Tier $tier+ Required";
        public string b079_msg_no_power { get; set; } = "Not enough power.";
        public string b079_msg_help_cmd_fail { get; set; } = "Invalid. Type \".$prefix ?\" for help.";

        [Description("Message activation A1")]
        public string b079_msg_a1_fail { get; set; } = "No SCPs found.";
        public string b079_msg_a1_run { get; set; } = "Teleporting...";

        [Description("Message A2")]
        public string b079_msg_a2_fail { get; set; } = "Memetics don't work here!";
        public string b079_msg_a2_run { get; set; } = "Activating...";

        [Description("Message A3")]
        public string b079_msg_a3_run { get; set; } = "Overcharging...";
        [Description("Message A4")]
        public string b079_msg_a4_run { get; set; } = "Flashing...";

        [Description("Memetic agent warning for humans in the room")]
        public string b079_msg_a2_warn { get; set; } = "<color=#ff0000>MEMETIC KILL AGENT will activate in this room in $seconds seconds.</color>";
        [Description("Memetic message activation")]
        public string b079_msg_a2_active { get; set; } = "<color=#ff0000>MEMETIC KILL AGENT ACTIVATED.</color>";
        [Description("SCP-079 spawn message")]
        public string b079_spawn_msg { get; set; } = "<color=#00ff00>[Better079] Type \".079 help\" in the console for more abilities.</color>";

        [Description("Configs A1 | teleport | distance skill A1")]
        public float b079_a1_dist { get; set; } = 15f;
        [Description("Power needed")]
        public float b079_a1_power { get; set; } = 15f;
        [Description("Tier required | 0 = Tier 1 | 1 = Tier 2 | 2 = Tier 3 | 3 = Tier 4 | 4 = Tier 5")]
        public int b079_a1_tier { get; set; } = 0;

        [Description("Configs A4 | Flash | power needed")]
        public float b079_a4_power { get; set; } = 40f;
        [Description("Tier required | 0 = Tier 1 | 1 = Tier 2 | 2 = Tier 3 | 3 = Tier 4 | 4 = Tier 5")]
        public int b079_a4_tier { get; set; } = 1;

        [Description("Configs A3 | facility lights")]
        public float b079_a3_power { get; set; } = 100f;
        [Description("Failure time")]
        public float b079_a3_timer { get; set; } = 30f;
        [Description("Tier required | 0 = Tier 1 | 1 = Tier 2 | 2 = Tier 3 | 3 = Tier 4 | 4 = Tier 5")]
        public int b079_a3_tier { get; set; } = 1;

        [Description("Configs A2 | Memetic kill agent")]
        public float b079_a2_power { get; set; } = 75f;
        [Description("Tier required | 0 = Tier 1 | 1 = Tier 2 | 2 = Tier 3 | 3 = Tier 4 | 4 = Tier 5")]
        public int b079_a2_tier { get; set; } = 2;
        [Description("Time before the doors close")]
        public int b079_a2_timer { get; set; } = 5;
        [Description("Memetic agent duration")]
        public int b079_a2_gas_timer { get; set; } = 10;
        [Description("Killing XP with memetic agent")]
        public float b079_a2_exp { get; set; } = 35f;
        [Description("Cooldown")]
        public float b079_a2_cooldown { get; set; } = 60f;
        [Description("Blacklist rooms | recommended: - LCZ_914 - EZ_GateB - EZ_GateA - HCZ_106")]
        public List<string> b079_a2_blacklisted_rooms { get; set; } = new List<string>();
        [Description("Cassie announcement memetic agent | for the sake of your ears leave this on false")]
        public bool b079_a2_disable_cassie { get; set; } = false;
        [Description("Is the XP boost activated")]
        public bool xpboost { get; set; } = false;
        [Description("SCP-079 XP boost, it does not affect the exp of the memetic agent | Default: KillAssist = 0 DirectKill = 1, HardwareHack = 2, AdminCheat = 3, GeneralInteractions = 4, PocketAssist = 5")]
        public Dictionary<ExpGainType, float> experienceGain { get; set; } = new Dictionary<ExpGainType, float> {
            {
                ExpGainType.GeneralInteractions, 5
            },
            {
                ExpGainType.KillAssist, 2
            }
        };
    }
}
