<<<<<<< HEAD
﻿namespace RPG_ConsoleGame.Core.Factories
{
    using Interfaces;
    using Map;
    using System.Collections.Generic;
    using Models.Buildings;
    using Items;

    public class ShopFactory : IShopFactory
    {
        public IShop CreateShop(Position position, char objectSymbol, string name)
        {
            Shop shop = new Shop(position, objectSymbol, name, new List<Item>());

            return shop;
        }
    }
}
=======
﻿namespace RPG_ConsoleGame.Core.Factories
{
    using Interfaces;
    using Map;
    using System.Collections.Generic;
    using Models.Buildings;
    using Items;

    public class ShopFactory : IShopFactory
    {
        public IShop CreateShop(Position position, char objectSymbol, string name)
        {
            Shop shop = new Shop(position, objectSymbol, name, new List<Item>());

            return shop;
        }
    }
}
>>>>>>> refs/remotes/origin/master
