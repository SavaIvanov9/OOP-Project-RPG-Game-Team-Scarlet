namespace RPG_ConsoleGame.Models.Characters.Bots.Creature
{
    using System;
    using Interfaces;
    using RPG_ConsoleGame.Characters;
    using Models.Items;
    using Map;

    [Serializable()]
    public class Creature: Bot, ICreature
    {
        //private readonly List<Item> inventory;

        //public int currentRow = 1;
        //public int currentCol = 1;

        public Creature(Position position, char objectSymbol, string name, PlayerRace race)
            : base(position, objectSymbol, name, 0, 0, 0, 0, 0)
        {
            this.Race = race;
            //this.inventory = new List<Item>();
            this.SetPlayerStats();
            //this.CurrentCol = currentCol;
            //this.CurrentRow = currentRow;

        }

        //public int CurrentCol { get; set; }
        //public int CurrentRow { get; set; }

        public PlayerRace Race { get; private set; }
        
        public void AddItemToInventory(Item item)
        {
            Inventory.Add(item);
        }

        //public void Heal()
        //{
        //    var beer = this.inventory.FirstOrDefault() as Beer;

        //    if (beer == null)
        //    {
        //        throw new NotEnoughBeerException("Not enough beer!!!");
        //    }

        //    this.Health += beer.HealthRestore;
        //    this.inventory.Remove(beer);
        //}

        public override string ToString()
        {
            return string.Format(
                   "Player {0} ({1}): Health ({2}),  Damage ({3}), Defence({4}), Energy ({5}), Reflexes: ({6})",
                   this.Name,
                   this.Race,
                   this.Health,
                   this.Damage,
                   this.Defence,
                   this.Energy,
                   this.Reflexes);
        }

        private void SetPlayerStats()
        {
            switch (this.Race)
            {
                case PlayerRace.Mage:
                    //abilities
                    Abilities.Add("Fireball");
                    Abilities.Add("Hellfire");
                    Abilities.Add("Reflect");
                    //passives
                    //stats
                    this.Health = 600;
                    this.Damage = 100;
                    this.Defence = 10;
                    this.Energy = 100;
                    this.Reflexes = 50;
                    break;
                case PlayerRace.Warrior:
                    //abilities
                    Abilities.Add("Slash");
                    Abilities.Add("Enrage");
                    Abilities.Add("ShieldWall");
                    //passeives
                    //stats
                    this.Health = 800;
                    this.Damage = 50;
                    this.Defence = 10;
                    this.Energy = 100;
                    this.Reflexes = 60;
                    break;
                case PlayerRace.Archer:
                    //abilities
                    Abilities.Add("MarkTarget");
                    Abilities.Add("Heavyshot");
                    Abilities.Add("Venomousarrow");
                    //passives
                    //stats
                    this.Health = 500;
                    this.Damage = 100;
                    this.Defence = 10;
                    this.Energy = 100;
                    this.Reflexes = 70;
                    break;
                case PlayerRace.Rogue:
                    //abilities
                    Abilities.Add("Backstab");
                    Abilities.Add("SharpenBlades");
                    Abilities.Add("Execute");
                    //passive                   
                    //stats
                    this.Health = 600;
                    this.Damage = 90;
                    this.Defence = 10;
                    this.Energy = 100;
                    this.Reflexes = 100;
                    break;
                case PlayerRace.Paladin:
                    //abilities
                    Abilities.Add("Smite");
                    Abilities.Add("Exorcism");
                    Abilities.Add("Heal");
                    //passive                   
                    //stats
                    this.Health = 800;
                    this.Damage = 50;
                    this.Defence = 10;
                    this.Energy = 100;
                    this.Reflexes = 60;
                    break;
                case PlayerRace.Warlock:
                    //abilities
                    Abilities.Add("LifeDrain");
                    Abilities.Add("LifeTap");
                    Abilities.Add("ShadowBolt");
                    //passive                   
                    //stats
                    this.Health = 500;
                    this.Damage = 100;
                    this.Defence = 10;
                    this.Energy = 100;
                    this.Reflexes = 50;
                    break;

                default:
                    throw new ArgumentException("Unknown player race.");
            }
        }

        public override string MakeDecision()
        {
            return this.Abilities[0];
        }
    }
}
