namespace RPG_ConsoleGame.Interfaces
{
    using Core;

    public interface IViewEngine
    {
        event OnMenuClickHandler OnMenuClick;
        void DrawMenu();
        IPlayer GetPlayer();
    }
}
