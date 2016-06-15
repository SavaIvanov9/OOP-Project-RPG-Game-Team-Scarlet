using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RPG_ConsoleGame.Characters;
using RPG_ConsoleGame.Map;

namespace RPG_ConsoleGame.Interfaces
{
    interface IPlayerFactory
    {
        IPlayer CreateHuman(Position position, char objectSymbol, string name, PlayerClass race);
    }
}
