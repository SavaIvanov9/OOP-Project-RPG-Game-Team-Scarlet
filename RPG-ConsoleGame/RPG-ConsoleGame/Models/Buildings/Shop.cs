namespace RPG_ConsoleGame.Models.Buildings
{
    using System.Collections.Generic;
    using Map;
    using Items;
    using Interfaces;

    public class Shop : Building , IShopable
    {
        private IList<Item> inventory;

        public Shop(Position position, char objectSymbol, string name, IList<Item> inventory) : base(position, objectSymbol, name)
        {
            this.Inventory = inventory;

        }

        public IList<Item> Inventory
        {
            get { return this.inventory; }
            set { this.inventory = value; }
        }

        
        //sell item to player or remove item from shop
        public void AddItem(ICharacter player)
        {
  
        }

        //buy item from player or populate shop with itmes
        public void RemoveItem(ICharacter player)
        {

        }
    }
}
