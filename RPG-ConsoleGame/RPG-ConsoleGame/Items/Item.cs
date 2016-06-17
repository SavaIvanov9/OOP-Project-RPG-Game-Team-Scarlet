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
        private int hp;
        private int power;
        private int defence;
        public Itempossition itemposition;
        public Item(int hp, int defence, int power, Itempossition possition)
        {
            this.Hp = hp;
            this.Defence = defence;
            this.Power = power;
            this.itemposition = possition;
        }
        public int Hp { get; set; }
        public int Power { get; set; }
        public int Defence { get; set; }
        public Item GetItem(Position position)
        {
            Random rnd=new Random();
            int tempPower = rnd.Next(10-100);
            Array values = Enum.GetValues(typeof(Itempossition));
            Itempossition rndPos = (Itempossition)values.GetValue(rnd.Next(values.Length));
            ///add item possin in the coef
            int coef=0;
            if (tempPower>=50)	{
                 coef+=(int)(tempPower*0.7);
            	}
            else{
                  coef+=(int)(tempPower*1.2);
            	}
            int tempHp=(int)(tempPower*coef);
            int tempDeff=tempHp/10;

            return new Item(tempHp,tempDeff,tempPower,rndPos);
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
