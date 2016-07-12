using RPG_ConsoleGame.Models.Characters.PlayerControlled;

namespace RPG_ConsoleGame.Interfaces
{
    using Map;

    interface IPlayerFactory
    {
        IPlayer CreatePlayer(Position position, char objectSymbol, string name, PlayerRace race);
    }
}
