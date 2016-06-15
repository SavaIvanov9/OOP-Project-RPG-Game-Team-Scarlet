namespace RPG_ConsoleGame.Characters
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using Map;
    using Interfaces;
    using Items;

    public class Player : Character, IPlayer
    {
        private List<Item> inventory;
        private Item[] bodyItems = new Item[5];

        private int currentRow = 1;
        private int currentCol = 1;

        public Player(Position position, char objectSymbol, string name, PlayerRace race)
            : base(position, objectSymbol, name, 0, 0, 0)
        {
            this.Race = race;
            this.inventory = new List<Item>();
            this.SetPlayerStats();
            this.CurrentCol = currentCol;
            this.CurrentRow = currentRow;

        }

        public int CurrentCol { get; set; }
        public int CurrentRow { get; set; }

        public PlayerRace Race { get; private set; }

        public IEnumerable<Item> Inventory
        {
            get
            {
                return this.inventory;
            }
        }

        public void Move(char[,] map)
        {
            if (Console.KeyAvailable)
            {
                ConsoleKeyInfo keyPressed = Console.ReadKey(true);
                while (Console.KeyAvailable)
                {
                    Console.ReadKey(true);
                }
                if (keyPressed.Key == ConsoleKey.LeftArrow)
                {
                    MoveLeft(map);
                }
                if (keyPressed.Key == ConsoleKey.RightArrow)
                {
                    MoveRight(map);
                }
                if (keyPressed.Key == ConsoleKey.DownArrow)
                {
                    MoveDown(map);
                }
                if (keyPressed.Key == ConsoleKey.UpArrow)
                {
                    MoveUp(map);
                }
            }
        }

        public void ItemCollect(Item item)
        {

            switch (item.itemposition)
            {
                case Itempossition.helmet:
                    ///Adding the item to helmet possition
                    this.bodyItems[0] = item;
                    break;
                case Itempossition.chest:
                    ///Adding the item to chest possition
                    this.bodyItems[1] = item;
                    break;
                case Itempossition.hands:
                    ///Adding the item to hand possition
                    this.bodyItems[2] = item;
                    break;
                case Itempossition.weapon:
                    ///Adding the item to WEAPON possition
                    this.bodyItems[3] = item;
                    break;
                case Itempossition.boots:
                    ///Adding the item to boots
                    this.bodyItems[4] = item;
                    break;
                case Itempossition.inventory:
                    this.inventory.Add(item);
                    break;
                default:
                    throw new ArgumentException("ItemCollect Method error!");

            }
        }


        //public void Heal()
        //{
        //    var beer = this.inventory.FirstOrDefault() as Beer;

        //    if (beer == null)
        //    {
        //        throw new NotEnoughBeerException("Not enough beer!!!");
        //    }

        //    this.Health += beer.HealthRestore;
        //    this.inventory.Remove(beer);
        //}

        public override string ToString()
        {
            return string.Format(
                "Player {0} ({1}): Damage ({2}), Health ({3}), Number of beers: {4}",
                this.Name,
                this.Race,
                this.Damage,
                this.Health,
                this.Inventory.Count());
        }

        private void SetPlayerStats()
        {
            switch (this.Race)
            {
                case PlayerRace.Mage:
                    this.Damage = 50;
                    this.Health = 100;
                    this.Defence = 10;
                    break;
                case PlayerRace.Warrior:
                    this.Damage = 20;
                    this.Health = 300;
                    this.Defence = 30;
                    break;
                case PlayerRace.Archer:
                    this.Damage = 40;
                    this.Health = 150;
                    this.Defence = 20;
                    break;
                case PlayerRace.Rogue:
                    this.Damage = 30;
                    this.Health = 200;
                    this.Defence = 10;
                    break;
                default:
                    throw new ArgumentException("Unknown player race.");
            }
        }
        private void MoveLeft(char[,] map)
        {
            if ((map[currentRow, currentCol - 1] != '.') &&
                            (map[currentRow, currentCol - 1] != 'w'))
            {
                char previousPosition = 'P';

                map[currentRow, currentCol] = ' ';

                map[currentRow, currentCol - 1] = previousPosition;
                currentCol--;

                position.X = currentRow;
                position.Y = currentCol;
            }
        }
        private void MoveRight(char[,] map)
        {
            if ((map[currentRow, currentCol + 1] != '.') &&
                            (map[currentRow, currentCol + 1] != 'w'))
            {
                char previousPosition = 'P';

                map[currentRow, currentCol] = ' ';

                map[currentRow, currentCol + 1] = previousPosition;
                currentCol++;

                position.X = currentRow;
                position.Y = currentCol;
            }
        }
        private void MoveUp(char[,] map)
        {
            if ((map[currentRow - 1, currentCol] != 'w') &&
                            ((currentRow - 1) > 0))
            {
                char previousPosition = 'P';

                map[currentRow, currentCol] = ' ';

                map[currentRow - 1, currentCol] = previousPosition;
                currentRow--;

                position.X = currentRow;
                position.Y = currentCol;
                //Position = new Position(CurrentRow, currentCol);
            }
        }
        private void MoveDown(char[,] map)
        {
            if ((map[currentRow + 1, currentCol] != 'w') &&
                            map[currentRow + 1, currentCol] != '.')
            {
                char previousPosition = 'P';

                map[currentRow, currentCol] = ' ';

                map[currentRow + 1, currentCol] = previousPosition;
                currentRow++;

                position.X = currentRow;
                position.Y = currentCol;
            }
        }
        private void ItemsStatsToPlayerStat()
        {
            ///write  a logic for thransforming item to play stats
        }
    }
}
