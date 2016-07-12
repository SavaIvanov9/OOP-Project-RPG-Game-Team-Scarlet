using RPG_ConsoleGame.Models.Items.NonConsumables;

namespace RPG_ConsoleGame.Characters
{
    using System;
    using System.Collections.Generic;
    using Map;
    using Interfaces;
    using Items;
    using Models.Items;

    [Serializable()]
    public abstract class Character : GameObject, ICharacter
    {
        private string name;
        private int health;
        private int damage;
        private int defence;
        private int energy;
        private int reflexes;
        private IList<string> abilities;
        private IList<IItem> inventory;
        private Dictionary<ItemType, IItem> bodyItems;
        private int gold;

        protected Character(Position position, char objectSymbol, string name,
            int health, int damage, int defence, int energy, int reflexes)
            : base(position, objectSymbol)
        {
            this.Name = name;
            this.Health = health;
            this.Damage = damage;
            this.Defence = defence;
            this.Energy = energy;
            this.Reflexes = reflexes;
            this.Abilities = new List<string>();
            this.Inventory = new List<IItem>();
            this.BodyItems = new Dictionary<ItemType, IItem>();
            this.Gold = 100;
        }

        public Dictionary<ItemType, IItem> BodyItems
        {
            get { return this.bodyItems; }
            protected set
            {
                this.bodyItems = value;
            }
        }

        public string Name
        {
            get
            {
                return this.name;
            }

            set
            {
                if (string.IsNullOrWhiteSpace(value))
                {
                    throw new ArgumentNullException("name", "Name cannot be null, empty or whitespace.");
                }

                this.name = value;
            }
        }
        
        public int Health
        {
            get { return this.health; }
            set { this.health = value; }
        }

        public int Damage
        {
            get { return this.damage; }
            set { this.damage = value; }
        }

        public int Defence
        {
            get
            {
                return this.defence;

            }
            set
            {
                if (value + this.defence >= 85)
                {
                    this.defence = 85;
                }
                else
                {
                    if (this.defence + value <= 0)
                    {
                        this.defence = 0;
                    }
                    else
                    {
                        this.defence = value;
                    }
                }
            }

        }

        public int Energy
        {
            get { return energy; }
            set { energy = value; }
        }

        public int Reflexes
        {
            get { return reflexes; }
            set { reflexes = value; }
        }


        public int Gold
        {
            get { return this.gold; }
            set { this.gold = value; }
        }

        public IList<string> Abilities
        {
            get
            {
                return abilities;
            }
            set
            {
                abilities = value;
            }
        }

        public IList<IItem> Inventory
        {
            get
            {
                return inventory;
            }
            set
            {
                inventory = value;
            }
        }
        
        public void Attack(ICharacter enemy)
        {
            enemy.Health -= this.Damage;
        }

        public abstract void UseItem(int i);
    }
}
