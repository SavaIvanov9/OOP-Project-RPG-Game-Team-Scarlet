namespace WindowsFormsApplication1.Interfaces
{
    using Map;

    public interface IShopFactory
    {
        IShop CreateShop(Position position, char objectSymbol, string name);
    }
}
