﻿namespace RPG_ConsoleGame.Core.Engines
{
    using System;
    using System.Linq;
    using System.Text;
    using System.Threading;
    using System.Collections.Generic;
    using Models.Characters.PlayerControlled;
    using Models.Items;
    using Sound;
    using Interfaces;
    using Map;
    using UserInterface;
    using System.IO;
    using System.Runtime.Serialization.Formatters.Binary;
    using StateManager;
    using Exceptions;
    using Constants;

    public delegate void OnMenuClickHandler(string selectedValue);

    public class ViewEngine
    {
        private readonly IInputReader reader = new ConsoleInputReader();
        private readonly IRender render = new ConsoleRender();

        public bool InsideGame = false;

        public event OnMenuClickHandler OnMenuClick;

        private void OnClick(string value)
        {
            if (OnMenuClick != null)
            {
                OnMenuClick(value);
            }
        }

        //Singleton pattern
        private static ViewEngine instance;

        public static ViewEngine Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new ViewEngine();
                }

                return instance;
            }
        }

        public void RenderShop(IShop shop, ICharacter character)
        {
            render.Clear();

            StringBuilder screen1 = new StringBuilder();

            screen1.AppendLine(
                "Enter number to make your choice:" + Environment.NewLine + Environment.NewLine +
                "1. Buy item" + Environment.NewLine + Environment.NewLine +
                "2. Sell item" + Environment.NewLine + Environment.NewLine +
                "3. Return back"
                );

            render.PrintScreen(screen1);

            string choice = reader.ReadLine();

            string[] validChoises = { "1", "2", "3" };

            while (!validChoises.Contains(choice))
            {
                render.WriteLine("Invalid choice, please re-enter.");
                choice = reader.ReadLine();
            }

            if (choice == "1")
            {
                RenderBuyItemMenu(shop, character);
            }
            else if (choice == "2")
            {
                SellItemMenu(shop, character);
            }
            else if (choice == "3")
            {
                BackEngine.Instance.StartMusic(SoundEffects.DefaultTheme);
                BackEngine.Instance.StartSinglePlayer();
            }
        }

        private void SellItemMenu(IShop shop, ICharacter character)
        {
            render.Clear();
            StringBuilder screen = new StringBuilder();

            screen.AppendLine(Environment.NewLine + Environment.NewLine +
                $"Your gold: {character.Gold}" + Environment.NewLine +
                "Inventory: " + Environment.NewLine);

            for (int i = 0; i < character.Inventory.Count; i++)
            {
                screen.AppendLine($"{i + 1}. Item {i + 1}: {character.Inventory[i].Type} level: {character.Inventory[i].Level}, price: {character.Inventory[i].Gold}");
            }

            screen.AppendLine("0. Return back" + Environment.NewLine + Environment.NewLine + "Choose number to sell item"
            + Environment.NewLine);

            render.PrintScreen(screen);

            List<string> validChoises2 = new List<string>();

            for (int i = 0; i <= character.Inventory.Count; i++)
            {
                validChoises2.Add(i.ToString());
            }

            string choice = reader.ReadLine();
            while (!validChoises2.Contains(choice))
            {
                render.WriteLine("Invalid choice, please re-enter.");
                choice = reader.ReadLine();
            }

            if (choice == "0")
            {
                //StateManager.Instance.StartState(StateConstants.SinglePlayer);
                RenderShop(shop, character);
            }
            else
            {
                var item = character.Inventory[Convert.ToInt32(choice) - 1];

                shop.TransferItemToShop(character, Convert.ToInt32(choice) - 1);
                render.WriteLine($"item {choice}: {item.Type}" +
                                 $" level {item.Level} sold");

                StartTimer(2);

                SellItemMenu(shop, character);
            }
        }

        private void RenderBuyItemMenu(IShop shop, ICharacter character)
        {
            render.Clear();

            render.PrintScreen(shop.ShowAmounts());

            StringBuilder screen2 = new StringBuilder();

            screen2.AppendLine($"Your gold is: {character.Gold}" + Environment.NewLine +
                "Enter number to make your choice:" + Environment.NewLine +
                $"1. Helmet lvl 1 price: 20 gold, lvl 2 price: 30 gold, lvl 3 price: 40 gold" + Environment.NewLine +
                "2. Chest lvl 1 price: 20 gold, lvl 2 price: 30 gold, lvl 3 price: 40 gold" + Environment.NewLine +
                "3. Hands lvl 1 price: 20 gold, lvl 2 price: 30 gold, lvl 3 price: 40 gold" + Environment.NewLine +
                "4. Weapon lvl 1 price: 20 gold, lvl 2 price: 30 gold, lvl 3 price: 40 gold" + Environment.NewLine +
                "5. Boots lvl 1 price: 20 gold, lvl 2 price: 30 gold, lvl 3 price: 40 gold" + Environment.NewLine +
                "6. Health Potion lvl 1 price: 10 gold, lvl 2 price: 20 gold, lvl 3 price: 30 gold" + Environment.NewLine +
                "7. Energy Potion lvl 1 price: 10 gold, lvl 2 price: 20 gold, lvl 3 price: 30 gold" + Environment.NewLine +
                "8. Guardian Scroll lvl 1 price: 10 gold, lvl 2 price: 20 gold, lvl 3 price: 30 gold" + Environment.NewLine +
                "9. Destruction Scroll lvl 1 price: 10 gold, lvl 2 price: 20 gold, lvl 3 price: 30 gold" + Environment.NewLine +
                "0. Return back"
                );

            render.PrintScreen(screen2);

            string choice = reader.ReadLine();

            string[] validChoises2 = { "0", "1", "2", "3", "4", "5", "6", "7", "8", "9" };

            while (!validChoises2.Contains(choice))
            {
                render.WriteLine("Invalid choice, please re-enter.");
                choice = reader.ReadLine();
            }

            if (choice == "1")
            {
                ChooseLvlItemToBuy(shop, character, ItemType.Helmet);
            }
            else if (choice == "2")
            {
                ChooseLvlItemToBuy(shop, character, ItemType.Hands);
            }
            else if (choice == "3")
            {
                ChooseLvlItemToBuy(shop, character, ItemType.Weapon);
            }
            else if (choice == "4")
            {
                ChooseLvlItemToBuy(shop, character, ItemType.Boots);
            }
            else if (choice == "5")
            {
                ChooseLvlItemToBuy(shop, character, ItemType.PotionHealth);
            }
            else if (choice == "6")
            {
                ChooseLvlItemToBuy(shop, character, ItemType.PotionEnergy);
            }
            else if (choice == "6")
            {
                ChooseLvlItemToBuy(shop, character, ItemType.ScrollDestruction);
            }
            else if (choice == "6")
            {
                ChooseLvlItemToBuy(shop, character, ItemType.ScrollGuardian);
            }
            else if (choice == "6")
            {
                ChooseLvlItemToBuy(shop, character, ItemType.ScrollDestruction);
            }
            else if (choice == "0")
            {
                RenderShop(shop, character);
            }
        }

        private void ChooseLvlItemToBuy(IShop shop, ICharacter character, ItemType type)
        {
            StringBuilder lvlItemScreen = new StringBuilder();

            lvlItemScreen.AppendLine(Environment.NewLine + "Choose level of item:"
                                     + Environment.NewLine + "1. Item level 1"
                                     + Environment.NewLine + "2. Item level 2"
                                     + Environment.NewLine + "3. Item level 3"
                                     + Environment.NewLine + "0. Return back");

            render.PrintScreen(lvlItemScreen);

            string[] validChoises3 = { "0", "1", "2", "3" };

            string choice = reader.ReadLine();

            while (!validChoises3.Contains(choice))
            {
                render.WriteLine("Invalid choice, please re-enter.");
                choice = reader.ReadLine();
            }

            if (choice == "1")
            {
                try
                {
                    shop.TransferItemToCharacter(character, type, 1);
                    render.WriteLine($"You have bought {type} lvl 1");
                }
                catch (Exception ex)
                {
                    render.WriteLine(ex.Message);
                }
                finally
                {
                    StartTimer(3);
                    RenderBuyItemMenu(shop, character);
                }
            }
            else if (choice == "2")
            {
                try
                {
                    shop.TransferItemToCharacter(character, type, 2);
                    render.WriteLine($"You have bought {type} lvl 2");
                }
                catch (Exception ex)
                {
                    render.WriteLine(ex.Message);
                }
                finally
                {
                    StartTimer(3);
                    RenderBuyItemMenu(shop, character);
                }
            }
            else if (choice == "3")
            {
                try
                {
                    shop.TransferItemToCharacter(character, type, 3);
                    render.WriteLine($"You have bought {type} lvl 3");
                }
                catch (Exception ex)
                {
                    render.WriteLine(ex.Message);
                }
                finally
                {
                    StartTimer(3);
                    RenderBuyItemMenu(shop, character);
                }
            }
            else if (choice == "0")
            {
                RenderBuyItemMenu(shop, character);
            }
        }

        public void RenderMenu()
        {
            if (InsideGame == false)
            {
                render.Clear();

                render.WriteLine("");
                StringBuilder screen1 = new StringBuilder();

                screen1.AppendLine(
                    "Enter number to make your choice:" + Environment.NewLine + Environment.NewLine +
                    "1. New Game" + Environment.NewLine + Environment.NewLine +
                    "2. Load Game" + Environment.NewLine + Environment.NewLine +
                    "3. Credits");

                Console.ForegroundColor = ConsoleColor.Cyan;
                render.PrintScreen(screen1);

                string choice = reader.ReadLine();
                render.WriteLine("");

                string[] validChoises = { "1", "2", "3" };

                while (!validChoises.Contains(choice))
                {
                    render.WriteLine("Invalid choice, please re-enter.");
                    choice = reader.ReadLine();
                }

                if (choice == "1")
                {
                    render.Clear();
                    render.WriteLine("");

                    StringBuilder screen2 = new StringBuilder();

                    screen2.AppendLine(
                        "Enter number to make your chaise:" + Environment.NewLine + Environment.NewLine +
                        "1. Single Player" + Environment.NewLine + Environment.NewLine +
                        "2. Multiplayer" + Environment.NewLine + Environment.NewLine +
                        "3. Survival Mode" + Environment.NewLine + Environment.NewLine +
                        "4. Return Back");

                    render.PrintScreen(screen2);
                    choice = reader.ReadLine();

                    string[] validChoises2 = { "1", "2", "3", "4" };

                    while (!validChoises2.Contains(choice))
                    {
                        render.WriteLine("Invalid choice, please re-enter.");
                        choice = reader.ReadLine();
                    }

                    if (choice == "1")
                    {
                        choice = StateConstants.NewSinglePlayer;
                    }
                    if (choice == "2")
                    {
                        choice = StateConstants.NewMultiplayer;
                    }
                    if (choice == "3")
                    {
                        choice = StateConstants.NewSurvival;
                    }
                    if (choice == "4")
                    {
                        RenderMenu();
                    }

                    InsideGame = true;
                }
                else if (choice == "2")
                {
                    //InsideGame = true;
                    choice = StateConstants.LoadGame;
                }
                else if (choice == "3")
                {
                    choice = StateConstants.Credits;
                }

                OnClick(choice);
            }
            else
            {
                render.Clear();

                StringBuilder screen = new StringBuilder();

                screen.AppendLine(
                    "Enter number to make your choice:" + Environment.NewLine + Environment.NewLine +
                    "1. Continue Game" + Environment.NewLine + Environment.NewLine +
                    "2. New Game" + Environment.NewLine + Environment.NewLine +
                    "3. Save Game" + Environment.NewLine + Environment.NewLine +
                    "4. Load Game" + Environment.NewLine + Environment.NewLine +
                    "5. Credits");

                Console.ForegroundColor = ConsoleColor.Cyan;
                render.PrintScreen(screen);
                string choice = reader.ReadLine();
                render.WriteLine("");

                string[] validChoises = { "1", "2", "3", "4", "5" };

                while (!validChoises.Contains(choice))
                {
                    render.WriteLine("Invalid choice, please re-enter.");
                    choice = reader.ReadLine();
                }

                if (choice == "1")
                {
                    choice = StateConstants.SinglePlayer;
                }
                else if (choice == "2")
                {
                    render.Clear();
                    render.WriteLine("");

                    StringBuilder screen2 = new StringBuilder();

                    screen2.AppendLine(
                        "Enter number to make your choice:" + Environment.NewLine + Environment.NewLine +
                        "1. Single Player" + Environment.NewLine + Environment.NewLine +
                        "2. Multiplayer" + Environment.NewLine + Environment.NewLine +
                        "3. Survival Mode" + Environment.NewLine + Environment.NewLine +
                        "4. Return Back");

                    render.PrintScreen(screen2);
                    choice = reader.ReadLine();

                    string[] validChoises2 = { "1", "2", "3", "4" };

                    while (!validChoises2.Contains(choice))
                    {
                        render.WriteLine("Invalid choice, please re-enter.");
                        choice = reader.ReadLine();
                    }

                    if (choice == "1")
                    {
                        choice = StateConstants.NewSinglePlayer;
                    }
                    if (choice == "2")
                    {
                        choice = StateConstants.NewMultiplayer;
                    }
                    if (choice == "3")
                    {
                        choice = StateConstants.NewSurvival;
                    }
                    if (choice == "4")
                    {
                        RenderMenu();
                    }
                }
                else if (choice == "3")
                {
                    choice = StateConstants.SaveGame;
                }
                else if (choice == "4")
                {
                    choice = StateConstants.LoadGame;
                }
                else if (choice == "5")
                {
                    choice = StateConstants.Credits;
                }

                OnClick(choice);
            }
        }

        public string ChooseSavedGameSlot()
        {
            render.Clear();

            StringBuilder screen = new StringBuilder();
            screen.AppendLine("Choose saved game slot:");

            for (int i = 1; i <= 5; i++)
            {
                try
                {
                    FileStream fs = new FileStream($@"..\..\GameSavedData\Save-{i}.xml", FileMode.Open);

                    BinaryFormatter formatter = new BinaryFormatter();

                    IGameDatabase obj = (IGameDatabase)formatter.Deserialize(fs);

                    screen.AppendLine(Environment.NewLine + Environment.NewLine +
                                      $"{i}. Game saved on {obj.Date}");
                    fs.Close();
                }
                catch (Exception e)
                {
                    screen.AppendLine(Environment.NewLine + Environment.NewLine + $"{i}. Free Slot");
                }
            }

            screen.AppendLine(Environment.NewLine + Environment.NewLine + "6. Return To Menu");

            Console.ForegroundColor = ConsoleColor.Cyan;
            render.PrintScreen(screen);

            string choice = reader.ReadLine();

            bool correctDecision = false;
            while (!correctDecision)
            {
                if (choice == "6")
                {
                    //InsideGame = false;
                    choice = "exit";
                    return choice;
                }
                try
                {
                    FileStream fs = new FileStream($@"..\..\GameSavedData\Save-{choice}.xml", FileMode.Open);

                    //BinaryFormatter formatter = new BinaryFormatter();

                    //IGameDatabase obj = (IGameDatabase)formatter.Deserialize(fs);
                    fs.Close();
                    correctDecision = true;

                }
                catch (Exception)
                {
                    render.WriteLine("Invalid choice, please re-enter." + Environment.NewLine);
                    choice = reader.ReadLine();
                }
            }

            return choice;
        }

        public void RenderPlayerStats(IPlayer player)
        {
            render.WriteLine("");
            render.WriteLine(player.ToString());
        }

        public void RenderBattleStats(ICharacter char1, ICharacter char2, StringBuilder history)
        {
            render.Clear();
            StringBuilder screen = new StringBuilder();
            screen.AppendLine();
            screen.AppendLine("Battle has started!!");
            screen.AppendLine();
            screen.AppendLine(new string('-', 60));
            screen.AppendLine();
            screen.AppendLine(char1.ToString());
            screen.AppendLine(char2.ToString());

            screen.AppendLine();
            screen.AppendLine("Choose number to cast ability:");

            for (int i = 0; i < char1.Abilities.Count; i++)
            {
                var ability = char1.Abilities[i];
                screen.AppendLine($"{i + 1} -> {ability}");
            }

            screen.AppendLine();
            screen.Append(history);
            render.PrintScreen(screen);
        }

        public void RenderBattleStatsMultiPlayer(ICharacter char1, ICharacter char2,
            StringBuilder history, int playerOnTurn)
        {
            render.Clear();
            StringBuilder screen = new StringBuilder();

            if (playerOnTurn == 1)
            {
                screen.AppendLine();
                screen.AppendLine("Battle has started!!");
                screen.AppendLine();
                screen.AppendLine(new string('-', 60));
                screen.AppendLine();
                screen.AppendLine("It is " + char1.Name + "'s turn");
                screen.AppendLine();
                //player1 stats
                screen.AppendLine(char1.ToString());

                screen.AppendLine("Choose number to cast ability:");
                screen.AppendLine();
                //player1 abilities
                for (int i = 0; i < char1.Abilities.Count; i++)
                {
                    var ability = char1.Abilities[i];
                    screen.AppendLine($"{i + 1} -> {ability}");
                }
                screen.AppendLine();
                //player2 stats
                screen.AppendLine(char2.ToString());
                //player2 abilities
                for (int i = 0; i < char2.Abilities.Count; i++)
                {
                    var ability = char2.Abilities[i];
                    screen.AppendLine($"{i + 1} -> {ability}");
                }

                screen.AppendLine();
                //screen.AppendLine("Choose number to cast ability:");

                //for (int i = 0; i < char1.Abilities.Count; i++)
                //{
                //    var ability = char1.Abilities[i];
                //    screen.AppendLine($"{i + 1} -> {ability}");
                //}
            }
            else
            {
                screen.AppendLine();
                screen.AppendLine("Battle has started!!");
                screen.AppendLine();
                screen.AppendLine(new string('-', 60));
                screen.AppendLine();
                screen.AppendLine("It is " + char2.Name + "'s turn");
                screen.AppendLine();
                //player1 stats
                screen.AppendLine(char2.ToString());

                screen.AppendLine("Choose number to cast ability:");
                screen.AppendLine();
                //player1 abilities
                for (int i = 0; i < char2.Abilities.Count; i++)
                {
                    var ability = char2.Abilities[i];
                    screen.AppendLine($"{i + 1} -> {ability}");
                }
                screen.AppendLine();
                //player2 stats
                screen.AppendLine(char1.ToString());
                //player2 abilities
                for (int i = 0; i < char1.Abilities.Count; i++)
                {
                    var ability = char1.Abilities[i];
                    screen.AppendLine($"{i + 1} -> {ability}");
                }

                screen.AppendLine();
                //screen.AppendLine("Choose number to cast ability:");

                //for (int i = 0; i < char1.Abilities.Count; i++)
                //{
                //    var ability = char1.Abilities[i];
                //    screen.AppendLine($"{i + 1} -> {ability}");
                //}
            }

            screen.AppendLine();
            screen.AppendLine("Action log:");
            screen.Append(history);
            render.PrintScreen(screen);
        }

        public void RenderMap(char[,] matrix)
        {
            StringBuilder map = new StringBuilder();

            for (int row = 0; row < matrix.GetLength(0); row++)
            {
                for (int col = 0; col < matrix.GetLength(1); col++)
                {

                    if (matrix[row, col] == '-')
                    {
                        map.Append("  ");
                    }
                    else
                    {
                        map.Append($"{matrix[row, col]} ");
                    }
                }

                map.AppendLine();
            }

            Console.ForegroundColor = ConsoleColor.Green;
            render.PrintScreen(map);
        }

        public IPlayer GetPlayer()
        {
            render.Clear();
            render.WriteLine("");
            var playerName = this.GetPlayerName();
            render.WriteLine("");
            PlayerRace race = this.GetPlayerRace();
            IPlayer newPlayer = new Player(new Position(), 'P', playerName, race);
            Console.ForegroundColor = ConsoleColor.Green;

            return newPlayer;
        }

        public IPlayer RegisterPlayerInMulti(int number)
        {
            render.Clear();
            render.WriteLine("");
            var playerName = GetPlayerNameInMulti(number);
            render.WriteLine("");
            PlayerRace race = this.GetPlayerRace();
            IPlayer newPlayer = new Player(new Position(), 'P', playerName, race);
            //Console.ForegroundColor = ConsoleColor.Green;

            return newPlayer;
        }

        private string GetPlayerNameInMulti(int number)
        {
            StringBuilder screen = new StringBuilder();

            screen.AppendLine($"Register Player {number}: ");

            render.PrintScreen(screen);

            return GetPlayerName();
        }

        private PlayerRace GetPlayerRace()
        {
            render.WriteLine(Constants.PlayerRaceDescription + Environment.NewLine);

            string choice = reader.ReadLine();

            string[] validChoises = { "1", "2", "3", "4", "5", "6" };

            while (!validChoises.Contains(choice))
            {
                render.WriteLine("Invalid choice of race, please re-enter.");
                choice = reader.ReadLine();
            }

            PlayerRace race = (PlayerRace)int.Parse(choice);

            return race;
        }

        private string GetPlayerName()
        {
            render.WriteLine("Please enter your name:");

            string playerName = reader.ReadLine();
            //while (string.IsNullOrWhiteSpace(playerName))
            //{
            //    render.WriteLine("Player name cannot be empty. Please re-enter.");
            //    //throw new IncorrectNameException("Player name cannot be empty. Please re-enter.");
            //    playerName = reader.ReadLine();
            //}

            if (string.IsNullOrWhiteSpace(playerName))
            {
                throw new IncorrectNameException("Player name cannot be empty. Please re-enter.");
            }

            return playerName;
        }

        public void RenderCredits()
        {
            render.Clear();

            StringBuilder screen = new StringBuilder();
            screen.AppendLine();
            screen.AppendLine("Teleric Software Academy");
            screen.AppendLine();
            screen.AppendLine("Team Scarlet");
            screen.AppendLine("https://github.com/SavaIvanov9/Skarlet/");
            screen.AppendLine();
            screen.AppendLine("OOP Team Project - C# RPG Game");
            render.PrintScreen(screen);
            StartTimer(5);
        }

        public void RenderWarningScreen(ConsoleColor color, StringBuilder message1, int time, StringBuilder message2 = null)
        {
            render.Clear();

            Console.ForegroundColor = color;
            StringBuilder screen = new StringBuilder();

            screen.AppendLine(
                new string('*',
                (message1.Length > ((message2 != null) ? message2.Length : 0)) ?
                message1.Length : message2.Length));
            screen.AppendLine();
            screen.AppendLine();
            screen.Append(message1 + Environment.NewLine);
            if (message2 != null)
            {
                screen.Append(Environment.NewLine + message2 + Environment.NewLine);

            }
            screen.AppendLine();
            screen.AppendLine();
            screen.AppendLine(
               new string('*',
               (message1.Length > ((message2 != null) ? message2.Length : 0)) ?
               message1.Length : message2.Length));

            render.PrintScreen(screen);

            StartTimer(time);
        }

        public void StartTimer(int seconds)
        {
            for (int i = 0; i < seconds * 40; i++)
            {
                Thread.Sleep(25);

                if (Console.KeyAvailable)
                {
                    if (reader.ReadKey() == "skip")
                    {
                        break;
                    }
                }
            }
        }

        public string ChooseSaveSlot()
        {
            render.Clear();
            StringBuilder screen = new StringBuilder();

            screen.AppendLine("Choose slot to save the game:");

            for (int i = 1; i <= 5; i++)
            {
                try
                {
                    FileStream fs = new FileStream($@"..\..\GameSavedData\Save-{i}.xml", FileMode.Open);

                    BinaryFormatter formatter = new BinaryFormatter();

                    IGameDatabase obj = (IGameDatabase)formatter.Deserialize(fs);

                    screen.AppendLine(Environment.NewLine + Environment.NewLine +
                                      $"{i}. Game saved on {obj.Date}");
                    fs.Close();
                }
                catch (Exception)
                {

                    screen.AppendLine(Environment.NewLine + Environment.NewLine + $"{i}. Free Slot");
                }
                //try
                //{
                //    using (Stream stream = File.Open($@"..\..\GameSavedData\Save-{i}.xml", FileMode.Open))
                //    {
                //        BinaryFormatter formatter = new BinaryFormatter();

                //        IGameDatabase obj = (IGameDatabase)formatter.Deserialize(stream);

                //        screen.AppendLine(Environment.NewLine + Environment.NewLine +
                //                          $"{i}. Game saved on {obj.Date}");
                //    }
                //}
                //catch (Exception)
                //{

                //    screen.AppendLine(Environment.NewLine + Environment.NewLine + $"{i}. Free Slot");
                //}
            }

            screen.AppendLine(Environment.NewLine + Environment.NewLine +
                              "6. Return To Menu");

            Console.ForegroundColor = ConsoleColor.Green;
            render.PrintScreen(screen);

            string choice = reader.ReadLine();

            string[] validChoises = { "1", "2", "3", "4", "5", "6" };
            while (!validChoises.Contains(choice))
            {
                render.WriteLine("Invalid choice, please re-enter.");
                choice = reader.ReadLine();
            }

            if (choice == "6")
            {
                RenderMenu();
            }

            return choice;
        }

        public string LoadSavedGameSlot()
        {
            render.Clear();

            StringBuilder screen = new StringBuilder();
            screen.AppendLine("Choose saved game slot:");

            for (int i = 1; i <= 5; i++)
            {
                try
                {
                    FileStream fs = new FileStream($@"..\..\GameSavedData\Save-{i}.xml", FileMode.Open);

                    BinaryFormatter formatter = new BinaryFormatter();

                    IGameDatabase obj = (IGameDatabase)formatter.Deserialize(fs);

                    screen.AppendLine(Environment.NewLine + Environment.NewLine +
                                      $"{i}. Game saved on {obj.Date}");
                    fs.Close();
                }
                catch (Exception)
                {
                    screen.AppendLine(Environment.NewLine + Environment.NewLine + $"{i}. Free Slot");
                }
            }

            screen.AppendLine(Environment.NewLine + Environment.NewLine + "6. Return To Menu");
            Console.ForegroundColor = ConsoleColor.Cyan;
            render.PrintScreen(screen);

            string choice = reader.ReadLine();

            bool correctDecision = false;
            while (!correctDecision)
            {
                if (choice == "6")
                {
                    InsideGame = false;
                    choice = "exit";
                    correctDecision = true;
                }

                try
                {
                    FileStream fs = new FileStream($@"..\..\GameSavedData\Save-{choice}.xml", FileMode.Open);

                    //BinaryFormatter formatter = new BinaryFormatter();

                    //IGameDatabase obj = (IGameDatabase)formatter.Deserialize(fs);
                    fs.Close();
                    correctDecision = true;

                }
                catch (Exception)
                {
                    render.WriteLine("Invalid choice, please re-enter." + Environment.NewLine);
                    choice = reader.ReadLine();
                }
            }

            return choice;
        }

        public void DisplayMessage(string message)
        {
            render.WriteLine(message);
        }

        public void DisplayMapDescription()
        {
            StringBuilder screen = new StringBuilder();

            screen.AppendLine(Environment.NewLine +
                              "Move with arrow keys" + Environment.NewLine +
                              "P - Player" + Environment.NewLine +
                              "E - Enemy" + Environment.NewLine +
                              "B - Boss" + Environment.NewLine +
                              "S - Shop" + Environment.NewLine +
                              "W - Wall" + Environment.NewLine +
                              "D - Abilities description" + Environment.NewLine +
                              @"Press ""I"" key to open the inventory" + Environment.NewLine +
                              @"Press ""ESC"" key to return to the menu" + Environment.NewLine);
            render.PrintScreen(screen);
        }

        public void RenderInventory(IPlayer character)
        {
            render.Clear();

            StringBuilder screen = new StringBuilder();

            screen.AppendLine("Equipped items: " + Environment.NewLine);

            //for (int i = 0; i < character.BodyItems.Keys.Count; i++)
            //{
            //    screen.AppendLine($"{character.BodyItems.Keys[i]}: " + character.Inventory[i]);
            //}

            foreach (var slot in character.BodyItems)
            {
                if (slot.Value != null)
                {
                    screen.AppendLine($"{slot.Key}: {slot.Value.Type} level {slot.Value.Level}");
                }
                else
                {
                    screen.AppendLine($"{slot.Key}: Empty slot");
                }
            }

            screen.AppendLine(Environment.NewLine +
                $"Your gold: {character.Gold}" + Environment.NewLine +
                Environment.NewLine + "Inventory: " + Environment.NewLine);

            for (int i = 0; i < character.Inventory.Count; i++)
            {
                screen.AppendLine($"{i + 1}. Item {i + 1}: {character.Inventory[i].Type} level {character.Inventory[i].Level}");
            }

            screen.AppendLine("0. Return back" + Environment.NewLine + Environment.NewLine + "Choose number to use item"
                + Environment.NewLine);

            render.PrintScreen(screen);
            RenderPlayerStats(character as IPlayer);

            List<string> validChoises = new List<string>();


            for (int i = 0; i <= character.Inventory.Count; i++)
            {
                validChoises.Add(i.ToString());
            }


            string choice = reader.ReadLine();
            while (!validChoises.Contains(choice))
            {
                render.WriteLine("Invalid choice, please re-enter.");
                choice = reader.ReadLine();
            }

            //while (true)
            //{
            if (choice == "0")
            {
                StateManager.Instance.StartState(StateConstants.SinglePlayer);
            }
            else
            {
                var item = character.Inventory[Convert.ToInt32(choice) - 1];

                character.UseItem(Convert.ToInt32(choice) - 1);

                render.WriteLine($"item {choice}: {item.Type}" +
                                 $" level {item.Level} used");

                StartTimer(2);

                RenderInventory(character);
                //validChoises.Clear();
                //validChoises.Add("0");
                //if (character.Inventory.Count > 0)
                //{
                //    for (int i = 0; i <= character.Inventory.Count; i++)
                //    {
                //        validChoises.Add((i + 1).ToString());
                //    }
                //}

                //choice = reader.ReadLine();

                //while (!validChoises.Contains(choice))
                //{
                //    render.WriteLine("Invalid choice, please re-enter.");
                //    choice = reader.ReadLine();
                //}
            }

        }
    }
}
