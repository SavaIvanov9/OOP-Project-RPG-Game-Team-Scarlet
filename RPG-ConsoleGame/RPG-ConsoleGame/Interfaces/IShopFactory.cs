namespace RPG_ConsoleGame.Interfaces
{
    using Map;

    public interface IShopFactory
    {
        IShopable CreateShop(Position position, char objectSymbol, string name);
    }
}
