namespace RPG_ConsoleGame.Interfaces
{
    using Models.Items;
    using System.Collections.Generic;
    using Map;

    public interface ICharacter : IAttack, IDestroyable
    {
        string Name { get; set; }
        int Health { get; set; }
        int Damage { get; set; }
        int Defence { get; set; }
        int Energy { get; set; }
        int Reflexes { get; set; }
        IList<string> Abilities { get; set; }
        IList<IItem> Inventory { get; set; }
        Position Position { get; set; }
    }
}
