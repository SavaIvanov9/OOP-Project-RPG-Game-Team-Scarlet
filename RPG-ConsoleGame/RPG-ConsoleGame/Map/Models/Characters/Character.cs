using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RPG_ConsoleGame.Map;

namespace RPG_ConsoleGame.Characters
{
    using Interfaces;
    using Items;
    public abstract class Character : GameObject, ICharacter
    {
        
        private string name;
        private int health;
        private int defense;
        private int reflexes;
        private IList<string> abilities;
        private IList<Item> inventory;

        protected Character(Position position, char objectSymbol, string name, 
            int damage, int health, int defence, int reflexes)
            : base(position, objectSymbol)
        {
            this.Damage = damage;
            this.Health = health;
            this.Name = name;
            this.Defense = defense;
            this.Reflexes = reflexes;
            this.Abilities = new List<string>();
            this.Inventory = new List<Item>();
        }

        public int Damage { get; set; }

        public int Health { get; set; }

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

        public int Defense
        {
            get
            {
                return this.defense;

            }
            set 
            {
                if (value+this.defense>=85)
                {
                    this.defense = 85;
                }
                else
                {
                    if (this.defense+value<=0)
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

        public int Reflexes
        {
            get { return reflexes; }
            set { reflexes = value; }
        }

        public void Attack(ICharacter enemy)
        {
            enemy.Health -= this.Damage;
        }
    }
}
