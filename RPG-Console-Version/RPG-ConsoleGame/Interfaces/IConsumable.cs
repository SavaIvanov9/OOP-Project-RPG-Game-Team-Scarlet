namespace RPG_ConsoleGame.Interfaces
{
    public interface IConsumable : IItem
    {
        void UseItem(int health, int damage, int defence, int energy, int reflexes);
    }
}
