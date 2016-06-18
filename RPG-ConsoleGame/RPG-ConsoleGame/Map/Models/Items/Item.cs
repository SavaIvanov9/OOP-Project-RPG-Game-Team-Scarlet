namespace RPG_ConsoleGame.Items
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    using RPG_ConsoleGame.Characters;
    using RPG_ConsoleGame.Map;

    public class Item
    {
        private double hp;
        private double power;
        private double defence;
        private ItemPossition itemposition;

        public Item(double hp, double defence, double power, ItemPossition possition)
        {
            this.Hp = hp;
            this.Defence = defence;
            this.Power = power;
            this.itemposition = possition;
        }

        public double Hp { get; private set; }

        public double Power { get; private set; }

        public double Defence { get; private set; }
        public ItemPossition ItemPossiton { get; set; }

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
