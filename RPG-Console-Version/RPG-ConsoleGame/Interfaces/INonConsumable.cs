namespace RPG_ConsoleGame.Interfaces
{
    public interface INonConsumable : IItem
    {
        void UnEquipItem(ICharacter character);
    }
}
