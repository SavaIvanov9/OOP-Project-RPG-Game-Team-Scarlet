namespace RPG_ConsoleGame.Core.Factories
{
    using Interfaces;
    using Models.Items;
    using Models.Items.Consumables;
    using Models.Items.NonConsumables;

    //Factory Pattern
    public class ItemFactory : IItemFactory
    {
        public IItem CreateConsumableItem(ItemType type, int level)
        {
            var item = new ConsumableItem(type, level);

            return item;
        }

        public IItem CreateNonConsumableItem(ItemType type, int level)
        {
            var item = new NonConsumableItem(type, level);

            return item;
        }
    }
}
