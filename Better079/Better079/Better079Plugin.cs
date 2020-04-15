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
