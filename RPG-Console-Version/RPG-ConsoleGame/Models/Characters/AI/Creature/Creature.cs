namespace RPG_ConsoleGame.Models.Characters.AI.Creature
{
    using System;
    using RPG_ConsoleGame.Characters;
    using Interfaces;
    using Map;
    using Items;
                          
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
                    Abilities.Add("Arcane Blast"); //direct dmg
                    Abilities.Add("Hellfire"); //DOT dmg
                    Abilities.Add("Mana Shield"); //shield + some hp
                    Abilities.Add("Recharge energy"); //add mana
                    //passives
                    //stats
                    this.Health = 300;
                    this.Damage = 75;
                    this.Defence = 0;
                    this.Energy = 100;
                    this.Reflexes = 80;
                    break;
                case PlayerRace.Warrior:
                    //abilities
                    Abilities.Add("Slash"); //direct dmg
                    Abilities.Add("Bleeding Wounds"); //dot dmg
                    Abilities.Add("Enrage"); //add dmg
                    Abilities.Add("Regenerate"); //recover some hp + some energy
                    //passeives
                    //stats
                    this.Health = 750;
                    this.Damage = 25;
                    this.Defence = 0;
                    this.Energy = 100;
                    this.Reflexes = 50;
                    break;
                case PlayerRace.Archer:
                    //abilities
                    Abilities.Add("Heavyshot"); //direct dmg
                    Abilities.Add("Venomous Arrow"); //dot dmg
                    Abilities.Add("Aim"); //add dmg
                    Abilities.Add("Activate Critical Shot"); //add chance to do 1.5x dmg
                    //passives
                    //stats
                    this.Health = 400;
                    this.Damage = 50;
                    this.Defence = 0;
                    this.Energy = 100;
                    this.Reflexes = 70;
                    break;
                case PlayerRace.Rogue:
                    //abilities
                    Abilities.Add("Backstab"); //direct dmg
                    Abilities.Add("SharpenBlades"); //add dmg + energy
                    Abilities.Add("Execute"); //if enemy under 50% hp, deal dmg x2
                    Abilities.Add("Disable"); //enemy deal -30% dmg
                    //passive                   
                    //stats
                    this.Health = 450;
                    this.Damage = 40;
                    this.Defence = 0;
                    this.Energy = 100;
                    this.Reflexes = 70;
                    break;
                case PlayerRace.Paladin:
                    //abilities
                    Abilities.Add("Smite"); //direct dmg
                    Abilities.Add("Righteous Strike"); //direct dmg + heal
                    Abilities.Add("Heal"); //heal
                    Abilities.Add("Divine Shield"); //add a lot hp for 1 turn
                    //passive                   
                    //stats
                    this.Health = 500;
                    this.Damage = 25;
                    this.Defence = 0;
                    this.Energy = 100;
                    this.Reflexes = 60;
                    break;
                case PlayerRace.Warlock:
                    //abilities
                    Abilities.Add("ShadowBolt"); //direct dmg, -hp, -en
                    Abilities.Add("Shadow Curse"); //dot
                    Abilities.Add("LifeDrain"); //dmg + heal
                    Abilities.Add("LifeTap"); //convert hp to en
                    //passive                   
                    //stats
                    this.Health = 600;
                    this.Damage = 25;
                    this.Defence = 0;
                    this.Energy = 100;
                    this.Reflexes = 60;
                    break;

                default:
                    throw new ArgumentException("Unknown player race.");
            }
        }

        public override string MakeDecision()
        {
            return this.Abilities[0];
        }

        public override void UseItem(int i)
        {
            
        }
    }
}
