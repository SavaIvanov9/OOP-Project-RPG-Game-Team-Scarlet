namespace RPG_ConsoleGame.Models.Buildings
{
    using Interfaces;
    using System.Collections.Generic;
    using Items;
    using Map;

    public class Shop : Building, IShop
    {
        private IList<Item> shopInventory;

        public Shop(Position position, char objectSymbol, string name, IList<Item> shopInventory) : base(position, objectSymbol, name)
        {
            this.ShopInventory = shopInventory;

        }

        public IList<Item> ShopInventory
        {
            get { return this.shopInventory; }
            set { this.shopInventory = value; }
        }
        
        public void AddItem(ICharacter player)
        {

        }

        public void RemoveItem(ICharacter player)
        {

        }
    }
}
