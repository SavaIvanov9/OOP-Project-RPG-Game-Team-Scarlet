namespace RPG_ConsoleGame.Interfaces
{
    public interface INonConsumable : IItem
    {
        void UseItem(int health, int damage, int defence, int energy, int reflexes);
    }
}
