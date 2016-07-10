using RPG_ConsoleGame.Models.Items.Consumables;

namespace RPG_ConsoleGame.Models.Buildings
{
    using System;
    using Interfaces;
    using System.Collections.Generic;
    using Items;
    using Map;
    using System.Linq;
    using System.Text;
    using Core.Factories;
    using Exceptions;

    [Serializable()]
    public class Shop : Building, IShop
    {
        private IList<IItem> helmets;
        private IList<IItem> chest;
        private IList<IItem> hands;
        private IList<IItem> weapons;
        private IList<IItem> boots;
        private IList<IItem> bags;
        private IList<IItem> potions;
        private IList<IItem> scrolls;
        private readonly IItemFactory itemFactory = new ItemFactory();

        public Shop(Position position, char objectSymbol, string name)
            : base(position, objectSymbol, name)
        {
            this.Helmets = new List<IItem>();
            this.Chests = new List<IItem>();
            this.Hands = new List<IItem>();
            this.Weapons = new List<IItem>();
            this.Boots = new List<IItem>();
            this.Bags = new List<IItem>();
            this.Potions = new List<IItem>();
            this.Scrolls = new List<IItem>();
            PopulateShop();
        }

        public IList<IItem> Helmets
        {
            get { return this.helmets; }
            set { this.helmets = value; }
        }

        public IList<IItem> Chests
        {
            get { return this.chest; }
            set { this.chest = value; }
        }

        public IList<IItem> Hands
        {
            get { return this.hands; }
            set { this.hands = value; }
        }

        public IList<IItem> Weapons
        {
            get { return this.weapons; }
            set { this.weapons = value; }
        }

        public IList<IItem> Boots
        {
            get { return this.boots; }
            set { this.boots = value; }
        }

        public IList<IItem> Bags
        {
            get { return this.bags; }
            set { this.bags = value; }
        }

        public IList<IItem> Potions
        {
            get { return this.potions; }
            set { this.potions = value; }
        }

        public IList<IItem> Scrolls
        {
            get { return this.scrolls; }
            set { this.scrolls = value; }
        }

        public void TransferItemToCharacter(ICharacter character, ItemType type, int level)
        {
            switch (type)
            {
                case ItemType.Helmet:
                    character.Inventory.Add(this.Helmets.First(x => x.Level.Equals(level)));
                    this.Helmets.Remove(this.Helmets.First(x => x.Level.Equals(level)));
                    break;
                case ItemType.Chest:
                    character.Inventory.Add(this.Chests.First(x => x.Level.Equals(level)));
                    this.Chests.Remove(this.Chests.First(x => x.Level.Equals(level)));
                    break;
                case ItemType.Hands:
                    character.Inventory.Add(this.Hands.First(x => x.Level.Equals(level)));
                    this.Hands.Remove(this.Hands.First(x => x.Level.Equals(level)));
                    break;
                case ItemType.Boots:
                    character.Inventory.Add(this.Boots.First(x => x.Level.Equals(level)));
                    this.Boots.Remove(this.Boots.First(x => x.Level.Equals(level)));
                    break;
                case ItemType.Weapon:
                    character.Inventory.Add(this.Weapons.First(x => x.Level.Equals(level)));
                    this.Weapons.Remove(this.Weapons.First(x => x.Level.Equals(level)));
                    break;
                case ItemType.PotionHealth:
                    character.Inventory.Add(this.Potions.First(x => x.Level.Equals(level)));
                    this.Potions.Remove(this.Potions.First(x => x.Level.Equals(level)));
                    break;
                case ItemType.PotionEnergy:
                    character.Inventory.Add(this.Potions.First(x => x.Level.Equals(level)));
                    this.Potions.Remove(this.Potions.First(x => x.Level.Equals(level)));
                    break;
                case ItemType.ScrollGuardian:
                    character.Inventory.Add(this.Scrolls.First(x => x.Level.Equals(level)));
                    this.Scrolls.Remove(this.Scrolls.First(x => x.Level.Equals(level)));
                    break;
                case ItemType.ScrollDestruction:
                    character.Inventory.Add(this.Scrolls.First(x => x.Level.Equals(level)));
                    this.Scrolls.Remove(this.Scrolls.First(x => x.Level.Equals(level)));
                    break;
                default:
                    throw new IncorrectTypeException("Incorrect item type in shop");
                    break;
            }
        }

        public void TransferItemToShop(ICharacter character, int index)
        {
            switch (character.Inventory[index].Type)
            {
                case ItemType.Helmet:
                    this.Helmets.Add(character.Inventory[index]);
                    character.Inventory.RemoveAt(index);
                    break;
                case ItemType.Chest:
                    this.Chests.Add(character.Inventory[index]);
                    character.Inventory.RemoveAt(index);
                    break;
                case ItemType.Hands:
                    this.Hands.Add(character.Inventory[index]);
                    character.Inventory.RemoveAt(index);
                    break;
                case ItemType.Boots:
                    this.Boots.Add(character.Inventory[index]);
                    character.Inventory.RemoveAt(index);
                    break;
                case ItemType.Weapon:
                    this.Weapons.Add(character.Inventory[index]);
                    character.Inventory.RemoveAt(index);
                    break;
                case ItemType.PotionHealth:
                    this.Potions.Add(character.Inventory[index]);
                    character.Inventory.RemoveAt(index);
                    break;
                case ItemType.PotionEnergy:
                    this.Potions.Add(character.Inventory[index]);
                    character.Inventory.RemoveAt(index);
                    break;
                case ItemType.ScrollGuardian:
                    this.Scrolls.Add(character.Inventory[index]);
                    character.Inventory.RemoveAt(index);
                    break;
                case ItemType.ScrollDestruction:
                    this.Scrolls.Add(character.Inventory[index]);
                    character.Inventory.RemoveAt(index);
                    break;
                default:
                    throw new IncorrectTypeException("Incorrect item type in shop");
                    break;
            }
        }

        public void PopulateShop()
        {
            for (int i = 0; i < 3; i++)
            {
                this.Helmets.Add(itemFactory.CreateNonConsumableItem(ItemType.Helmet, 1));
                this.Helmets.Add(itemFactory.CreateNonConsumableItem(ItemType.Helmet, 2));
                this.Helmets.Add(itemFactory.CreateNonConsumableItem(ItemType.Helmet, 3));

                this.Chests.Add(itemFactory.CreateNonConsumableItem(ItemType.Chest, 1));
                this.Chests.Add(itemFactory.CreateNonConsumableItem(ItemType.Chest, 2));
                this.Chests.Add(itemFactory.CreateNonConsumableItem(ItemType.Chest, 3));

                this.Hands.Add(itemFactory.CreateNonConsumableItem(ItemType.Hands, 1));
                this.Hands.Add(itemFactory.CreateNonConsumableItem(ItemType.Hands, 2));
                this.Hands.Add(itemFactory.CreateNonConsumableItem(ItemType.Hands, 3));

                this.Hands.Add(itemFactory.CreateNonConsumableItem(ItemType.Hands, 1));
                this.Hands.Add(itemFactory.CreateNonConsumableItem(ItemType.Hands, 2));
                this.Hands.Add(itemFactory.CreateNonConsumableItem(ItemType.Hands, 3));

                this.Boots.Add(itemFactory.CreateNonConsumableItem(ItemType.Boots, 1));
                this.Boots.Add(itemFactory.CreateNonConsumableItem(ItemType.Boots, 2));
                this.Boots.Add(itemFactory.CreateNonConsumableItem(ItemType.Boots, 3));

                this.Weapons.Add(itemFactory.CreateNonConsumableItem(ItemType.Weapon, 1));
                this.Weapons.Add(itemFactory.CreateNonConsumableItem(ItemType.Weapon, 2));
                this.Weapons.Add(itemFactory.CreateNonConsumableItem(ItemType.Weapon, 3));

                //this.Potions.Add(new ConsumableItem(ItemType.PotionEnergy, 1));
                this.Potions.Add(itemFactory.CreateConsumableItem(ItemType.PotionEnergy, 1));
                this.Potions.Add(itemFactory.CreateConsumableItem(ItemType.PotionEnergy, 2));
                this.Potions.Add(itemFactory.CreateConsumableItem(ItemType.PotionEnergy, 3));

                this.Potions.Add(itemFactory.CreateConsumableItem(ItemType.PotionHealth, 1));
                this.Potions.Add(itemFactory.CreateConsumableItem(ItemType.PotionHealth, 2));
                this.Potions.Add(itemFactory.CreateConsumableItem(ItemType.PotionHealth, 3));

                this.Scrolls.Add(itemFactory.CreateConsumableItem(ItemType.ScrollDestruction, 1));
                this.Scrolls.Add(itemFactory.CreateConsumableItem(ItemType.ScrollDestruction, 2));
                this.Scrolls.Add(itemFactory.CreateConsumableItem(ItemType.ScrollDestruction, 3));

                this.Scrolls.Add(itemFactory.CreateConsumableItem(ItemType.ScrollGuardian, 1));
                this.Scrolls.Add(itemFactory.CreateConsumableItem(ItemType.ScrollGuardian, 2));
                this.Scrolls.Add(itemFactory.CreateConsumableItem(ItemType.ScrollGuardian, 3));
            }
        }

        public StringBuilder ShowAmounts()
        {
            StringBuilder screen = new StringBuilder();

            screen.AppendLine("Helmets:");
            for (int i = 0; i < this.Helmets.Count; i++)
            {
                screen.AppendLine($"{Helmets[i].Type} level {Helmets[i].Level}");
            }

            screen.AppendLine(Environment.NewLine + "Chests:");
            for (int i = 0; i < this.Chests.Count; i++)
            {
                screen.AppendLine($"{Chests[i].Type} level {Chests[i].Level}");
            }

            screen.AppendLine(Environment.NewLine + "Hands:");
            for (int i = 0; i < this.Hands.Count; i++)
            {
                screen.AppendLine($"{Hands[i].Type} level {Hands[i].Level}");
            }

            screen.AppendLine(Environment.NewLine + "Boots:");
            for (int i = 0; i < this.Boots.Count; i++)
            {
                screen.AppendLine($"{Boots[i].Type} level {Boots[i].Level}");
            }

            screen.AppendLine(Environment.NewLine + "Weapons:");
            for (int i = 0; i < this.Weapons.Count; i++)
            {
                screen.AppendLine($"{Weapons[i].Type} level {Weapons[i].Level}");
            }

            screen.AppendLine(Environment.NewLine + "Potions:");
            for (int i = 0; i < this.Potions.Count; i++)
            {
                screen.AppendLine($"{Potions[i].Type} level {Potions[i].Level}");
            }

            screen.AppendLine(Environment.NewLine + "Scrolls:");
            for (int i = 0; i < this.Scrolls.Count; i++)
            {
                screen.AppendLine($"{Scrolls[i].Type} level {Scrolls[i].Level}");
            }

            return screen;
        }
    }
}
