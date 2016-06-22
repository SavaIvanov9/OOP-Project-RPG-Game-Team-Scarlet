namespace RPG_ConsoleGame.Core.Factories
{
    using Interfaces;
    using Map;
    using Models.Buildings;
    using Items;
    using System.Collections.Generic;

    public class ShopFactory : IShopFactory
    {
        public IShopable CreateShop(Position position, char objectSymbol, string name)
        {
            Shop shop = new Shop(position, objectSymbol, name, new List<Item>());

            return shop;
        }
    }
}
