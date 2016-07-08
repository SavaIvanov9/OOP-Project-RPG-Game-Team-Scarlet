namespace WindowsFormsApplication1.Interfaces
{
    using System;
    using System.Collections.Generic;

    public interface IGameDatabase
    {
        IList<char[,]> Maps { get; set; }
        IList<IPlayer> Players { get; set; }
        IList<ICreature> Creatures { get; set; }
        IList<IShop> Shops { get; set; }
        IList<IBoss> Bosses { get; set; }
        bool IsLoaded { get; set; }
        DateTime Date { get; set; }

        void AddMap(char[,] map);
        void AddPlayer(IPlayer player);
        void AddCreature(ICreature creature);
        void AddShop(IShop shop);
        void AddBoss(IBoss boss);
        void ClearData();
        void LoadData(IGameDatabase data);
    }
}
