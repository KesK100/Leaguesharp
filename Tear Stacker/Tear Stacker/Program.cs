using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Remoting.Messaging;
using System.Text;
using System.Threading.Tasks;
using SharpDX;
using LeagueSharp;
using LeagueSharp.Common;
using Tear_Stacker.Champions;

namespace Tear_Stacker
{
    class Program
    {
        public static String name = ObjectManager.Player.ChampionName;
        public static Menu Config = new Menu("Tear Stacker", "Tear Stacker", true);
        public static Spell Q, W, E, R;

        public static Vector3 waypointpos; 
        
        public Obj_AI_Hero Player { get { return ObjectManager.Player; } }
        private static int Tear = (int) ItemId.Tear_of_the_Goddess;
        private static int Tear2 = (int)ItemId.Tear_of_the_Goddess_Crystal_Scar;

        private static int Muramana = 3042;
        private static int Seraph = 3040;

        private static int Manamune = (int)ItemId.Manamune;
        private static int Manamune2 = (int)ItemId.Manamune_Crystal_Scar;
        private static int Archangels = (int)ItemId.Archangels_Staff;
        private static int Archangels2 = (int)ItemId.Archangels_Staff_Crystal_Scar;
        private static Boolean Delay = true;

        static void Main(string[] args)
        {
            CustomEvents.Game.OnGameLoad += Game_OnGameLoad;
        }

        private static void Game_OnGameLoad(EventArgs args)
        {
            Game.PrintChat(name);
            if (!ChampionLoader())
                return;

            Config.SubMenu(name).AddItem(new MenuItem("usageType" + name, "Usage Type").SetValue(new StringList(new[] { "Only Base", "Only While Toggle", "Base or Toggle" })));
            Config.SubMenu(name).AddItem(new MenuItem("qUse" + name, "Use Q").SetValue(false));
            Config.SubMenu(name).AddItem(new MenuItem("wUse" + name, "Use W").SetValue(false));
            Config.SubMenu(name).AddItem(new MenuItem("eUse" + name, "Use E").SetValue(false));
          //  Config.AddItem(new MenuItem("rUse" + name, "Use R").SetValue(false));
            Config.SubMenu(name).AddItem(new MenuItem("toggle" + name, "Toggle", false).SetValue(new KeyBind("U".ToCharArray()[0], KeyBindType.Toggle)));
            Config.SubMenu(name).AddItem(new MenuItem("manaLimit" + name, "Min. Mana to Stack").SetValue(new Slider(80, 100, 0)));
            Config.SubMenu(name).AddItem(new MenuItem("active" + name, "Stacker Active").SetValue(false));
            Config.AddToMainMenu();
            Q = new Spell(SpellSlot.Q);
            W = new Spell(SpellSlot.W);
            E = new Spell(SpellSlot.E);
            R = new Spell(SpellSlot.R);
            Game.PrintChat("<font color=\"#4EE2EC\">Tear Stacker</font> - KesK");

            Obj_AI_Base.OnIssueOrder += Obj_AI_Base_OnIssueOrder;
        }

        private static void Obj_AI_Base_OnIssueOrder(Obj_AI_Base sender, GameObjectIssueOrderEventArgs args)
        {
            waypointpos = args.TargetPosition;
        }

        protected static bool UsageChecker()
        {
            switch (Config.Item("usageType" + name).GetValue<StringList>().SelectedIndex)
            {
                case 0:
                    if (ObjectManager.Player.InShop())
                        return true;
                    break;
                case 1:
                    if (Config.Item("toggle" + name).GetValue<KeyBind>().Active)
                        return true;
                    break;
                case 2:
                    if (ObjectManager.Player.InShop() || Config.Item("toggle" + name).GetValue<KeyBind>().Active)
                        return true;
                    break;
            }
            return false;
        }

        protected static bool ManaChecker()
        {
            return ObjectManager.Player.ManaPercent > Config.Item("manaLimit" + name).GetValue<Slider>().Value;
        }

        protected static bool TearChecker()
        {
            return ((Items.HasItem(Tear) || Items.HasItem(Tear2)) || (Items.HasItem(Manamune) || Items.HasItem(Manamune2)) || (Items.HasItem(Archangels) || Items.HasItem(Archangels2))) && Delay;
        }

        protected static bool CompleteChecker()
        {
            return Items.HasItem(Muramana) || Items.HasItem(Seraph);
        }

        protected static void DelayChecker()
        {
            Delay = false;
            Utility.DelayAction.Add(3000, () =>
            {
                Delay = true;
            });
        }

        private static bool ChampionLoader()
        {
            switch (name)
            {
                case "Anivia":
                    new Anivia();
                    return true;
                case "Cassiopeia":
                    new Cassiopeia();
                    return true;
                case "Jayce":
                    new Jayce();
                    return true;
                case "Karthus":
                    new Karthus();
                    return true;
                case "KogMaw":
                    new Kogmaw();
                    return true;
                case "Nidalee":
                    new Nidalee();
                    return true;
                case "Ryze":
                    new Ryze();
                    return true;
                case "Singed":
                    new Singed();
                    return true;
                case "Varus":
                    new Varus();
                    return true;
                case "Yorick":
                    new Yorick();
                    return true;
                case "Zilean":
                    new Zilean();
                    return true;
                default:
                    return false;
            }
        }

    }
}
