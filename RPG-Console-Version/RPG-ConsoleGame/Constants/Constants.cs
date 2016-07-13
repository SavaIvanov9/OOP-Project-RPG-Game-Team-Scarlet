namespace RPG_ConsoleGame.Constants
{
    using System;

    public static class Constants
    {
        public static string PlayerRaceDescription =
            "Choose a race:"
            + Environment.NewLine +
            "1. Mage (Health: 600, Damage: 150, Defense: 0, Energy: 200, Reflexes: 80)" +
            Environment.NewLine +
            "2. Warrior (Health: 1500, Damage: 50, Defense: 0, Energy: 100, Reflexes: 50)" +
            Environment.NewLine +
            "3. Archer (Health: 800, Damage: 100, Defense: 0, Energy: 100, Reflexes: 70)" +
            Environment.NewLine +
            "4. Rogue (Health: 900, Damage: 80, Defense: 0, Energy: 100, Reflexes: 70)" +
            Environment.NewLine +
            "5. Paladin (Health: 1000, Damage: 50, Defense: 0, Energy: 100, Reflexes: 60)" +
            Environment.NewLine +
            "6. Warlock (Health: 1200, Damage: 50, Defense: 0, Energy: 100, Reflexes: 60)";

        public static string AbilitiesDescription =
            "ArcaneBlast = Energy cost: 20, deals character's dmg +50." +
            Environment.NewLine +
            "HellFire = Energy cost: 20, deals character's dmg +25 and dot efect" +
            Environment.NewLine +
            "ManaShield = Energy cost: 20, protects character from 100 dmg." +
            Environment.NewLine +
            "RechargeEnergy = Recharges 100 energy." + Environment.NewLine +
            Environment.NewLine +

            "Slash = Energy cost: 20, deals character's dmg +25." +
            Environment.NewLine +
            "BleedingWounds = Energy cost: 20,  deals character's dmg/2 and dot efect." +
            Environment.NewLine +
            "Enrage = Energy cost: 10, hp cost: 50, adds dmg x2." +
            Environment.NewLine +
            "Regenerate = Adds +50 energy, +100 hp." +
            Environment.NewLine + Environment.NewLine +

            "Heavyshot = Energy cost: 20, deals character's dmg +100." +
            Environment.NewLine +
            "VenomousArrow = Energy cost: 20, deals character's dmg/2 + dot efect." +
            Environment.NewLine +
            "Aim = Adds +100 dmg." + Environment.NewLine +
            "ActivateCriticalShot = Adds chance to deal dmg x3." +
            Environment.NewLine + Environment.NewLine +

            "Backstab = Energy cost: 40, deals character's dmg x2." +
            Environment.NewLine +
            "SharpenBlades = Energy cost: 20, adds +50 dmg" +
            Environment.NewLine +
            "Execute = Energy cost: 20, deals character's dmg x3 if enemy hp is under 300." +
            Environment.NewLine +
            "Disable = Reduces enemy dmg" +
            Environment.NewLine + Environment.NewLine +

            "Smite = Energy cost: 20, deals character's dmg +50." +
            Environment.NewLine +
            "RighteousStrike = Deals character's dmg/2, heals for character's dmg." +
            Environment.NewLine +
            "Heal = Energy cost: 30, adds hp equals own dmg *2" +
            Environment.NewLine +
            "DivineShield = Prevents huge amount of dmg for 1 turn." +
            Environment.NewLine + Environment.NewLine +

            "LifeDrain = Energy cost: 20, deals character's dmg and heals for the same amount." +
            Environment.NewLine +
            "ShadowBolt = Energy cost: 20, hp cost: 100, deals character's dmg + 200." +
            Environment.NewLine +
            "ShadowCurse = Deals dot dmg." +
            Environment.NewLine +
            "LifeTap = Hp cost: character's dmg, add own dmg* as energy.";

        //Mage
        public static string ArcaneBlast = "Energy cost: 20, deals character's dmg +50.";
        public static string HellFire = "Energy cost: 20, deals character's dmg +25 and dot efect";
        public static string ManaShield = "Energy cost: 20, protects character from 100 dmg.";
        public static string RechargeEnergy = "Recharges 100 energy.";

        //Warrior
        public static string Slash = "Energy cost: 20, deals character's dmg +25.";
        public static string BleedingWounds = "Energy cost: 20,  deals character's dmg/2 and dot efect.";
        public static string Enrage = "Energy cost: 10, hp cost: 50, adds dmg x2.";
        public static string Regenerate = "Adds +50 energy, +100 hp.";

        //Archer
        public static string Heavyshot = "Energy cost: 20, deals character's dmg +100.";
        public static string VenomousArrow = "Energy cost: 20, deals character's dmg/2 + dot efect.";
        public static string Aim = "Adds +100 dmg.";
        public static string ActivateCriticalShot = "Adds chance to deal dmg x3.";

        // Rogue
        public static string Backstab = "Energy cost: 40, deals character's dmg x2.";
        public static string SharpenBlades = "Energy cost: 20, adds +50 dmg";
        public static string Execute = "Energy cost: 20, deals character's dmg x3 if enemy hp is under 300.";
        public static string Disable = "Reduces enemy dmg";

        //Paladin
        public static string Smite = "Energy cost: 20, deals character's dmg +50.";
        public static string RighteousStrike = "Deals character's dmg/2, heals for character's dmg.";
        private static string Heal = "Energy cost: 30, adds hp equals own dmg *2";
        public static string DivineShield = "Prevents huge amount of dmg for 1 turn.";

        //Warlock
        public static string LifeDrain = "Energy cost: 20, deals character's dmg and heals for the same amount.";
        public static string ShadowBolt = "Energy cost: 20, hp cost: 100, deals character's dmg + 200.";
        public static string ShadowCurse = "Deals dot dmg.";
        public static string LifeTap = "Hp cost: character's dmg, add own dmg* as energy.";

        ////Boss1
        //public static string
        //    public static string
        //    public static string
        //    public static string
        //private void Ability1(ICharacter player, ICharacter enemy)
        //{
        //    player.Energy -= 20;
        //    enemy.Health -= (player.Damage + 40);
        //}

        //private void Ability2(ICharacter player, ICharacter enemy)
        //{
        //    player.Energy -= 20;
        //    enemy.Health -= (player.Damage + 15);

        //}

        //private void Ability3(ICharacter player, ICharacter enemy)
        //{
        //    player.Energy -= 20;
        //    enemy.Health -= enemy.Damage;
        //    player.Health += enemy.Damage;
        //}
    }
}
