using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RPG_ConsoleGame.Map;

namespace RPG_ConsoleGame.Interfaces
{
    public interface ICharacter : IAttack, IDestroyable
    {
        int Defense { get; set; }
        string Name { get; set; }

        Position Position { get; set; }
    }
}
