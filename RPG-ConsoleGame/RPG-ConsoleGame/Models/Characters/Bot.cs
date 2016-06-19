namespace RPG_ConsoleGame.Characters
{
    using System;
    using Interfaces;
    using Items;
    using Map;

    public class Bot: Character, IBot
    {
        //private readonly List<Item> inventory;

        public int currentRow = 1;
        public int currentCol = 1;

        public Bot(Position position, char objectSymbol, string name, PlayerRace race)
            : base(position, objectSymbol, name, 0, 0, 0, 0)
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
                "Player {0} ({1}): Damage ({2}), Health ({3}), Reflexes: ({4})",
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
                case PlayerRace.Mage:
                    //abilities
                    Abilities.Add("Fireball");
                    Abilities.Add("Hellfire");
                    Abilities.Add("Reflect");
                    //passives
                    this.Damage = 10;
                    this.Health = 100;
                    this.Defense = 10;
                    this.Reflexes = 100;
                    break;
                case PlayerRace.Warrior:
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
                case PlayerRace.Archer:
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
                case PlayerRace.Rogue:
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
                case PlayerRace.Paladin:
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
                case PlayerRace.Warlock:
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
