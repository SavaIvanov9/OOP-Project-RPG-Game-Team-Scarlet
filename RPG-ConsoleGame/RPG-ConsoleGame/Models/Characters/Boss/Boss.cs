namespace RPG_ConsoleGame.Characters
{
    using System;
    using Interfaces;
    using Items;
    using Map;

    public class Boss : Character, IBoss
    {

        public Boss(Position position, char objectSymbol, string name, BossRace race)
            : base(position, objectSymbol, name, 0, 0, 0, 0)
        {
            this.Race = race;
            this.SetPlayerStats();
        }

        public BossRace Race { get; private set; }

        public void AddItemToInventory(Item item)
        {
            Inventory.Add(item);
        }

        public override string ToString()
        {
            return string.Format(
                "Boss {0} ({1}): Damage ({2}), Health ({3}), Reflexes: ({4})",
                this.Name,
                this.Race,
                this.Damage,
                this.Health,
                this.Reflexes);
        }

        private void SetPlayerStats()
        {
            switch (this.Race)
            {
                case BossRace.Boss1:
                    //abilities
                    Abilities.Add("Ability1");
                    Abilities.Add("Ability2");
                    Abilities.Add("Ability3");
                    //passives
                    this.Damage = 10;
                    this.Health = 100;
                    this.Defense = 10;
                    this.Reflexes = 100;
                    break;
                case BossRace.Boss2:
                    //abilities
                    Abilities.Add("Slash");
                    Abilities.Add("Enrage");
                    Abilities.Add("ShieldWall");
                    //passeives
                    this.Damage = 15;
                    this.Health = 200;
                    this.Defense = 20;
                    this.Reflexes = 200;
                    break;
                case BossRace.Boss3:
                    //abilities
                    Abilities.Add("MarkTarget");
                    Abilities.Add("Heavyshot");
                    Abilities.Add("Venomousarrow");
                    //passives
                    this.Damage = 40;
                    this.Health = 130;
                    this.Defense = 15;
                    this.Reflexes = 100;
                    break;
                case BossRace.Boss4:
                    //abilities
                    Abilities.Add("Backstab");
                    Abilities.Add("SharpenBlades");
                    Abilities.Add("Execute");
                    //passive                   
                    this.Damage = 30;
                    this.Health = 130;
                    this.Defense = 10;
                    this.Reflexes = 300;
                    break;
                case BossRace.Boss5:
                    //abilities
                    Abilities.Add("Smite");
                    Abilities.Add("Exorcism");
                    Abilities.Add("Heal");
                    //passive                   
                    this.Damage = 20;
                    this.Health = 180;
                    this.Defense = 20;
                    this.Reflexes = 200;
                    break;
                case BossRace.Boss6:
                    //abilities
                    Abilities.Add("LifeDrain");
                    Abilities.Add("LifeTap");
                    Abilities.Add("ShadowBolt");
                    //passive                   
                    this.Damage = 10;
                    this.Health = 200;
                    this.Defense = 0;
                    this.Reflexes = 100;
                    break;

                default:
                    throw new ArgumentException("Unknown player race.");
            }
        }

        public string MakeDecision()
        {
            return this.Abilities[0];
        }
    }
}