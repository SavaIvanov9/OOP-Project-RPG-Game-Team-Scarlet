namespace RPG_ConsoleGame.Interfaces
{
    using System.Collections;
    using Items;
    using System.Collections.Generic;

    public interface IShopable
    {
        IList<Item> Inventory { get; set; }

    }
}
