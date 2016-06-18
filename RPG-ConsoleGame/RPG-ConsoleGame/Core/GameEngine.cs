using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Xml.Linq;
using RPG_ConsoleGame.Characters;
using RPG_ConsoleGame.Core;
using RPG_ConsoleGame.Core.Factories;
using RPG_ConsoleGame.Interfaces;
using RPG_ConsoleGame.Map;
using RPG_ConsoleGame.Models.Characters.Abilities.Mage;
using RPG_ConsoleGame.UserInterface;

namespace RPG_ConsoleGame.Engine
{
    public class GameEngine
    {
        private readonly IInputReader reader = new ConsoleInputReader();
        private readonly IRender render = new ConsoleRender();
        private readonly IPlayerFactory playerFactory = new PlayerFactory();
        private readonly IBotFactory botFactory = new BotFactory();
        private readonly IGameDatabase database = new GameDatabase();
        private readonly IAbilitiesProcessor abilitiesProcessor = new AbilitiesProcessor();

        public bool IsRunning { get; private set; }

        static Map.Map mapMatrix = new Map.Map();

        char[,] map = mapMatrix.ReadMap("../../../Map1.txt");

        static Position plPos = new Position();

        //private IInputReader reader;
        //private IRender render;

        //public GameEngine(IInputReader reader, IRender render)
        //{
        //    this.reader = reader;
        //    this.render = render;
        //    //this.characters = new List<GameObject>();
        //    //this.items = new List<GameObject>();
        //}

        private static GameEngine instance;

        //Singleton patern
        public static GameEngine Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new GameEngine();
                }

                return instance;
            }
        }

        public void Run()
        {
            var playerName = this.GetPlayerName();
            PlayerRace race = this.GetPlayerRace();
            Player newPlayer = new Player(plPos, 'P', playerName, race);

            database.Players.Add(newPlayer);

            database.AddBot(botFactory.CreateBot(new Position(2, 7), 'E', "demon", PlayerRace.Mage));
            database.AddPlayer(playerFactory.CreateHuman(new Position(5, 5), 'A', "Go6o", PlayerRace.Mage));
            
            //Using ability
            //abilitiesProcessor.ProcessCommand(database.Players[0].Abilities[0], database.Bots[0]);


            this.IsRunning = true;
            while (this.IsRunning)
            {
                if (Console.KeyAvailable)
                {
                    Console.Clear();

                    database.Players[0].Move(map);

                    PrintMap(map, plPos.X, plPos.Y);
                    PrintPlayerStats(database.Players[0]);
                    if (database.Bots.Count > 0)
                    {
                        CheckForBattle(database.Players[0], database.Bots[0]);
                    }
                    
                    RemoveDead(database);
                }

                
            }
        }

        //string command = this.reader.ReadLine();

        //try
        //{
        //    this.ExecuteCommand(command);
        //}
        //catch (ObjectOutOfBoundsException ex)
        //{
        //    this.renderer.WriteLine(ex.Message);
        //}
        //catch (NotEnoughBeerException ex)
        //{
        //    this.renderer.WriteLine(ex.Message);
        //}
        //catch (Exception ex)
        //{
        //    this.renderer.WriteLine(ex.Message);
        //}

        //if (this.characters.Count == 0)
        //{
        //    this.IsRunning = false;
        //    this.renderer.WriteLine("Valar morgulis!");
        //}

        private void CheckForBattle(ICharacter char1, ICharacter char2)
        {
            if (char1.Position.X == char2.Position.X && char1.Position.Y == char2.Position.Y)
            {
                //Console.CursorVisible = true;
                render.Clear();

                StringBuilder screen = new StringBuilder();
                screen.AppendLine();
                screen.AppendLine("You have entered in battle!!");
                screen.AppendLine();
                screen.AppendLine(new string('-', 60));
                screen.AppendLine();
                screen.AppendLine(char1.ToString());
                screen.AppendLine(char2.ToString());

                //StringBuilder screen2 = new StringBuilder();
                screen.AppendLine();
                screen.AppendLine("Choose number to cast ability:");

                for (int i = 0; i < char1.Abilities.Count; i++)
                {
                    var ability = char1.Abilities[i];
                    screen.AppendLine(string.Format("{0} -> {1}", i + 1, ability));
                }

                //string choice = "";
                //screen.AppendLine("");
                //screen.AppendLine("Chosen ability: " + choice);
                //screen.AppendLine("");

                StringBuilder history = new StringBuilder();

                render.PrintScreen(screen);
                
                ConsoleKeyInfo keyPressed = Console.ReadKey(true);

                //while (Console.KeyAvailable)
                //{
                //    Console.ReadKey(true);
                //}
                
                if (keyPressed.Key == ConsoleKey.D1)
                {
                    //render.Clear();
                   abilitiesProcessor.ProcessCommand(char1.Abilities[0], char1, char2);

                    render.WriteLine("");
                    render.WriteLine("Chosen ability: " + char1.Abilities[2]);
                    render.WriteLine("");

                    render.Clear();

                    render.WriteLine("");
                    render.WriteLine("You have entered in battle!!");
                    render.WriteLine("");
                    render.WriteLine(new string('-', 60));
                    render.WriteLine("");
                    render.WriteLine(char1.ToString());
                    render.WriteLine(char2.ToString());

                    render.WriteLine("");
                    render.WriteLine("Choose number to cast ability:");
                    for (int i = 0; i < char1.Abilities.Count; i++)
                    {
                        var ability = char1.Abilities[i];
                        render.WriteLine(string.Format("{0} -> {1}", i + 1, ability));
                    }

                    //screen.AppendLine();
                    //screen.AppendLine("You have entered in battle!!");
                    //screen.AppendLine();
                    //screen.AppendLine(new string('-', 60));
                    //screen.AppendLine();
                    //screen.AppendLine(char1.ToString());
                    //screen.AppendLine(char2.ToString());

                    //screen.Clear();

                    //screen.AppendLine(char1.ToString());
                    //screen.AppendLine(char2.ToString());
                    //screen.AppendLine();
                    //screen.AppendLine("Choose number to cast ability:");
                    //for (int i = 0; i < char1.Abilities.Count; i++)
                    //{
                    //    var ability = char1.Abilities[i];
                    //    screen.AppendLine(string.Format("{0} -> {1}", i + 1, ability));
                    //}

                }
                if (keyPressed.Key == ConsoleKey.D2)
                {
                    abilitiesProcessor.ProcessCommand(char1.Abilities[2], char1, char2);

                    //screen.AppendLine();
                    //screen.AppendLine("Chosen ability: " + char1.Abilities[1]);
                    //screen.AppendLine();

                    render.WriteLine("");
                    render.WriteLine("Chosen ability: " + char1.Abilities[1]);
                    render.WriteLine("");

                    render.Clear();

                    render.WriteLine("");
                    render.WriteLine("You have entered in battle!!");
                    render.WriteLine("");
                    render.WriteLine(new string('-', 60));
                    render.WriteLine("");
                    render.WriteLine(char1.ToString());
                    render.WriteLine(char2.ToString());

                    render.WriteLine("");
                    render.WriteLine("Choose number to cast ability:");
                    for (int i = 0; i < char1.Abilities.Count; i++)
                    {
                        var ability = char1.Abilities[i];
                        render.WriteLine(string.Format("{0} -> {1}", i + 1, ability));
                    }

                    ////screen.Clear();

                    //screen.AppendLine(char1.ToString());
                    //screen.AppendLine(char2.ToString());
                    //screen.AppendLine();
                    //screen.AppendLine("Choose number to cast ability:");
                    //for (int i = 0; i < char1.Abilities.Count; i++)
                    //{
                    //    var ability = char1.Abilities[i];
                    //    screen.AppendLine(string.Format("{0} -> {1}", i + 1, ability));
                    //}
                }
                if (keyPressed.Key == ConsoleKey.D3)
                {
                    abilitiesProcessor.ProcessCommand(char1.Abilities[2], char1, char2);
                    //render.WriteLine("");
                    //render.WriteLine("Chosen ability: " + char1.Abilities[2]);
                    //render.WriteLine("");

                    render.WriteLine("");
                    render.WriteLine("Chosen ability: " + char1.Abilities[2]);
                    render.WriteLine("");

                    render.Clear();

                    render.WriteLine("");
                    render.WriteLine("You have entered in battle!!");
                    render.WriteLine("");
                    render.WriteLine(new string('-', 60));
                    render.WriteLine("");
                    render.WriteLine(char1.ToString());
                    render.WriteLine(char2.ToString());

                    render.WriteLine("");
                    render.WriteLine("Choose number to cast ability:");
                    for (int i = 0; i < char1.Abilities.Count; i++)
                    {
                        var ability = char1.Abilities[i];
                        render.WriteLine(string.Format("{0} -> {1}", i + 1, ability));
                    }
                    //Console.WriteLine("kur");
                    //choice = char1.Abilities[2];


                    render.PrintScreen(history);

                    //screen.Clear();

                    //screen.AppendLine(char1.ToString());
                    //screen.AppendLine(char2.ToString());
                    //screen.AppendLine();
                    //screen.AppendLine("Choose number to cast ability:");
                    //for (int i = 0; i < char1.Abilities.Count; i++)
                    //{
                    //    var ability = char1.Abilities[i];
                    //    screen.AppendLine(string.Format("{0} -> {1}", i + 1, ability));
                    //}
                }
                if (keyPressed.Key == ConsoleKey.D4)
                {
                    //render.WriteLine("Chosen ability: {0}", char1.Abilities[3]);
                    //render.WriteLine("");
                    abilitiesProcessor.ProcessCommand(char1.Abilities[2], char1, char2);

                    render.WriteLine("");
                    render.WriteLine("Chosen ability: " + char1.Abilities[2]);
                    render.WriteLine("");

                    render.Clear();

                    render.WriteLine("");
                    render.WriteLine("You have entered in battle!!");
                    render.WriteLine("");
                    render.WriteLine(new string('-', 60));
                    render.WriteLine("");
                    render.WriteLine(char1.ToString());
                    render.WriteLine(char2.ToString());

                    render.WriteLine("");
                    render.WriteLine("Choose number to cast ability:");
                    for (int i = 0; i < char1.Abilities.Count; i++)
                    {
                        var ability = char1.Abilities[i];
                        render.WriteLine(string.Format("{0} -> {1}", i + 1, ability));
                    }
                    //screen.Clear();

                    //screen.AppendLine(char1.ToString());
                    //screen.AppendLine(char2.ToString());
                    //screen.AppendLine();
                    //screen.AppendLine("Choose number to cast ability:");
                    //for (int i = 0; i < char1.Abilities.Count; i++)
                    //{
                    //    var ability = char1.Abilities[i];
                    //    screen.AppendLine(string.Format("{0} -> {1}", i + 1, ability));
                    //}
                }

                //render.PrintScreen(history);

                if (char2.Health <= 0)
                {
                    render.WriteLine("You have killed the enemy!!");
                    //database.Bots.Remove((IBot)char2);
                }
            }
        }

        //static void PrintMap(char[,] matrix, int currentRow, int currentCol)
        //{
        //    for (int row = 0; row < matrix.GetLength(0); row++)
        //    {
        //        for (int col = 0; col < matrix.GetLength(1); col++)
        //        {
        //            if (matrix[row, col] == '.')
        //            {
        //                Console.Write(". ");
        //            }
        //            else if ((row >= currentRow - 5) && (row <= currentRow + 5) &&
        //                (col >= currentCol - 5) && (col <= currentCol + 5))
        //            {

        //                if (matrix[row, col] == '-')
        //                {
        //                    Console.Write("  ");
        //                }
        //                else
        //                {
        //                    Console.Write("{0} ", matrix[row, col]);
        //                }
        //            }
        //            else
        //            {
        //                Console.Write("  ");
        //            }
        //        }

        //        Console.WriteLine();
        //    }

        static void PrintMap(char[,] matrix, int currentRow, int currentCol)
        {
            for (int row = 0; row < matrix.GetLength(0); row++)
            {
                for (int col = 0; col < matrix.GetLength(1); col++)
                {

                    if (matrix[row, col] == '-')
                    {
                        Console.Write("  ");
                    }
                    else
                    {
                        Console.Write("{0} ", matrix[row, col]);
                    }
                }

                Console.WriteLine();
            }
        }

        private void PrintPlayerStats(IPlayer player)
        {
            render.WriteLine("");
            render.WriteLine(player.ToString());
        }

        private PlayerRace GetPlayerRace()
        {
            render.WriteLine("Choose a race:");
            render.WriteLine("1. Mage (damage: 50, health: 100)");
            render.WriteLine("2. Warrior (damage: 20, health: 300)");
            render.WriteLine("3. Archer (damage: 40, health: 150)");
            render.WriteLine("4. Rogue (damage: 30, health: 200)");

            string choice = reader.ReadLine();

            string[] validChoises = { "1", "2", "3", "4" };

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
            while (string.IsNullOrWhiteSpace(playerName))
            {
                render.WriteLine("Player name cannot be empty. Please re-enter.");
                playerName = reader.ReadLine();
            }

            return playerName;
        }

        private void RemoveDead(IGameDatabase database)
        {
            for (int index = 0; index < database.Bots.Count; index++)
            {
                var bot = database.Bots[index];
                if (bot.Health <= 0)
                {
                    database.Bots.Remove(bot);
                }
            }
        }
    }
}

