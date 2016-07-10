using RPG_ConsoleGame.Exceptions;

namespace RPG_ConsoleGame.Models.Items
{
    using System;
   
    [Serializable()]
    public abstract class Item
    {
        private ItemType type;
        private int level;
        protected int health;
        protected int damage;
        protected int defence;
        protected int energy;
        protected int reflexes;

        protected Item(ItemType type, int level)
        {
            this.Type = type;
            this.Level = level;
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
                if (level <= 0 || level > 3)
                {
                    throw new IncorrectLevelException("Invalid level. Level must be in range of [1:3]");
                }

                this.level = value;
            }
        }
    }
}
