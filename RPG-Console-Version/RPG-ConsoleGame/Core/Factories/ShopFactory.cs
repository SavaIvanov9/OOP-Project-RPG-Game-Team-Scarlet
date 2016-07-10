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
            var shop = new Shop(position, objectSymbol, name);
            
            return shop;
        }
    }
}
