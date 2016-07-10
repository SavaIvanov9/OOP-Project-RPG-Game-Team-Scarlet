namespace RPG_ConsoleGame.Interfaces
{
    using System.Collections.Generic;
    using Map;
    using Models.Items;

    public interface IShop
    {
        IList<Item> Helmets { get; set; }
        IList<Item> Chest { get; set; }
        IList<Item> Hands { get; set; }
        IList<Item> Weapons { get; set; }
        IList<Item> Boots { get; set; }
        IList<Item> Inventory { get; set; }

        Position Position { get; set; }
        void AddItem(ICharacter player, string itemType);
        void RemoveItem(ICharacter player, string itemType);
        void PopulateShop();
    }
}
