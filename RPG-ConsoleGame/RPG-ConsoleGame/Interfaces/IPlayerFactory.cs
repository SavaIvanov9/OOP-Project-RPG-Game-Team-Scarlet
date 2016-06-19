namespace RPG_ConsoleGame.Interfaces
{
    using Characters;
    using Map;

    interface IPlayerFactory
    {
        IPlayer CreateHuman(Position position, char objectSymbol, string name, PlayerRace race);
    }
}
