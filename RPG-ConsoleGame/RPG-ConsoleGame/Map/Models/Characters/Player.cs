using RPG_ConsoleGame.Models.Characters.Abilities.Mage;

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
        private Item[] bodyItems = new Item[5];

        private int currentRow = 1;
        private int currentCol = 1;

        public Player(Position position, char objectSymbol, string name, PlayerRace race)
            : base(position, objectSymbol, name, 0, 0, 0)
        {
            this.Race = race;
            
            this.SetPlayerStats();
            this.CurrentCol = currentCol;
            this.CurrentRow = currentRow;
           
        }

        public int CurrentCol { get; set; }
        public int CurrentRow { get; set; }

        public PlayerRace Race { get; private set; }

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

        public void SetBodyItems(Item item)
        {
            switch (item.ItemPossiton)
            {
                case ItemPossition.helmet:
                    ///Adding the item to helmet possition
                    this.bodyItems[0] = item;
                    break;
                case ItemPossition.chest:
                    ///Adding the item to chest possition
                    this.bodyItems[1] = item;
                    break;
                case ItemPossition.hands:
                    ///Adding the item to hand possition
                    this.bodyItems[2] = item;
                    break;
                case ItemPossition.weapon:
                    ///Adding the item to WEAPON possition
                    this.bodyItems[3] = item;
                    break;
                case ItemPossition.boots:
                    ///Adding the item to boots
                    this.bodyItems[4] = item;
                    break;
                case ItemPossition.inventory:
                    this.Inventory.Add(item);
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
                "Player {0} ({1}): Dmg: ({2}), Hp: ({3}), Deff: ({4}) Inventory count: {5}",
                this.Name,
                this.Race,
                this.Damage,
                this.Health,
                this.Defense,
                this.Inventory.Count());
        }

        private void SetPlayerStats()
        {
            /// make it with variable
            switch (this.Race)
            {
                case PlayerRace.Mage:
                    //abilities
                    Abilities.Add("Fireball");
                    Abilities.Add("Hellfire");
                    Abilities.Add("Reflect");
                    //passives
                    this.Damage = 10;
                    this.Health = 100;
                    this.Defense = 10;
                    break;
                case PlayerRace.Warrior:
                    //abilities
                    Abilities.Add("Slash");
                    Abilities.Add("Enrage");
                    Abilities.Add("ShieldWall");
                    //passeives
                    this.Damage = 15;
                    this.Health = 200;
                    this.Defense = 20;
                    break;
                case PlayerRace.Archer:
                    //abilities
                    Abilities.Add("MarkTarget");
                    Abilities.Add("Heavyshot");
                    Abilities.Add("Venomousarrow");
                    //passives
                    this.Damage = 40;
                    this.Health = 130;
                    this.Defense = 15;
                    break;
                case PlayerRace.Rogue:
                    //abilities
                    Abilities.Add("Backstab");
                    Abilities.Add("Ambush");
                    Abilities.Add("Kick");
                    //passive                   
                    this.Damage = 30;
                    this.Health = 150;
                    this.Defense = 10;
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

        private void ItemsStatsToPlayerStat(Item newItem)
        {
            ///write  a logic for thransforming item to play stats
        }
    }
}
