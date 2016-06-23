namespace RPG_ConsoleGame.Interfaces
{
    using System;
    using System.Text;
    using Core.Engines;

    public interface IViewEngine
    {
        event OnMenuClickHandler OnMenuClick;
        void RenderMenu();
        void RenderBattleStats(ICharacter char1, ICharacter char2, StringBuilder history);
        void RenderCredits();
        void RenderMap(char[,] matrix);
        void RenderPlayerStats(IPlayer player);
        void RenderWarningScreen(ConsoleColor color, StringBuilder message, int time, StringBuilder message2 = null);
        IPlayer GetPlayer();
        void StartTimer(int seconds);
    }
}
