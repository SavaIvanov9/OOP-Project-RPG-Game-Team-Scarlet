using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RPG_ConsoleGame.Map;

namespace RPG_ConsoleGame.Items
{
    public abstract class Item : GameObject
    {
        private int hp;
        private int power;
        private int defence;
        public ItemBodyPossition itemposition;

        public Item(Position position, char objectSymbol, ItemBodyPossition itemBodyPosition, int hp, int defence, int power) 
            : base(position, objectSymbol)
        {
            this.Hp = hp;
            this.Defence = defence;
            this.Power = power;
            this.itemposition = itemBodyPosition;
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
