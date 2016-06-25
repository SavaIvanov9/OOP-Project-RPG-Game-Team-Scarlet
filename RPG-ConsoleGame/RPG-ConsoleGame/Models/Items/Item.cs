namespace RPG_ConsoleGame.Models.Items
{
    using System;
    using RPG_ConsoleGame.Items;
    using Map;

    [Serializable()]
    public class Item : GameObject
    {
        public ItemBodyPossition Type;
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
