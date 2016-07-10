using RPG_ConsoleGame.Exceptions;
using RPG_ConsoleGame.Models.Items.Consumables;

namespace RPG_ConsoleGame.Models.Characters.PlayerControlled
{
    using System;
    using RPG_ConsoleGame.Characters;
    using Interfaces;
    using Map;
    using Items;

    [Serializable()]
    public class Player : Character, IPlayer
    {
        private IItem[] bodyItems = new IItem[5];

        private int currentRow = 1;
        private int currentCol = 1;
        private bool insideBuilding = false;
        private char inBuildingSymbol;

        public Player(Position position, char objectSymbol, string name, PlayerRace race)
            : base(position, objectSymbol, name, 0, 0, 0, 0, 0)
        {
            this.Race = race;
            this.SetPlayerStats();
            this.CurrentCol = currentCol;
            this.CurrentRow = currentRow;

        }

        public int CurrentCol { get; set; }
        public int CurrentRow { get; set; }

        public PlayerRace Race { get; private set; }

        public void Move(char[,] map, string command)
        {
            if (command == "moveLeft")
            {
                MoveLeft(map);
            }
            if (command == "moveRight")
            {
                MoveRight(map);
            }
            if (command == "moveDown")
            {
                MoveDown(map);
            }
            if (command == "moveUp")
            {
                MoveUp(map);
            }
        }

        private void SetBodyItem(IItem item)
        {
            switch (item.Type)
            {
                case ItemType.Helmet:
                    //Adding the item to helmet possition
                    this.bodyItems[0] = item;
                    break;
                case ItemType.Chest:
                    //Adding the item to chest possition
                    this.bodyItems[1] = item;
                    break;
                case ItemType.Hands:
                    //Adding the item to hand possition
                    this.bodyItems[2] = item;
                    break;
                case ItemType.Boots:
                    //Adding the item to boots
                    this.bodyItems[3] = item;
                    break;
                case ItemType.Weapon:
                    //Adding the item to WEAPON possition
                    this.bodyItems[4] = item;
                    break;
                //case ItemType.Bag:
                //    this.Inventory.Add(item);
                //    break;
                default:
                    throw new ArgumentException("ItemCollect Method error!");

            }
        }

        public void UseItem(int i)
        {
            if (this.Inventory[i].Type == ItemType.Helmet ||
                this.Inventory[i].Type == ItemType.Chest ||
                this.Inventory[i].Type == ItemType.Hands ||
                this.Inventory[i].Type == ItemType.Boots ||
                this.Inventory[i].Type == ItemType.Weapon)
            {
                SetBodyItem(this.Inventory[i]);
                this.Inventory[i].UseItem(this.Health, this.Damage, this.Defence, this.Energy, this.Reflexes);
                this.Inventory.RemoveAt(i);
            }
            else if(this.Inventory[i].Type == ItemType.PotionEnergy ||
                this.Inventory[i].Type == ItemType.PotionHealth ||
                this.Inventory[i].Type == ItemType.ScrollGuardian ||
                    this.Inventory[i].Type == ItemType.ScrollDestruction)
            {
                this.Inventory[i].UseItem(this.Health, this.Damage, this.Defence, this.Energy, this.Reflexes);
                this.Inventory.RemoveAt(i);
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
                "Player {0} ({1}): Health ({2}),  Damage ({3}), Defence({4}), Energy ({5}), Reflexes: ({6})",
                this.Name,
                this.Race,
                this.Health,
                this.Damage,
                this.Defence,
                this.Energy,
                this.Reflexes);
        }

        private void SetPlayerStats()
        {
            switch (this.Race)
            {
                case PlayerRace.Mage:
                    //abilities
                    Abilities.Add("Fireball");
                    Abilities.Add("Hellfire");
                    Abilities.Add("Reflect");
                    //passives
                    //stats
                    this.Health = 600;
                    this.Damage = 100;
                    this.Defence = 10;
                    this.Energy = 100;
                    this.Reflexes = 50;
                    break;
                case PlayerRace.Warrior:
                    //abilities
                    Abilities.Add("Slash");
                    Abilities.Add("Enrage");
                    Abilities.Add("ShieldWall");
                    //passeives
                    //stats
                    this.Health = 800;
                    this.Damage = 50;
                    this.Defence = 10;
                    this.Energy = 100;
                    this.Reflexes = 60;
                    break;
                case PlayerRace.Archer:
                    //abilities
                    Abilities.Add("MarkTarget");
                    Abilities.Add("Heavyshot");
                    Abilities.Add("Venomousarrow");
                    //passives
                    //stats
                    this.Health = 500;
                    this.Damage = 100;
                    this.Defence = 10;
                    this.Energy = 100;
                    this.Reflexes = 70;
                    break;
                case PlayerRace.Rogue:
                    //abilities
                    Abilities.Add("Backstab");
                    Abilities.Add("SharpenBlades");
                    Abilities.Add("Execute");
                    //passive                   
                    //stats
                    this.Health = 600;
                    this.Damage = 90;
                    this.Defence = 10;
                    this.Energy = 100;
                    this.Reflexes = 100;
                    break;
                case PlayerRace.Paladin:
                    //abilities
                    Abilities.Add("Smite");
                    Abilities.Add("Exorcism");
                    Abilities.Add("Heal");
                    //passive                   
                    //stats
                    this.Health = 800;
                    this.Damage = 50;
                    this.Defence = 10;
                    this.Energy = 100;
                    this.Reflexes = 60;
                    break;
                case PlayerRace.Warlock:
                    //abilities
                    Abilities.Add("LifeDrain");
                    Abilities.Add("LifeTap");
                    Abilities.Add("ShadowBolt");
                    //passive                   
                    //stats
                    this.Health = 500;
                    this.Damage = 100;
                    this.Defence = 10;
                    this.Energy = 100;
                    this.Reflexes = 50;
                    break;

                default:
                    throw new IncorrectRaceException("Invalid character race.");
            }
        }

        private void MoveLeft(char[,] map)
        {
            if (insideBuilding)
            {
                if ((map[currentRow, currentCol - 1] != '.') &&
                          (map[currentRow, currentCol - 1] != 'w'))
                {
                    char previousPosition = 'P';

                    map[currentRow, currentCol] = inBuildingSymbol;
                    this.insideBuilding = false;

                    map[currentRow, currentCol - 1] = previousPosition;
                    currentCol--;

                    position.X = currentRow;
                    position.Y = currentCol;
                }
            }
            else
            {
                if ((map[currentRow, currentCol - 1] != '.') &&
                          (map[currentRow, currentCol - 1] != 'w'))
                {
                    char previousPosition = 'P';

                    if (map[currentRow, currentCol - 1] == 'S')
                    {
                        this.inBuildingSymbol = 'S';
                        this.insideBuilding = true;
                    }
                    else if (map[currentRow, currentCol - 1] == 'F')
                    {
                        this.inBuildingSymbol = 'F';
                        this.insideBuilding = true;
                    }

                    map[currentRow, currentCol] = ' ';

                    map[currentRow, currentCol - 1] = previousPosition;
                    currentCol--;

                    position.X = currentRow;
                    position.Y = currentCol;
                }
            }

        }

        private void MoveRight(char[,] map)
        {
            if (insideBuilding)
            {
                if ((map[currentRow, currentCol + 1] != '.') &&
                           (map[currentRow, currentCol + 1] != 'w'))
                {
                    char previousPosition = 'P';

                    map[currentRow, currentCol] = inBuildingSymbol;
                    this.insideBuilding = false;

                    map[currentRow, currentCol + 1] = previousPosition;
                    currentCol++;

                    position.X = currentRow;
                    position.Y = currentCol;
                }
            }
            else
            {
                if ((map[currentRow, currentCol + 1] != '.') &&
                           (map[currentRow, currentCol + 1] != 'w'))
                {
                    char previousPosition = 'P';

                    if (map[currentRow, currentCol + 1] == 'S')
                    {
                        this.inBuildingSymbol = 'S';
                        this.insideBuilding = true;
                    }
                    else if (map[currentRow, currentCol + 1] == 'F')
                    {
                        this.inBuildingSymbol = 'F';
                        this.insideBuilding = true;
                    }

                    map[currentRow, currentCol] = ' ';

                    map[currentRow, currentCol + 1] = previousPosition;
                    currentCol++;

                    position.X = currentRow;
                    position.Y = currentCol;
                }
            }

        }

        private void MoveUp(char[,] map)
        {
            if (insideBuilding)
            {
                if ((map[currentRow - 1, currentCol] != 'w') &&
                            ((currentRow - 1) > 0))
                {
                    char previousPosition = 'P';

                    map[currentRow, currentCol] = inBuildingSymbol;
                    this.insideBuilding = false;

                    map[currentRow - 1, currentCol] = previousPosition;
                    currentRow--;

                    position.X = currentRow;
                    position.Y = currentCol;
                    //Position = new Position(CurrentRow, currentCol);
                }
            }
            else
            {
                if ((map[currentRow - 1, currentCol] != 'w') &&
                            ((currentRow - 1) > 0))
                {
                    char previousPosition = 'P';

                    if (map[currentRow - 1, currentCol] == 'S')
                    {
                        this.inBuildingSymbol = 'S';
                        this.insideBuilding = true;
                    }
                    else if (map[currentRow - 1, currentCol] == 'F')
                    {
                        this.inBuildingSymbol = 'F';
                        this.insideBuilding = true;
                    }

                    map[currentRow, currentCol] = ' ';

                    map[currentRow - 1, currentCol] = previousPosition;
                    currentRow--;

                    position.X = currentRow;
                    position.Y = currentCol;
                    //Position = new Position(CurrentRow, currentCol);
                }
            }

        }

        private void MoveDown(char[,] map)
        {
            if (insideBuilding)
            {
                if ((map[currentRow + 1, currentCol] != 'w') &&
                            map[currentRow + 1, currentCol] != '.')
                {
                    char previousPosition = 'P';

                    map[currentRow, currentCol] = inBuildingSymbol;
                    this.insideBuilding = false;
                    map[currentRow + 1, currentCol] = previousPosition;
                    currentRow++;

                    position.X = currentRow;
                    position.Y = currentCol;
                }
            }
            else
            {
                if ((map[currentRow + 1, currentCol] != 'w') &&
                            map[currentRow + 1, currentCol] != '.')
                {
                    char previousPosition = 'P';

                    if (map[currentRow + 1, currentCol] == 'S')
                    {
                        this.inBuildingSymbol = 'S';
                        this.insideBuilding = true;
                    }
                    else if (map[currentRow + 1, currentCol] == 'F')
                    {
                        this.inBuildingSymbol = 'F';
                        this.insideBuilding = true;
                    }

                    map[currentRow, currentCol] = ' ';

                    map[currentRow + 1, currentCol] = previousPosition;
                    currentRow++;

                    position.X = currentRow;
                    position.Y = currentCol;
                }
            }
            //if ((map[currentRow + 1, currentCol] != 'w') &&
            //                map[currentRow + 1, currentCol] != '.')
            //{
            //    char previousPosition = 'P';

            //    map[currentRow, currentCol] = ' ';

            //    map[currentRow + 1, currentCol] = previousPosition;
            //    currentRow++;

            //    position.X = currentRow;
            //    position.Y = currentCol;
            //}
        }

    }
}
