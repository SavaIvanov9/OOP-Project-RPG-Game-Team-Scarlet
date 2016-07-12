namespace RPG_ConsoleGame.Models.Characters.PlayerControlled
{
    using System;
    using System.Collections.Generic;
    using RPG_ConsoleGame.Characters;
    using Interfaces;
    using Map;
    using Items;
    using Exceptions;

    [Serializable()]
    public class Player : Character, IPlayer
    {
        private int currentRow;
        private int currentCol;
        private bool insideBuilding;
        private char inBuildingSymbol;
        private bool isEnteringBuilding;
       private Dictionary<string, int> effects;

        public Player(Position position, char objectSymbol, string name, PlayerRace race)
            : base(position, objectSymbol, name, 0, 0, 0, 0, 0)
        {
            this.Race = race;
            this.SetPlayerStats();
            this.CurrentCol = currentCol;
            this.CurrentRow = currentRow;
            this.IsEnteringBuilding = isEnteringBuilding;
            SetDefaultEquippedItems();
            this.Effects = new Dictionary<string, int>();
        }

        public Dictionary<string, int> Effects
        {
            get { return this.effects; }
            set { this.effects = value; }
        }
        public int CurrentCol { get; set; }

        public int CurrentRow { get; set; }

        public bool IsEnteringBuilding { get; set; }
       
        public PlayerRace Race { get; private set; }

        public void SetPosition(char[,] matrix)
        {
            for (int row = 0; row < matrix.GetLength(0); row++)
            {
                for (int col = 0; col < matrix.GetLength(1); col++)
                {
                    if (matrix[row, col] == 'P')
                    {
                        this.currentRow = row;
                        this.currentCol = col;
                    }
                }
            }
        }

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

        private void SetDefaultEquippedItems()
        {
            BodyItems.Add(ItemType.Helmet, null);
            BodyItems.Add(ItemType.Chest, null);
            BodyItems.Add(ItemType.Hands, null);
            BodyItems.Add(ItemType.Boots, null);
            BodyItems.Add(ItemType.Weapon, null);
        }

        private void SetBodyItem(IItem item)
        {
            if (!this.BodyItems.ContainsKey(item.Type))
            {
                throw new IncorrectTypeException($"Invalid body item type {item.Type}");
            }

            if (this.BodyItems[item.Type] != null)
            {
                var currItem = this.BodyItems[item.Type];
                this.Inventory.Add(currItem);
                ((INonConsumable)currItem).UnEquipItem(this);
            }

            this.BodyItems[item.Type] = item;
        }

        public override void UseItem(int i)
        {
            if (this.Inventory[i].Type == ItemType.Helmet ||
                this.Inventory[i].Type == ItemType.Chest ||
                this.Inventory[i].Type == ItemType.Hands ||
                this.Inventory[i].Type == ItemType.Boots ||
                this.Inventory[i].Type == ItemType.Weapon)
            {
                SetBodyItem(this.Inventory[i]);
                ((INonConsumable)this.Inventory[i]).UseItem(this);
                this.Inventory.RemoveAt(i);
            }
            else if (this.Inventory[i].Type == ItemType.PotionEnergy ||
                     this.Inventory[i].Type == ItemType.PotionHealth ||
                     this.Inventory[i].Type == ItemType.ScrollGuardian ||
                     this.Inventory[i].Type == ItemType.ScrollDestruction)
            {
                ((IConsumable)this.Inventory[i]).UseItem(this);
                this.Inventory.RemoveAt(i);
            }
        }

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
                    Abilities.Add("Arcane Blast"); //direct dmg
                    Abilities.Add("Hellfire"); //DOT dmg
                    Abilities.Add("Mana Shield"); //shield + some hp
                    Abilities.Add("Recharge energy"); //add mana
                    //passives
                    //stats
                    this.Health = 600;
                    this.Damage = 150;
                    this.Defence = 0;
                    this.Energy = 200;
                    this.Reflexes = 80;
                    break;
                case PlayerRace.Warrior:
                    //abilities
                    Abilities.Add("Slash"); //direct dmg
                    Abilities.Add("Bleeding Wounds"); //dot dmg
                    Abilities.Add("Enrage"); //add dmg
                    Abilities.Add("Regenerate"); //recover some hp + some energy
                    //passeives
                    //stats
                    this.Health = 1500;
                    this.Damage = 50;
                    this.Defence = 0;
                    this.Energy = 100;
                    this.Reflexes = 50;
                    break;
                case PlayerRace.Archer:
                    //abilities
                    Abilities.Add("Heavyshot"); //direct dmg
                    Abilities.Add("Venomous Arrow"); //dot dmg
                    Abilities.Add("Aim"); //add dmg
                    Abilities.Add("Activate Critical Shot"); //add chance to do 1.5x dmg
                    //passives
                    //stats
                    this.Health = 800;
                    this.Damage = 100;
                    this.Defence = 0;
                    this.Energy = 100;
                    this.Reflexes = 70;
                    break;
                case PlayerRace.Rogue:
                    //abilities
                    Abilities.Add("Backstab"); //direct dmg
                    Abilities.Add("SharpenBlades"); //add dmg + energy
                    Abilities.Add("Execute"); //if enemy under 50% hp, deal dmg x3
                    Abilities.Add("Disable"); //enemy deal -30% dmg
                    //passive                   
                    //stats
                    this.Health = 900;
                    this.Damage = 80;
                    this.Defence = 0;
                    this.Energy = 100;
                    this.Reflexes = 70;
                    break;
                case PlayerRace.Paladin:
                    //abilities
                    Abilities.Add("Smite"); //direct dmg
                    Abilities.Add("Righteous Strike"); //direct dmg + energy
                    Abilities.Add("Heal"); //heal
                    Abilities.Add("Divine Shield"); //add a lot hp for 1 turn
                    //passive                   
                    //stats
                    this.Health = 1000;
                    this.Damage = 50;
                    this.Defence = 0;
                    this.Energy = 100;
                    this.Reflexes = 60;
                    break;
                case PlayerRace.Warlock:
                    //abilities
                    Abilities.Add("ShadowBolt"); //direct dmg, -hp, -en
                    Abilities.Add("Shadow Curse"); //dot
                    Abilities.Add("LifeDrain"); //dmg + heal
                    Abilities.Add("LifeTap"); //convert hp to en
                    //passive                   
                    //stats
                    this.Health = 1200;
                    this.Damage = 50;
                    this.Defence = 0;
                    this.Energy = 100;
                    this.Reflexes = 60;
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
