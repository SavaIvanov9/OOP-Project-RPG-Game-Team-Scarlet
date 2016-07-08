namespace WindowsFormsApplication1.Models.Characters
{
    using System;
    using System.Collections.Generic;
    using Interfaces;
    using Map;
    using Items;

    [Serializable()]
    public abstract class Character : GameObject, ICharacter
    {
        private string name;
        private int health;
        private int damage;
        private int defense;
        private int energy;
        private int reflexes;
        private IList<string> abilities;
        private IList<Item> inventory;

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
            this.Inventory = new List<Item>();
        }

        public string Name
        {
            get
            {
                return this.name;
            }

            set
            {
                if (string.IsNullOrEmpty(value))
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
                return this.defense;

            }
            set
            {
                if (value + this.defense >= 85)
                {
                    this.defense = 85;
                }
                else
                {
                    if (this.defense + value <= 0)
                    {
                        this.defense = 0;
                    }
                    else
                    {
                        this.defense = value;
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

        public IList<Item> Inventory
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
    }
}
