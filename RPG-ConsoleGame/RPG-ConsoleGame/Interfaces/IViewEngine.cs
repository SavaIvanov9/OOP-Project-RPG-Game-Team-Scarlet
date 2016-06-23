namespace RPG_ConsoleGame.Interfaces
{
    using Core;

    public interface IViewEngine
    {
        event OnMenuClickHandler OnMenuClick;
        void DrawMenu();
        void DrawCredits();
        void StartTimer(int seconds);
        IPlayer GetPlayer();
    }
}
