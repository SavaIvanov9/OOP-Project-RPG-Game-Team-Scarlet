namespace WindowsFormsApplication1.Interfaces
{
    using Map;
    using Models.Characters.PlayerControlled;

    public interface ICreatureFactory
    {
        ICreature CreateCreature(Position position, char objectSymbol, string name, PlayerRace race);
    }
}
