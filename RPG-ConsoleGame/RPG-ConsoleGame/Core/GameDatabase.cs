namespace RPG_ConsoleGame.Core
{
    using System.Collections.Generic;
    using Interfaces;

    public class GameDatabase : IGameDatabase
    {
        private IList<IPlayer> players = new List<IPlayer>();
        private IList<IBot> bots = new List<IBot>();
        private IList<IBoss> bosses = new List<IBoss>();

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

        public IList<IBoss> Bosses
        {
            get { return this.bosses; }
            set { this.bosses = value; }
        }

        public void AddBot(IBot bot)
        {
            Bots.Add(bot);
        }

        public void AddPlayer(IPlayer player)
        {
            Players.Add(player);
        }

        public void AddBoss(IBoss boss)
        {
            Bosses.Add(boss);
        }
    }
}
