namespace RPG_ConsoleGame.Interfaces
{
    using Items;
    using System.Collections.Generic;

    public interface IShop
    {
        IList<Item> ShopInventory { get; set; }
    }
}
