namespace RPG_ConsoleGame.Core.Factories
{
    using Interfaces;
    using Map;
    using Models.Buildings;

    //Factory Pattern
    public class ShopFactory : IShopFactory
    {
        public IShop CreateShop(Position position, char objectSymbol, string name)
        {
            Shop shop = new Shop(position, objectSymbol, name);

            shop.PopulateShop();

            return shop;
        }
    }
}
