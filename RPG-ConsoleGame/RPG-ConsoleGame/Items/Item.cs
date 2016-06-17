using RPG_ConsoleGame.Characters;
using RPG_ConsoleGame.Map;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RPG_ConsoleGame.Items
{
    public class Item
    {
        private double hp;
        private double power;
        private double defence;
        public Itempossition itemposition;
        public Item(double hp, double defence, double power, Itempossition possition)
        {
            this.Hp = hp;
            this.Defence = defence;
            this.Power = power;
            this.itemposition = possition;
        }
        public double Hp { get; private set; }
        public double Power { get; private set; }
        public double Defence { get; private set; }
        public Item GetItem()
        {
            Random rnd = new Random();
            double tempPower = rnd.Next(10 - 100);
            double powercoef = 0.7;
            Array values = Enum.GetValues(typeof(Itempossition));
            Itempossition rndPos = (Itempossition)values.GetValue(rnd.Next(values.Length));
            double coef = 0;
            double tempHp = tempPower * coef;
            double tempDeff = tempHp / 10;
            if (tempPower >= 50)
            {
                coef += (tempPower * powercoef);
            }
            else
            {
                coef += (tempPower * (powercoef + 0.5));
            }
            switch (rndPos)
            {
                case Itempossition.helmet:
                case Itempossition.chest:
                case Itempossition.hands:
                case Itempossition.boots:
                    ///nothing here  some other logic some other time
                    break;
                case Itempossition.inventory:
                    tempPower = 0;
                    break;
                case Itempossition.weapon:
                    tempPower = (tempPower * 1.1);
                    tempDeff = 0;
                    break;
                default:
                    break;
            }
            return new Item(tempHp, tempDeff, tempPower, rndPos);
        }
        /// TO DO Add Item, possition based on List<Items>
        /// 0-Head
        /// 1-Chest
        /// 2-hands
        /// 3-Wepon
        /// 4-Boots
        /// Enum of possition
        /// 
    }
}
