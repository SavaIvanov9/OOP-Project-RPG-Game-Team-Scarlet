namespace RPG_ConsoleGame.Interfaces
{
    using System.Collections.Generic;
    using Map;
    using Models.Items;

    public interface IShop : IBuilding
    {
        IList<IItem> Helmets { get; set; }
        IList<IItem> Chests { get; set; }
        IList<IItem> Hands { get; set; }
        IList<IItem> Weapons { get; set; }
        IList<IItem> Boots { get; set; }
        IList<IItem> Potions { get; set; }
        IList<IItem> Scrolls { get; set; }
        IList<IItem> Bags { get; set; }

        Position Position { get; set; }
        void TransferItemToCharacter(ICharacter character, ItemType type, int level);
        void TransferItemToShop(ICharacter character, int index);
        void PopulateShop();
    }
}
