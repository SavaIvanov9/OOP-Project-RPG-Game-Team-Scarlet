using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RPG_ConsoleGame.Core
{
    public delegate void OnMenuClickHandler(string selectedValue);

    public class ViewEngine
    {
        public event OnMenuClickHandler OnMenuClick;

        protected virtual void OnClick(string value)
        {
            if (OnMenuClick != null)
            {
                OnMenuClick(value);
            }
        }

        public void DrawMenu()
        {
            // .. draw and ReadLine()

            OnClick("New Game");
        }
    }
}
