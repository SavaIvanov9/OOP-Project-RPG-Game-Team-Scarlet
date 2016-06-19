namespace RPG_ConsoleGame.Engine
{
    using System;
    using System.Linq;
    using System.Text;
    using Characters;
    using Core;
    using Core.Factories;
    using Interfaces;
    using Map;
    using Models.Characters.Abilities.Mage;
    using Sound;
    using UserInterface;

    public class GameEngine
    {
        private readonly IInputReader reader = new ConsoleInputReader();
        private readonly IRender render = new ConsoleRender();
        private readonly IPlayerFactory playerFactory = new PlayerFactory();
        private readonly IBotFactory botFactory = new BotFactory();
        private readonly IGameDatabase database = new GameDatabase();
        private readonly IAbilitiesProcessor abilitiesProcessor = new AbilitiesProcessor();

        public bool IsRunning { get; private set; }

        static Map mapMatrix = new Map();

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
            Console.CursorVisible = false;

            Sound.SFX(SoundEffects.DefaultTheme);

            var playerName = this.GetPlayerName();
            PlayerRace race = this.GetPlayerRace();
            Player newPlayer = new Player(plPos, 'P', playerName, race);

            database.Players.Add(newPlayer);

            database.AddBot(botFactory.CreateBot(new Position(2, 7), 'E', "demon", PlayerRace.Mage));
            database.AddPlayer(playerFactory.CreateHuman(new Position(5, 5), 'A', "Go6o", PlayerRace.Mage));

            //Using ability
            //abilitiesProcessor.ProcessCommand(database.Players[0].Abilities[0], database.Bots[0]);

            this.IsRunning = true;

            Console.Clear();

            PrintMap(map);
            PrintPlayerStats(database.Players[0]);

            while (this.IsRunning)
            {
                if (Console.KeyAvailable)
                {
                    Console.Clear();

                    database.Players[0].Move(map);

                    PrintMap(map);
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
                var isInBattle = true;
                var history = new StringBuilder();
                var turnsCount = 0;
                while (isInBattle)
                {

                    render.Clear();

                    var screen = RenderBattleStats(char1, char2, history);

                    //string choice = "";
                    //screen.AppendLine("");
                    //screen.AppendLine("Chosen ability: " + choice);
                    //screen.AppendLine("");

                    render.PrintScreen(screen);

                    if (char1.Reflexes >= char2.Reflexes)
                    {
                        ConsoleKeyInfo keyPressed = Console.ReadKey(true);


                        //while (Console.KeyAvailable)
                        //{
                        //    Console.ReadKey(true);
                        //}

                        if (keyPressed.Key == ConsoleKey.D1)
                        {
                            turnsCount++;
                            RenderBattleAbility(char1.Abilities[0], char1, char2, turnsCount, history);

                            //bot AI in action
                            ExecuteBotDecision(turnsCount, char2, char1, history);
                        }
                        if (keyPressed.Key == ConsoleKey.D2)
                        {
                            turnsCount++;
                            RenderBattleAbility(char1.Abilities[1], char1, char2, turnsCount, history);

                            //bot AI in action
                            ExecuteBotDecision(turnsCount, char2, char1, history);
                        }
                        if (keyPressed.Key == ConsoleKey.D3)
                        {
                            turnsCount++;
                            RenderBattleAbility(char1.Abilities[2], char1, char2, turnsCount, history);

                            //bot AI in action
                            ExecuteBotDecision(turnsCount, char2, char1, history);
                        }
                        if (keyPressed.Key == ConsoleKey.D4)
                        {
                            turnsCount++;
                            RenderBattleAbility(char1.Abilities[3], char1, char2, turnsCount, history);

                            //bot AI in action
                            ExecuteBotDecision(turnsCount, char2, char1, history);
                        }
                    }
                    if(char1.Reflexes < char2.Reflexes)
                    {
                        //bot AI in action
                        ExecuteBotDecision(turnsCount, char2, char1, history);
                        //abilitiesProcessor.ProcessCommand(((IBot)char2).MakeDecision(), char2, char1);
                        //history.AppendLine($"{turn}. Chosen ability: " + ability);
                    }

                    RegenerateStats(char1, char2);

                    //check if someone died
                    if (char1.Health <= 0 && char2.Health > 0)
                    {
                        render.Clear();
                        screen = RenderBattleStats(char1, char2, history);

                        render.PrintScreen(screen);
                        render.WriteLine("You have died... Ask admin to resurrect you :D");
                        render.WriteLine("");
                        //this.isInBattle = false;
                        this.IsRunning = false;
                        break;

                    }
                    if (char2.Health <= 0 && char1.Health > 0 )
                    {
                        render.Clear();
                        screen = RenderBattleStats(char1, char2, history);

                        render.PrintScreen(screen);
                        render.WriteLine("You have killed the enemy!!");
                        isInBattle = false;
                        //database.Bots.Remove((IBot)char2);
                    }
                    if (char1.Health <= 0 && char2.Health <= 0)
                    {
                        render.Clear();
                        screen = RenderBattleStats(char1, char2, history);

                        render.PrintScreen(screen);
                        render.WriteLine("You have killed the enemy, but you have died too... Ask admin to resurrect you :D");
                        render.WriteLine("");
                        //this.isInBattle = false;
                        this.IsRunning = false;
                        break;
                    }
                }
                //Console.CursorVisible = true;

            }
        }

        private void RegenerateStats(ICharacter player1, ICharacter player2)
        {
            player1.Reflexes += 5;
            player2.Reflexes += 5;
        }

        private void ExecuteBotDecision(int turnsCount, ICharacter char2, ICharacter char1, StringBuilder history)
        {
            turnsCount++;
            RenderBattleAbility(((IBot)char2).MakeDecision(), char2, char1, turnsCount, history);
        }
        
        private StringBuilder RenderBattleStats(ICharacter char1, ICharacter char2, StringBuilder history)
        {
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
                screen.AppendLine($"{i + 1} -> {ability}");
            }

            screen.AppendLine();
            screen.Append(history);
            return screen;
        }

        private void RenderBattleAbility(string ability, ICharacter player, ICharacter enemy, int turn, StringBuilder history)
        {
            abilitiesProcessor.ProcessCommand(ability, player, enemy);

            history.AppendLine($"{turn}. {player.Name} used ability {ability} on {enemy.Name}");
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

        static void PrintMap(char[,] matrix)
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

            Console.WriteLine(map.ToString());
        }

        private void PrintPlayerStats(IPlayer player)
        {
            render.WriteLine("");
            render.WriteLine(player.ToString());
        }

        private PlayerRace GetPlayerRace()
        {
            render.WriteLine("Choose a race:");
            render.WriteLine("1. Mage (damage: 50, health: 100, defense: 10)");
            render.WriteLine("2. Warrior (damage: 20, health: 300, defense: 20)");
            render.WriteLine("3. Archer (damage: 40, health: 150, defense: 15)");
            render.WriteLine("4. Rogue (damage: 30, health: 200, defense: 10)");
            render.WriteLine("5. Paladin (damage: 20, health: 180, defense: 20)");
            render.WriteLine("6. Warlock (damage: 10, health: 200, defense: 0");
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

