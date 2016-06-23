using System;
using System.Linq;
using System.Text;
using System.Threading;
using RPG_ConsoleGame.Characters;
using RPG_ConsoleGame.Core.Factories;
using RPG_ConsoleGame.Interfaces;
using RPG_ConsoleGame.Map;
using RPG_ConsoleGame.UserInterface;
using RPG_ConsoleGame.Models.Characters.Abilities.Mage;
using RPG_ConsoleGame.Sound;

namespace RPG_ConsoleGame.Core.StateManager
{
    public class StateManager : IStateManager
    {
        private readonly IInputReader reader = new ConsoleInputReader();
        private readonly IRender render = new ConsoleRender();
        private readonly IPlayerFactory playerFactory = new PlayerFactory();
        private readonly IBotFactory botFactory = new BotFactory();
        private readonly IBossFactory bossFactory = new BossFactory();
        private readonly IGameDatabase database = new GameDatabase();
        private readonly IAbilitiesProcessor abilitiesProcessor = new AbilitiesProcessor();
        private readonly ISound sound = new Sound.Sound();
        private readonly IViewEngine viewEngine = new ViewEngine();

        public bool IsRunning { get; private set; }

        static Map.Map mapMatrix = new Map.Map();

        char[,] map = mapMatrix.ReadMap("../../../Map1.txt");

        static Position plPos = new Position();

        public StateManager()
        {
            viewEngine.OnMenuClick += StartState;
        }

        public void StartState(string state)
        {
            switch (state)
            {
                case StateConstants.BeginGame:
                    viewEngine.DrawMenu();
                    break;
                case StateConstants.SinglePlayer:
                    StartSinglePlayer();
                    break;
                case StateConstants.Multiplayer:
                    StartSinglePlayer();
                    break;
                case StateConstants.SurvivalMode:
                    StartSinglePlayer();
                    break;
                case StateConstants.LoadGame:
                    StartSinglePlayer();
                    break;
                case StateConstants.Credits:
                    viewEngine.DrawCredits();
                    viewEngine.DrawMenu();
                    break;
                //case StateConstants.ReturnBack:
                //    ReturnBack();
                //    break;
                default:
                    break;
            }
        }

        private void ReturnBack(string command)
        {
            if(command == "exit")
            {
                render.Clear();
                viewEngine.DrawMenu();
            }
        }

        private void StartSinglePlayer()
        {
            var newPlayer = viewEngine.GetPlayer();
            database.Players.Add(newPlayer);

            database.AddBot(botFactory.CreateBot(new Position(2, 7), 'E', "demon", PlayerRace.Mage));
            database.AddPlayer(playerFactory.CreateHuman(new Position(5, 5), 'A', "Go6o", PlayerRace.Mage));
            database.AddBoss(bossFactory.CreateBoss(new Position(9, 11), 'B', "Boss1", BossRace.Boss1));
            //Using ability
            //abilitiesProcessor.ProcessCommand(database.Players[0].Abilities[0], database.Bots[0]);

            this.IsRunning = true;

            Console.Clear();

            PrintMap(map);
            PrintPlayerStats(database.Players[0]);

            while (this.IsRunning)
            {
                //ReturnBack();

                if (Console.KeyAvailable)
                {
                    //ConsoleKeyInfo keyPressed = Console.ReadKey(true);
                    //if (keyPressed.Key == ConsoleKey.Escape)
                    //{
                    //    render.Clear();
                    //    viewEngine.DrawMenu();
                    //}
                    

                    Console.Clear();
                    string command = reader.ReadKey();
                    ReturnBack(command);
                    database.Players[0].Move(map, command);

                    PrintMap(map);
                    PrintPlayerStats(database.Players[0]);
                    if (database.Bots.Count > 0)
                    {
                        CheckForBattle(database.Players[0], database.Bots[0]);
                    }
                    if (database.Bosses.Count > 0)
                    {
                        CheckForBattle(database.Players[0], database.Bosses[0]);
                    }

                    RemoveDead(database);
                }
            }
        }

        private void CheckForBattle(ICharacter char1, ICharacter char2)
        {
            if (char1.Position.X == char2.Position.X && char1.Position.Y == char2.Position.Y)
            {
                StartMusic(SoundEffects.BattleStart);
                viewEngine.WarningScreen(ConsoleColor.Red, new StringBuilder("YOU ARE ENGAGING ENEMY!!"), 3);
               
                StartMusic(SoundEffects.BattleTheme);

                var isInBattle = true;
                var history = new StringBuilder();
                var turnsCount = 0;

                while (isInBattle)
                {
                    render.Clear();

                    var screen = RenderBattleStats(char1, char2, history);

                    render.PrintScreen(screen);

                    if (char1.Reflexes >= char2.Reflexes)
                    {
                        ConsoleKeyInfo keyPressed = Console.ReadKey(true);

                        if (keyPressed.Key == ConsoleKey.D1)
                        {
                            turnsCount++;
                            RegenerateStats(char1);
                            RenderBattleAbility(char1.Abilities[0], char1, char2, turnsCount, history);

                            //bot AI in action
                            ExecuteBotDecision(turnsCount, char2, char1, history);
                            turnsCount++;
                            RegenerateStats(char2);
                        }
                        if (keyPressed.Key == ConsoleKey.D2)
                        {
                            turnsCount++;
                            RegenerateStats(char1);
                            RenderBattleAbility(char1.Abilities[1], char1, char2, turnsCount, history);

                            //bot AI in action
                            ExecuteBotDecision(turnsCount, char2, char1, history);
                            turnsCount++;
                            RegenerateStats(char2);
                        }
                        if (keyPressed.Key == ConsoleKey.D3)
                        {
                            turnsCount++;
                            RegenerateStats(char1);
                            RenderBattleAbility(char1.Abilities[2], char1, char2, turnsCount, history);

                            //bot AI in action
                            ExecuteBotDecision(turnsCount, char2, char1, history);
                            turnsCount++;
                            RegenerateStats(char2);
                        }
                        if (keyPressed.Key == ConsoleKey.D4)
                        {
                            turnsCount++;
                            RegenerateStats(char1);
                            RenderBattleAbility(char1.Abilities[3], char1, char2, turnsCount, history);

                            //bot AI in action
                            ExecuteBotDecision(turnsCount, char2, char1, history);
                            turnsCount++;
                            RegenerateStats(char2);
                        }
                    }
                    if (char1.Reflexes < char2.Reflexes)
                    {
                        //bot AI in action
                        turnsCount++;
                        RegenerateStats(char2);
                        ExecuteBotDecision(turnsCount, char2, char1, history);
                    }

                    //check if someone died
                    if (char1.Health <= 0 && char2.Health > 0)
                    {
                        render.Clear();
                        screen = RenderBattleStats(char1, char2, history);

                        render.PrintScreen(screen);

                        StartMusic(SoundEffects.BattleStart);
                        viewEngine.WarningScreen(ConsoleColor.Red, new StringBuilder("YOU HAVE DIED!! Give beer to admin to resurrect you :D"), 3, new StringBuilder("Press enter to continue"));
                       

                        ReturnBack("exit");
                        //this.IsRunning = false;
                        //break;
                    }
                    if (char2.Health <= 0 && char1.Health > 0)
                    {
                        render.Clear();
                        screen = RenderBattleStats(char1, char2, history);

                        render.PrintScreen(screen);

                        StartMusic(SoundEffects.EnemyIsDestroyed);
                        viewEngine.WarningScreen(
                            ConsoleColor.Red, new StringBuilder("YOU HAVE KILLED THE ENEMY!!"),
                            2, new StringBuilder("Press enter to continue."));
                        
                        StartMusic(SoundEffects.DefaultTheme);

                        Console.ForegroundColor = ConsoleColor.Green;
                        isInBattle = false;
                        //database.Bots.Remove((IBot)char2);
                    }
                    if (char1.Health <= 0 && char2.Health <= 0)
                    {
                        render.Clear();
                        screen = RenderBattleStats(char1, char2, history);

                        render.PrintScreen(screen);

                        StartMusic(SoundEffects.BattleStart);
                        viewEngine.WarningScreen(
                            ConsoleColor.Red, new StringBuilder(
                            " You have killed the enemy, but you have died too... Give beer to admin to resurrect you :D"), 
                            3, new StringBuilder("Press enter or escape continue"));
                        StartMusic(SoundEffects.DefaultTheme);

                        ReturnBack("exit");

                        //this.IsRunning = false;
                        //break;
                    }
                }

            }
        }

        private void RegenerateStats(ICharacter player)
        {
            player.Reflexes += 5;
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

        private void RemoveDead(IGameDatabase database)
        {
            for (int index = 0; index < database.Bots.Count; index++)
            {
                if (database.Bots[index].Health <= 0)
                {
                    database.Bots.Remove(database.Bots[index]);
                }
            }

            for (int index = 0; index < database.Bosses.Count; index++)
            {
                if (database.Bosses[index].Health <= 0)
                {
                    database.Bosses.Remove(database.Bosses[index]);
                }
            }
        }

        private void StartMusic(SoundEffects stage)
        {
            sound.SFX(stage);
        }
    }
}
