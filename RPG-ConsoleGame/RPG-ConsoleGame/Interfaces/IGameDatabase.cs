namespace RPG_ConsoleGame.Interfaces
{
    using System.Collections.Generic;

    public interface IGameDatabase
    {
        IList<char[,]> Maps { get; set; }
        IList<IPlayer> Players { get; set; }
        IList<ICreature> Creatures { get; set; }
        IList<IShop> Shops { get; set; }
        IList<IBoss> Bosses { get; set; }

        void AddMap(char[,] map);
        void AddPlayer(IPlayer player);
        void AddCreature(ICreature creature);
        void AddShop(IShop shop);
        void AddBoss(IBoss boss);
        void ClearData();
    }
}
