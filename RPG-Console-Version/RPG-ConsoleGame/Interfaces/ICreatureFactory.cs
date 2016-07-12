using RPG_ConsoleGame.Models.Characters.PlayerControlled;

namespace RPG_ConsoleGame.Interfaces
{
    using Characters;
    using Map;

    public interface ICreatureFactory 
    {
        ICreature CreateCreature(Position position, char objectSymbol, string name, PlayerRace race);
    }
}
