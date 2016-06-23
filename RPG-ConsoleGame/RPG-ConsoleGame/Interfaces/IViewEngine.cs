namespace RPG_ConsoleGame.Interfaces
{
    using System;
    using System.Text;
    using Core;

    public interface IViewEngine
    {
        event OnMenuClickHandler OnMenuClick;
        void DrawMenu();
        void DrawCredits();
        void WarningScreen(ConsoleColor color, StringBuilder message, int time, StringBuilder message2 = null);
        void StartTimer(int seconds);
        IPlayer GetPlayer();
    }
}
