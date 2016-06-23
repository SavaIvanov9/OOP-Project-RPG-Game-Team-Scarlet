﻿namespace RPG_ConsoleGame.Core.Engines
{
    using Characters;
    using Factories;
    using Map;
    using System;
    using System.Text;
    using Interfaces;
    using Sound;
    using UserInterface;
    using Models.Characters.Abilities;

    public class BackEngine : IBackEngine
    {
        private readonly IInputReader reader = new ConsoleInputReader();
        private readonly IRender render = new ConsoleRender();
        private readonly IPlayerFactory playerFactory = new PlayerFactory();
        private readonly IBotFactory botFactory = new BotFactory();
        private readonly IBossFactory bossFactory = new BossFactory();
        private readonly IGameDatabase database = new GameDatabase();
        private readonly IViewEngine viewEngine = new ViewEngine();
        //private readonly IBackEngine backEngine = new BackEngine();
        private readonly IAbilitiesProcessor abilitiesProcessor = new AbilitiesProcessor();
        private readonly ISound sound = new Sound();

        public bool IsRunning { get; private set; }

        static Map mapMatrix = new Map();

        char[,] map = mapMatrix.ReadMap("../../../Map1.txt");

        public void StartSinglePlayer()
        {
            var newPlayer = viewEngine.GetPlayer();
            database.Players.Add(newPlayer);

            database.AddBot(botFactory.CreateBot(new Position(2, 7), 'E', "demon", PlayerRace.Mage));
            database.AddPlayer(playerFactory.CreateHuman(new Position(5, 5), 'A', "Go6o", PlayerRace.Mage));
            database.AddBoss(bossFactory.CreateBoss(new Position(9, 11), 'B', "Boss1", BossRace.Boss1));
            //Using ability
            //abilitiesProcessor.ProcessCommand(database.Players[0].Abilities[0], database.Bots[0]);

            this.IsRunning = true;

            render.Clear();

            viewEngine.RenderMap(map);
            viewEngine.RenderPlayerStats(database.Players[0]);

            while (this.IsRunning)
            {
                if (Console.KeyAvailable)
                {
                    render.Clear();
                    string command = reader.ReadKey();
                    ReturnBack(command);
                    database.Players[0].Move(map, command);

                    viewEngine.RenderMap(map);
                    viewEngine.RenderPlayerStats(database.Players[0]);

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

        public void CheckForBattle(ICharacter char1, ICharacter char2)
        {
            if (char1.Position.X == char2.Position.X && char1.Position.Y == char2.Position.Y)
            {
                StartMusic(SoundEffects.BattleStart);
                viewEngine.RenderWarningScreen(ConsoleColor.Red, new StringBuilder("YOU ARE ENGAGING ENEMY!!"), 3);

                StartMusic(SoundEffects.BattleTheme);

                var isInBattle = true;
                var history = new StringBuilder();
                var turnsCount = 0;

                while (isInBattle)
                {
                    viewEngine.RenderBattleStats(char1, char2, history);

                    if (char1.Reflexes >= char2.Reflexes)
                    {
                        ConsoleKeyInfo keyPressed = Console.ReadKey(true);

                        if (keyPressed.Key == ConsoleKey.D1)
                        {
                            turnsCount++;
                            RegenerateStats(char1);
                            ExecutePlayerAbility(char1.Abilities[0], char1, char2, turnsCount, history);

                            //bot AI in action
                            ExecuteBotDecision(turnsCount, char2, char1, history);
                            turnsCount++;
                            RegenerateStats(char2);
                        }
                        if (keyPressed.Key == ConsoleKey.D2)
                        {
                            turnsCount++;
                            RegenerateStats(char1);
                            ExecutePlayerAbility(char1.Abilities[1], char1, char2, turnsCount, history);

                            //bot AI in action
                            ExecuteBotDecision(turnsCount, char2, char1, history);
                            turnsCount++;
                            RegenerateStats(char2);
                        }
                        if (keyPressed.Key == ConsoleKey.D3)
                        {
                            turnsCount++;
                            RegenerateStats(char1);
                            ExecutePlayerAbility(char1.Abilities[2], char1, char2, turnsCount, history);

                            //bot AI in action
                            ExecuteBotDecision(turnsCount, char2, char1, history);
                            turnsCount++;
                            RegenerateStats(char2);
                        }
                        if (keyPressed.Key == ConsoleKey.D4)
                        {
                            turnsCount++;
                            RegenerateStats(char1);
                            ExecutePlayerAbility(char1.Abilities[3], char1, char2, turnsCount, history);

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
                        viewEngine.RenderBattleStats(char1, char2, history);

                        StartMusic(SoundEffects.BattleStart);
                        viewEngine.RenderWarningScreen(ConsoleColor.Red, new StringBuilder("YOU HAVE DIED!! Give beer to admin to resurrect you :D"), 3, new StringBuilder("Press enter to continue"));

                        ReturnBack("exit");
                    }
                    if (char2.Health <= 0 && char1.Health > 0)
                    {
                        viewEngine.RenderBattleStats(char1, char2, history);

                        StartMusic(SoundEffects.EnemyIsDestroyed);
                        viewEngine.RenderWarningScreen(
                            ConsoleColor.Red, new StringBuilder("YOU HAVE KILLED THE ENEMY!!"),
                            2, new StringBuilder("Press enter to continue."));

                        StartMusic(SoundEffects.DefaultTheme);

                        Console.ForegroundColor = ConsoleColor.Green;
                        isInBattle = false;
                        //database.Bots.Remove((IBot)char2);
                    }
                    if (char1.Health <= 0 && char2.Health <= 0)
                    {
                        viewEngine.RenderBattleStats(char1, char2, history);

                        StartMusic(SoundEffects.BattleStart);
                        viewEngine.RenderWarningScreen(
                            ConsoleColor.Red, new StringBuilder(
                            " You have killed the enemy, but you have died too... Give beer to admin to resurrect you :D"),
                            3, new StringBuilder("Press enter or escape continue"));
                        StartMusic(SoundEffects.DefaultTheme);

                        ReturnBack("exit");
                    }
                }

            }
        }

        public void RegenerateStats(ICharacter player)
        {
            player.Reflexes += 5;
        }

        public void ExecuteBotDecision(int turnsCount, ICharacter char2, ICharacter char1, StringBuilder history)
        {
            turnsCount++;
            ExecutePlayerAbility(((IBot)char2).MakeDecision(), char2, char1, turnsCount, history);
        }

        public void ExecutePlayerAbility(string ability, ICharacter player, ICharacter enemy, int turn, StringBuilder history)
        {
            abilitiesProcessor.ProcessCommand(ability, player, enemy);

            history.AppendLine($"{turn}. {player.Name} used ability {ability} on {enemy.Name}");
        }

        public void RemoveDead(IGameDatabase database)
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

        public void ReturnBack(string command)
        {
            if (command == "exit")
            {
                render.Clear();
                viewEngine.RenderMenu();
            }
        }

        public void StartMusic(SoundEffects stage)
        {
            sound.SFX(stage);
        }
    }
}
