namespace RPG_ConsoleGame.Interfaces
{
<<<<<<< HEAD
    using Items;
=======
    using System.Collections.Generic;
    using Map;
>>>>>>> 832b300dbc599325fca42c08d22d0c5bac4df48f

    public interface ICharacter : IAttack, IDestroyable
    {
        string Name { get; set; }
        int Defense { get; set; }
        int Reflexes { get; set; }
        IList<string> Abilities { get; set; }
        Position Position { get; set; }

        void AddItem(Item item);
    }
}
