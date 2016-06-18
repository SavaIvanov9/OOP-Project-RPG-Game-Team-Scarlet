using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RPG_ConsoleGame.Interfaces
{
    public interface IAbilitiesProcessor
    {
        void ProcessCommand(string command, ICharacter player, ICharacter enemy);
    }
}
