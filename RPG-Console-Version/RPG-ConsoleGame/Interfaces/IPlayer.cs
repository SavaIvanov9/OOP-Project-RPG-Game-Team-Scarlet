namespace RPG_ConsoleGame.Interfaces
{
    using System;
    using System.Collections.Generic;

    public interface IPlayer : ICharacter, IMoveable, ICollect 
    {
        //PlayerClass Class { get; }
        IList<string> Abilities { get; set; }
        bool IsEnteringBuilding { get; set; }
        void SetPosition(char[,] matrix);
    }
}
