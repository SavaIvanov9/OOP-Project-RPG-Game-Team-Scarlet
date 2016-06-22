using System.Collections.Generic;

namespace RPG_ConsoleGame.Core
{
    using Interfaces;

    public class GameDatabase : IGameDatabase
    {
        private IList<IPlayer> players = new List<IPlayer>();
        private IList<IBot> bots = new List<IBot>();
        private IList<IShopable> shops = new List<IShopable>();

        public IList<IPlayer> Players
        {
            get { return this.players; } 
            set { this.players = value; }
        }

        public IList<IBot> Bots
        {
            get { return this.bots; }
            set { this.bots = value; }
        }

        public IList<IShopable> Shops
        {
            get { return this.shops; }
            set { this.shops = value; }
        }

        public void AddBot(IBot bot)
        {
            Bots.Add(bot);
        }

        public void AddPlayer(IPlayer player)
        {
            Players.Add(player);
        }

        public void AddShop(IShopable shop)
        {
            Shops.Add(shop);
        }
    }
}
