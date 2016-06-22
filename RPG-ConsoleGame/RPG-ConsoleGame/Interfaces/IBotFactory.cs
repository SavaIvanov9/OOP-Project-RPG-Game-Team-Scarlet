namespace RPG_ConsoleGame.Interfaces
{
    using Characters;
    using Map;

    public interface IBotFactory 
    {
        IBot CreateBot(Position position, char objectSymbol, string name, PlayerRace race);
    }
}
