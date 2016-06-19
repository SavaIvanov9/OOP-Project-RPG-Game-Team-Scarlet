namespace RPG_ConsoleGame.Items
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    using RPG_ConsoleGame.Characters;
    using RPG_ConsoleGame.Map;
    /// <summary>
    /// Item class
    /// </summary>
    public class Item
    {
        private int hp;
        private int power;
        private int defence;
        private ItemPossition itemposition;
        /// <summary>
        /// create an Item with 3 variables for inventory
        /// </summary>
        /// <param name="inputHp"></param>
        /// <param name="inputDefence"></param>
        /// <param name="inputPower"></param>
        public Item(double inputHp, double inputDefence, double inputPower)
        {
            this.Power = inputPower;
            this.Hp = inputHp;
            this.Defence = inputDefence;
            this.itemposition = ItemPossition.inventory;
        }
        /// <summary>
        /// Create an Item constructor with 4 variables
        /// </summary>
        /// <param name="inputHp"></param>
        /// <param name="inputDefence"></param>
        /// <param name="inputPower"></param>
        /// <param name="inputPossition"></param>
        public Item(double inputHp, double inputDefence, double inputPower, ItemPossition inputPossition)
        {
            this.Hp = inputHp;
            this.Defence = inputDefence;
            this.Power = inputPower;
            this.ItemPossiton = inputPossition;
        }
        /// <summary>
        /// Hp prop
        /// </summary>

        public double Hp
        {
            get
            {
                return this.hp;
            }

            private set
            {
                this.hp = (int)value;
            }
        }
        /// <summary>
        /// deff prop
        /// </summary>

        public double Defence
        {
            get;
            private set
            {
                this.defence = (int)value;
            }
        }
        /// <summary>
        /// 
        /// </summary>

        public double Power
        {
            get;
            private set
            {
                this.power = (int)value;
            }
        }
        /// <summary>
        /// 
        /// </summary>
        public ItemPossition ItemPossiton
        {
            get;
            private set
            {
                if ((int)value < 0 && (int)value > 5)
                {
                    throw new ArgumentException("Item possiton should be 0-5");
                }
                else
                {
                    this.itemposition = value;
                }
            }
        }
        /// <summary>
        /// function for generating a new Item
        /// </summary>
        /// <returns>new Item</returns>
        public Item GetItem()
        {
            Random rnd = new Random();
            double tempPower = rnd.Next(10 - 100);
            double powercoef = 0.7;
            Array values = Enum.GetValues(typeof(ItemPossition));
            ItemPossition rndPos = (ItemPossition)values.GetValue(rnd.Next(values.Length));
            double coef = 0;
            double tempHp = tempPower * coef;
            double tempDeff = tempHp / 10;
            if (tempPower >= 50)
            {
                coef += tempPower * powercoef;
            }
            else
            {
                coef += tempPower * (powercoef + 0.5);
            }

            switch (rndPos)
            {
                case ItemPossition.helmet:
                case ItemPossition.chest:
                case ItemPossition.hands:
                case ItemPossition.boots:
                    //nothing here  some other logic some other time
                    break;
                case ItemPossition.inventory:
                    tempPower = 0;
                    break;
                case ItemPossition.weapon:
                    tempPower = tempPower * 1.1;
                    tempDeff = 0;
                    break;
                default:
                    break;
            }

            return new Item(tempHp, tempDeff, tempPower, rndPos);
        }
    }
}
