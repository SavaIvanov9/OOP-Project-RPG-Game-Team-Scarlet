﻿namespace RPG_ConsoleGame.Core
{
    using System.Collections.Generic;
    using Interfaces;

    public class GameDatabase : IGameDatabase
    {
        private IList<IPlayer> players = new List<IPlayer>();
        private IList<ICreature> creatures = new List<ICreature>();
        private IList<IBoss> bosses = new List<IBoss>();
        private IList<IShop> shops = new List<IShop>();
        
        public IList<IPlayer> Players
        {
            get { return this.players; } 
            set { this.players = value; }
        }

        public IList<ICreature> Creatures
        {
            get { return this.creatures; }
            set { this.creatures = value; }
        }

        public IList<IShop> Shops
        {
            get { return this.shops; }
            set { this.shops = value; }
        }

        public IList<IBoss> Bosses
        {
            get { return this.bosses; }
            set { this.bosses = value; }
        }
        
        public void AddCreature(ICreature creature)
        {
            Creatures.Add(creature);
        }

        public void AddPlayer(IPlayer player)
        {
            Players.Add(player);
        }

        public void AddShop(IShop shop)
        {
            Shops.Add(shop);
        }
        
        public void AddBoss(IBoss boss)
        {
            Bosses.Add(boss);
        }
    }
}
