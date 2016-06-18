using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RPG_ConsoleGame.Map;

namespace RPG_ConsoleGame.Interfaces
{
    using Items;

    public interface ICharacter : IAttack, IDestroyable
    {
        string Name { get; set; }
        int Defense { get; set; }
        IList<string> Abilities { get; set; }
        Position Position { get; set; }

        void AddItem(Item item);
    }
}
