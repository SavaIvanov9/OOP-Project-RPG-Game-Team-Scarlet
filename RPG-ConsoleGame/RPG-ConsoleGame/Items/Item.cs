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
        public Item()
        {

        }
        public int Hp { get; set; }
        public int Power { get; set; }
        public int Defence { get; set; }
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
