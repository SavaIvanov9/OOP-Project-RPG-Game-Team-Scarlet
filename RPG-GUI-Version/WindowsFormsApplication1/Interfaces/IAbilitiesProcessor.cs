namespace WindowsFormsApplication1.Interfaces
{
    public interface IAbilitiesProcessor
    {
        void ProcessCommand(string command, ICharacter player, ICharacter enemy);
    }
}
