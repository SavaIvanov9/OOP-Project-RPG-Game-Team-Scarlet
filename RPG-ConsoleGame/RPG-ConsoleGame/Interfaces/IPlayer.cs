namespace RPG_ConsoleGame.Interfaces
{
    using System.Collections.Generic;
    using Characters;

    public interface IPlayer : ICharacter, IMoveable, ICollect
    {
        //PlayerClass Class { get; }
        IList<string> Abilities { get; set; }
    }
}
