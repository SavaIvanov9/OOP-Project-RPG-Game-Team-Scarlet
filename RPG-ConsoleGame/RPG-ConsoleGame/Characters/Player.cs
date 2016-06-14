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
        private readonly List<Item> inventory;

        public int currentRow = 1;
        public int currentCol = 1;

        public Player(Position position, char objectSymbol, string name, PlayerClass race)
            : base(position, objectSymbol, name, 0, 0)
        {
            this.Race = race;
            this.inventory = new List<Item>();
            this.SetPlayerStats();
            this.CurrentCol = currentCol;
            this.CurrentRow = currentRow;
        }

        public int CurrentCol { get; set; }
        public int CurrentRow { get; set; }

        public PlayerClass Race { get; private set; }

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
                    if ((map[currentRow, currentCol - 1] != '.') &&
                        (map[currentRow, currentCol - 1] != 'w'))
                    {
                        char previousPosition = 'p';

                        map[currentRow, currentCol] = ' ';

                        map[currentRow, currentCol - 1] = previousPosition;
                        currentCol--;
                    }

                }
                if (keyPressed.Key == ConsoleKey.RightArrow)
                {
                    if ((map[currentRow, currentCol + 1] != '.') &&
                        (map[currentRow, currentCol + 1] != 'w'))
                    {
                        char previousPosition = 'p';

                        map[currentRow, currentCol] = ' ';

                        map[currentRow, currentCol + 1] = previousPosition;
                        currentCol++;
                    }

                }
                if (keyPressed.Key == ConsoleKey.DownArrow)
                {
                    if ((map[currentRow + 1, currentCol] != 'w') &&
                        map[currentRow + 1, currentCol] != '.')

                    {
                        char previousPosition = 'p';

                        map[currentRow, currentCol] = ' ';

                        map[currentRow + 1, currentCol] = previousPosition;
                        currentRow++;
                    }
                }
                if (keyPressed.Key == ConsoleKey.UpArrow)
                {
                    if ((map[currentRow - 1, currentCol] != 'w') &&
                        ((currentRow - 1) > 0))
                    {
                        char previousPosition = 'p';

                        map[currentRow, currentCol] = ' ';

                        map[currentRow - 1, currentCol] = previousPosition;
                        currentRow--;
                    }
                }
            }
        }

        public void AddItemToInventory(Item item)
        {
            this.inventory.Add(item);
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
                case PlayerClass.Mage:
                    this.Damage = 50;
                    this.Health = 100;
                    break;
                case PlayerClass.Warrior:
                    this.Damage = 20;
                    this.Health = 300;
                    break;
                case PlayerClass.Archer:
                    this.Damage = 40;
                    this.Health = 150;
                    break;
                case PlayerClass.Rogue:
                    this.Damage = 30;
                    this.Health = 200;
                    break;
                default:
                    throw new ArgumentException("Unknown player race.");
            }
        }
    }
}
