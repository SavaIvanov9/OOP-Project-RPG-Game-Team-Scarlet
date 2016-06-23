namespace RPG_ConsoleGame.Interfaces
{
    using Items;
    using System.Collections.Generic;
    using Map;

    public interface IShop
    {
        IList<Item> Helmets { get; set; }
        IList<Item> Chest { get; set; }
        IList<Item> Hands { get; set; }
        IList<Item> Weapons { get; set; }
        IList<Item> Boots { get; set; }
        IList<Item> Inventory { get; set; }

        Position Position { get; set; }
        //void AddItem();
        //void RemoveItem();
        void PopulateShop();
    }
}
