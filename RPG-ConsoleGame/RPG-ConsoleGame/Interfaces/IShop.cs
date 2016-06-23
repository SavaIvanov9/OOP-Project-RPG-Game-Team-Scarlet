namespace RPG_ConsoleGame.Interfaces
{
    using Items;
    using System.Collections.Generic;
    using Map;

    public interface IShop
    {
        IList<Item> ShopInventory { get; set; }
        Position Position { get; set; }
    }
}
