namespace WindowsFormsApplication1.Interfaces
{
    using Map;
    using Models.Characters.PlayerControlled;

    interface IPlayerFactory
    {
        IPlayer CreatePlayer(Position position, char objectSymbol, string name, PlayerRace race);
    }
}
