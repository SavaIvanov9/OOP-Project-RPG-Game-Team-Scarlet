using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RPG_ConsoleGame.Models.Items;

namespace RPG_ConsoleGame.Interfaces
{
    public interface IItem
    {
        ItemType Type { get; set; }
        int Level { get; set; }
        void UseItem(int health, int damage, int defence, int energy, int reflexes);
    }
}
