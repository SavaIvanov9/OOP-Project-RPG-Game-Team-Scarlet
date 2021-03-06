﻿namespace RPG_ConsoleGame.Models.Items
{
    using System;
    using Exceptions;
    using Interfaces;

    [Serializable()]
    public abstract class Item : IItem
    {
        private ItemType type;
        private int level;
        protected int health;
        protected int damage;
        protected int defence;
        protected int energy;
        protected int reflexes;
        private int gold;

        protected Item(ItemType type, int level)
        {
            this.Type = type;
            this.Level = level;
        }

        public int Gold
        {
            get { return this.gold; }
            protected set { this.gold = value; }
        }

        public ItemType Type
        {
            get
            {
                return this.type;
            }

            set
            {
                this.type = value;
            }
        }

        public int Level
        {
            get
            {
                return this.level;
            }

            set
            {
                if (value <= 0 || value > 3)
                {
                    throw new IncorrectLevelException("Invalid level. Level must be in range of [1:3]");
                }

                this.level = value;
            }
        }

        public abstract void UseItem(ICharacter character);
    }
}
