namespace RPG_ConsoleGame.Interfaces
{
    using Items;
    using System.Collections.Generic;

    public interface IShopable
    {
        IList<Item> Inventory { get; set; }

    }
}
