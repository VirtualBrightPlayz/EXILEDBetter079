using Exiled.API.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Better079
{
    public class Config : IConfig
    {
        public bool IsEnabled { get; set; } = true;

        public string b079_prefix { get; set; } = "079";
        public string b079_help_title { get; set; } = "Abilities/Commands:";
        public string b079_help_a1 { get; set; } = "Teleport to your SCP friends";
        public string b079_help_a2 { get; set; } = "Activate a memetic in the current room (only on humans)";
        public string b079_help_a3 { get; set; } = "Shutdown Zone Lighting";
        public string b079_help_a4 { get; set; } = "Camera Flash (blinds others)";

        public string b079_msg_tier_required { get; set; } = "Tier $tier+ Required";
        public string b079_msg_no_power { get; set; } = "Not enough power.";
        public string b079_msg_help_cmd_fail { get; set; } = "Invalid. Type \".$prefix ?\" for help.";

        public string b079_msg_a1_fail { get; set; } = "No SCPs found.";
        public string b079_msg_a1_run { get; set; } = "Teleporting...";

        public string b079_msg_a2_fail { get; set; } = "Memetics don't work here!";
        public string b079_msg_a2_run { get; set; } = "Activating...";

        public string b079_msg_a3_run { get; set; } = "Overcharging...";
        public string b079_msg_a4_run { get; set; } = "Flashing...";

        public string b079_msg_a2_warn { get; set; } = "<color=#ff0000>MEMETIC KILL AGENT will activate in this room in $seconds seconds.</color>";
        public string b079_msg_a2_active { get; set; } = "<color=#ff0000>MEMETIC KILL AGENT ACTIVATED.</color>";
        public string b079_spawn_msg { get; set; } = "<color=#00ff00>[Better079] Type \".079 help\" in the console for more abilities.</color>";

        public float b079_a1_dist { get; set; } = 15f;
        public float b079_a1_power { get; set; } = 15f;
        public int b079_a1_tier { get; set; } = 0;

        public float b079_a4_power { get; set; } = 40f;
        public int b079_a4_tier { get; set; } = 1;

        public float b079_a3_power { get; set; } = 100f;
        public float b079_a3_timer { get; set; } = 30f;
        public int b079_a3_tier { get; set; } = 1;

        public float b079_a2_power { get; set; } = 75f;
        public int b079_a2_tier { get; set; } = 2;
        public int b079_a2_timer { get; set; } = 5;
        public int b079_a2_gas_timer { get; set; } = 10;
        public float b079_a2_exp { get; set; } = 35f;
        public float b079_a2_cooldown { get; set; } = 60f;
        public List<string> b079_a2_blacklisted_rooms { get; set; } = new List<string>();
        public bool b079_a2_disable_cassie { get; set; } = false;
    }
}
