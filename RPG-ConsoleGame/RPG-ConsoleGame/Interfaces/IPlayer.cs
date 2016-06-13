using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RPG_ConsoleGame.Interfaces
{
    using Characters;
    public interface IPlayer : ICharacter, IMoveable, ICollect
    {
        //PlayerClass Class { get; }
    }
}
