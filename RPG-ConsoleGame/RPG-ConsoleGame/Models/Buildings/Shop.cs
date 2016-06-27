namespace RPG_ConsoleGame.Models.Buildings
{
    using System;
    using RPG_ConsoleGame.Items;
    using Interfaces;
    using System.Collections.Generic;
    using Items;
    using Map;

    [Serializable()]
    public class Shop : Building, IShop
    {
        private IList<Item> helmets;
        private IList<Item> chest;
        private IList<Item> hands;
        private IList<Item> weapons;
        private IList<Item> boots;
        private IList<Item> inventory;

        public Shop(Position position, char objectSymbol, string name)
            : base(position, objectSymbol, name)
        {
            this.Helmets = new List<Item>();
            this.Chest = new List<Item>();
            this.Hands = new List<Item>();
            this.Weapons = new List<Item>();
            this.Boots = new List<Item>();
            this.Inventory = new List<Item>();

        }

        //public IList<Item> ShopInventory
        //{
        //    get { return this.shopInventory; }
        //    set { this.shopInventory = value; }
        //}

        public IList<Item> Helmets
        {
            get { return this.helmets; }
            set { this.helmets = value; }
        }

        public IList<Item> Chest
        {
            get { return this.chest; }
            set { this.chest = value; }
        }

        public IList<Item> Hands
        {
            get { return this.hands; }
            set { this.hands = value; }
        }

        public IList<Item> Weapons
        {
            get { return this.weapons; }
            set { this.weapons = value; }
        }

        public IList<Item> Boots
        {
            get { return this.boots; }
            set { this.boots = value; }
        }

        public IList<Item> Inventory
        {
            get { return this.inventory; }
            set { this.inventory = value; }
        }

        public void AddItem(ICharacter player)
        {
             
        }

        public void RemoveItem(ICharacter player)
        {

        }

        public void PopulateShop()
        {
            for (int i = 0; i < 3; i++)
            {
                this.Helmets.Add(new Item(new Position(3, 3), 'I', ItemBodyPossition.Helmet, 50, 50, 50));
                this.Chest.Add(new Item(new Position(3, 3), 'I', ItemBodyPossition.Chest, 50, 50, 50));
                this.Hands.Add(new Item(new Position(3, 3), 'I', ItemBodyPossition.Hands, 50, 50, 50));
                this.Helmets.Add(new Item(new Position(3, 3), 'I', ItemBodyPossition.Helmet, 50, 50, 50));
                this.Weapons.Add(new Item(new Position(3, 3), 'I', ItemBodyPossition.Weapon, 50, 50, 50));
                this.Boots.Add(new Item(new Position(3, 3), 'I', ItemBodyPossition.Boots, 50, 50, 50));
                this.Inventory.Add(new Item(new Position(3, 3), 'I', ItemBodyPossition.Inventory, 50, 50, 50));
            }
        }

        public object Clone()
        {
            throw new NotImplementedException();
        }
    }
}
