namespace RPG_ConsoleGame.Interfaces
{
    using System.Collections.Generic;

    public interface IGameDatabase
    {
        IList<IPlayer> Players { get; set; }
        IList<IBot> Bots { get; set; }
        IList<IShopable> Shops { get; set; }

        void AddPlayer(IPlayer player);
        void AddBot(IBot bot);
        void AddShop(IShopable shop);
    }
}
