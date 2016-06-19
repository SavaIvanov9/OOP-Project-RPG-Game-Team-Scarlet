namespace RPG_ConsoleGame.Interfaces
{
    using System.Collections.Generic;
    using Map;

    public interface ICharacter : IAttack, IDestroyable
    {
        string Name { get; set; }
        int Defense { get; set; }
        int Reflexes { get; set; }
        IList<string> Abilities { get; set; }
        Position Position { get; set; }
    }
}
