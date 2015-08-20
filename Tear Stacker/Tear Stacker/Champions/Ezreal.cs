using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SharpDX;
using LeagueSharp;
using LeagueSharp.Common;

namespace Tear_Stacker.Champions
{
    class Ezreal : Program
    {
        public Ezreal()
        {
            Game.OnUpdate += Game_OnGameUpdate;
        }

        private static void Game_OnGameUpdate(EventArgs args)
        {
            if (ObjectManager.Player.CountEnemiesInRange(2500f) != 0 || MinionManager.GetMinions(ObjectManager.Player.Position, 2500f, MinionTypes.All, MinionTeam.Enemy, MinionOrderTypes.MaxHealth).Count != 0)
                return;

            if (CompleteChecker())
                Game.OnUpdate -= Game_OnGameUpdate;
            if (!TearChecker() || !ManaChecker() || !Config.Item("active" + name).GetValue<bool>() || !UsageChecker()) return;
            if (Q.IsReady() && Config.Item("qUse" + name).GetValue<bool>())
            {
                Q.Cast(waypointpos);
                DelayChecker();
            }
            else if (W.IsReady() && Config.Item("wUse" + name).GetValue<bool>())
            {
                W.Cast(waypointpos);
                DelayChecker();
            }
            else if (E.IsReady() && Config.Item("eUse" + name).GetValue<bool>())
            {
                E.Cast(waypointpos);
                DelayChecker();
            }



        }
    }
}
