namespace RPG_ConsoleGame.Interfaces
{
    using Models.Items;

    public interface IItemFactory
    {
        IItem CreateNonConsumableItem(ItemType type, int level);

        IItem CreateConsumableItem(ItemType type, int level);
    }
}
