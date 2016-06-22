namespace RPG_ConsoleGame.Interfaces
{
    public interface IAbilitiesProcessor
    {
        void ProcessCommand(string command, ICharacter player, ICharacter enemy);
    }
}
