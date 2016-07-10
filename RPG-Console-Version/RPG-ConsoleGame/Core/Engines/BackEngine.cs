using RPG_ConsoleGame.Models.Items;

namespace RPG_ConsoleGame.Core.Engines
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
    using System.IO;
    using System.Runtime.Serialization;
    using System.Runtime.Serialization.Formatters.Binary;
    using Exceptions;

    //***********************************************
    //ima eksepshyn za fiksvane, no sa mi se spi.
    //1. ne se regenerira energy-to. izmisli mu logika
    //2. level manager sy6to trqbva da se napravi
    //************************************************

    public class BackEngine
    {
        private readonly IInputReader reader = new ConsoleInputReader();
        private readonly IRender render = new ConsoleRender();
        private readonly IPlayerFactory playerFactory = new PlayerFactory();
        private readonly ICreatureFactory creatureFactory = new CreatureFactory();
        private readonly IBossFactory bossFactory = new BossFactory();
        private readonly IShopFactory shopFactory = new ShopFactory();
        private readonly IItemFactory itemFactory = new ItemFactory();
        private readonly IGameDatabase database = new GameDatabase();

        private readonly IAbilitiesProcessor abilitiesProcessor = new AbilitiesProcessor();
        private readonly ISound sound = new Sound();

        public bool IsRunning { get; private set; }

        //static Map mapMatrix = new Map();

        //char[,] map = mapMatrix.ReadMap("../../../Map1.txt");


        //Singleton pattern
        private static BackEngine instance;

        public static BackEngine Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new BackEngine();
                }

                return instance;
            }
        }

        public void StartSinglePlayer()
        {
            //try
            //{
            InitializeDefaultsSinglePlayer();

            while (this.IsRunning)
            {
                if (Console.KeyAvailable)
                {
                    render.Clear();
                    string command = reader.ReadKey();
                    ReturnBack(command);
                    SaveGame(command);
                    OpenInventory(command, database.Players[0]);

                    database.Players[0].Move(database.Maps[0], command);

                    ViewEngine.Instance.RenderMap(database.Maps[0]);
                    ViewEngine.Instance.RenderPlayerStats(database.Players[0]);
                    ViewEngine.Instance.DisplayMapDescription();

                    if (CheckForEnemies("Bot"))
                    {
                        CheckForBattle(database.Players[0], "Bot");
                    }
                    if (CheckForEnemies("Boss"))
                    {
                        CheckForBattle(database.Players[0], "Boss");
                    }

                    CheckForShop(database.Players[0]);

                    RemoveDead();
                }
            }
            //}
            //catch (Exception)
            //{
            //    render.Clear();
            //    render.WriteLine("Problem has occurred. Restart the game.");
            //    ViewEngine.Instance.StartTimer(5);
            //    Environment.Exit(0);
            //}
        }

        private void CheckForShop(ICharacter char1)
        {

            if (char1.Position.X == database.Shops[0].Position.X &&
                char1.Position.Y == database.Shops[0].Position.Y)
            {
                render.Clear();
                StartMusic(SoundEffects.EnterShop);

                ViewEngine.Instance.RenderWarningScreen(ConsoleColor.Yellow,
                            new StringBuilder("Welcome, you entered the shop"), 3);

                render.Clear();
                StartMusic(SoundEffects.ShopTheme);


                string choice = ViewEngine.Instance.RenderShop();

                var shop = database.Shops[0];

                while (choice != "return_back")
                {
                    if (choice == "buy_helmet")
                    {
                        shop.RemoveItem(char1, choice);

                        choice = ViewEngine.Instance.RenderShop();
                    }
                    else if (choice == "buy_chest")
                    {
                        shop.RemoveItem(char1, choice);
                        choice = ViewEngine.Instance.RenderShop();

                    }
                    else if (choice == "buy_hands")
                    {
                        shop.RemoveItem(char1, choice);
                        choice = ViewEngine.Instance.RenderShop();
                    }
                    else if (choice == "buy_weapon")
                    {
                        shop.RemoveItem(char1, choice);
                        choice = ViewEngine.Instance.RenderShop();
                    }
                    else if (choice == "buy_boots")
                    {
                        shop.RemoveItem(char1, choice);
                        choice = ViewEngine.Instance.RenderShop();
                    }
                    else if (choice == "return_back")
                    {
                        break;
                    }
                }
            }
        }

        //StartMultiPlayer
        public void StartMultiPlayer()
        {
            try
            {
                InitializeDefaultsMultiplayer();

                this.IsRunning = true;

                //render.Clear();


                while (this.IsRunning)
                {
                    if (Console.KeyAvailable)
                    {
                        render.Clear();
                        string command = reader.ReadKey();
                        ReturnBack(command);
                        SaveGame(command);

                        StartMultiplayerBattle();
                    }
                }

                Console.ForegroundColor = ConsoleColor.Green;
                StartMusic(SoundEffects.DefaultTheme);
                database.ClearData();
                ViewEngine.Instance.InsideGame = false;
                ViewEngine.Instance.RenderMenu();
            }
            catch (Exception)
            {
                render.Clear();
                render.WriteLine("Problem has occurred. Restart the game.");
                ViewEngine.Instance.StartTimer(5);
                Environment.Exit(0);
            }
        }

        private void InitializeDefaultsSinglePlayer()
        {
            if (!IsRunning)
            {
                //database.ClearData();

                //beshe if bez eksepshyna
                while (database.Players.Count == 0)
                {
                    try
                    {
                        database.Players.Add(ViewEngine.Instance.GetPlayer());
                        database.AddMap(new Map().ReadMap("../../../Map1.txt"));
                        PopulateMap(database.Maps[0]);
                    }
                    catch (IncorrectNameException exception)
                    {
                        render.WriteLine(exception.Message + Environment.NewLine);
                        //database.Players.Add(ViewEngine.Instance.GetPlayer());
                    }
                }

                //For testing purposes 
                //**********************************************************************************
                database.Players[0].Health += 100000;
                //**********************************************************************************

                this.IsRunning = true;

                render.Clear();

                ViewEngine.Instance.RenderMap(database.Maps[0]);
                ViewEngine.Instance.RenderPlayerStats(database.Players[0]);
                ViewEngine.Instance.DisplayMapDescription();

                if (database.Players[0].Inventory.Count >= 0)
                {
                    database.Players[0].Inventory.Add(itemFactory.CreateNonConsumableItem(ItemType.Helmet, 1));
                    database.Players[0].Inventory.Add(itemFactory.CreateNonConsumableItem(ItemType.Helmet, 1));
                    database.Players[0].Inventory.Add(itemFactory.CreateNonConsumableItem(ItemType.Weapon, 2));
                    database.Players[0].Inventory.Add(itemFactory.CreateConsumableItem(ItemType.PotionHealth, 1));
                }
            }
        }

        private void InitializeDefaultsMultiplayer()
        {
            database.ClearData();

            while (database.Players.Count <= 1)
            {
                try
                {
                    database.Players.Add(ViewEngine.Instance.RegisterPlayerInMulti(1));
                    database.Players.Add(ViewEngine.Instance.RegisterPlayerInMulti(2));
                }
                catch (IncorrectNameException exception)
                {
                    render.WriteLine(exception.Message + Environment.NewLine);
                }
            }

            //For testing purposes 
            //**********************************************************************************
            database.Players[0].Health += 500;
            database.Players[1].Health += 500;
            //**********************************************************************************
        }

        //MultiplayerBattle
        private void StartMultiplayerBattle()
        {
            IPlayer player1 = database.Players[0];
            IPlayer player2 = database.Players[1];

            StartMusic(SoundEffects.BattleStart);
            ViewEngine.Instance.RenderWarningScreen(ConsoleColor.Red,
                new StringBuilder("PREPARE YOURSELVES!! THE BATTLE IS ABOUT TO BEGIN!!"), 3);

            StartMusic(SoundEffects.BattleTheme);

            var isInBattle = true;
            var history = new StringBuilder();
            var turnsCount = 0;

            var playerOnTurn = 0;

            if (player1.Reflexes > player2.Reflexes)
            {
                playerOnTurn = 1;
            }
            else if (player1.Reflexes < player2.Reflexes)
            {
                playerOnTurn = 2;
            }
            else
            {
                Random r = new Random();
                int n = r.Next(1, 100);

                if (n <= 50)
                {
                    playerOnTurn = 1;
                }
                else
                {
                    playerOnTurn = 2;
                }
            }

            while (isInBattle)
            {
                if (playerOnTurn == 1)
                {
                    ViewEngine.Instance.RenderBattleStatsMultiPlayer(player1, player2, history, playerOnTurn);

                    ConsoleKeyInfo keyPressed = Console.ReadKey(true);

                    if (keyPressed.Key == ConsoleKey.D1)
                    {
                        turnsCount++;
                        RegenerateStats(player1);
                        ExecutePlayerAbility(player1.Abilities[0], player1, player2, turnsCount, history);
                    }
                    if (keyPressed.Key == ConsoleKey.D2)
                    {
                        turnsCount++;
                        RegenerateStats(player1);
                        ExecutePlayerAbility(player1.Abilities[1], player1, player2, turnsCount, history);
                    }
                    if (keyPressed.Key == ConsoleKey.D3)
                    {
                        turnsCount++;
                        RegenerateStats(player1);
                        ExecutePlayerAbility(player1.Abilities[2], player1, player2, turnsCount, history);
                    }
                    if (keyPressed.Key == ConsoleKey.D4)
                    {
                        turnsCount++;
                        RegenerateStats(player1);
                        ExecutePlayerAbility(player1.Abilities[3], player1, player2, turnsCount, history);
                    }

                    playerOnTurn = 2;
                }
                if (playerOnTurn == 2)
                {
                    ViewEngine.Instance.RenderBattleStatsMultiPlayer(player1, player2, history, playerOnTurn);

                    ConsoleKeyInfo keyPressed = Console.ReadKey(true);

                    if (keyPressed.Key == ConsoleKey.D1)
                    {
                        turnsCount++;
                        RegenerateStats(player2);
                        ExecutePlayerAbility(player2.Abilities[0], player2, player1, turnsCount, history);
                    }
                    if (keyPressed.Key == ConsoleKey.D2)
                    {
                        turnsCount++;
                        RegenerateStats(player2);
                        ExecutePlayerAbility(player2.Abilities[1], player2, player1, turnsCount, history);
                    }
                    if (keyPressed.Key == ConsoleKey.D3)
                    {
                        turnsCount++;
                        RegenerateStats(player2);
                        ExecutePlayerAbility(player2.Abilities[2], player2, player1, turnsCount, history);
                    }
                    if (keyPressed.Key == ConsoleKey.D4)
                    {
                        turnsCount++;
                        RegenerateStats(player2);
                        ExecutePlayerAbility(player2.Abilities[3], player2, player1, turnsCount, history);
                    }
                    ViewEngine.Instance.RenderBattleStats(player2, player1, history);

                    playerOnTurn = 1;
                }

                //check if someone died
                if (player1.Health <= 0 && player2.Health > 0)
                {
                    ViewEngine.Instance.RenderBattleStats(player1, player2, history);

                    StartMusic(SoundEffects.BattleStart);
                    ViewEngine.Instance.RenderWarningScreen(ConsoleColor.Red,
                        new StringBuilder("Player " + player2.Name + " has won the battle!!"), 3,
                        new StringBuilder("Press enter to continue"));

                    IsRunning = false;
                    break;
                }
                if (player2.Health <= 0 && player1.Health > 0)
                {
                    ViewEngine.Instance.RenderBattleStats(player1, player2, history);

                    StartMusic(SoundEffects.EnemyIsDestroyed);
                    ViewEngine.Instance.RenderWarningScreen(
                        ConsoleColor.Red, new StringBuilder("Player " + player1.Name + " has won the battle!!"),
                        2, new StringBuilder("Press enter to continue."));

                    IsRunning = false;
                    break;
                }
                if (player1.Health <= 0 && player2.Health <= 0)
                {
                    ViewEngine.Instance.RenderBattleStats(player1, player2, history);

                    StartMusic(SoundEffects.BattleStart);
                    ViewEngine.Instance.RenderWarningScreen(
                        ConsoleColor.Red, new StringBuilder(
                            "You both died... Give beer to admin to resurrect you :D"),
                        3, new StringBuilder("Press enter or escape continue"));

                    IsRunning = false;
                    break;
                }
            }
        }

        private void PopulateMap(char[,] map)
        {
            Random random = new Random();

            for (int row = 0; row < map.GetLength(0); row++)
            {
                for (int col = 0; col < map.GetLength(1); col++)
                {
                    int raceNumber = random.Next(1, 6);

                    if (map[row, col] == 'E')
                    {
                        database.AddCreature(creatureFactory.CreateCreature(
                            new Position(row, col), 'E', "Bot",
                            (raceNumber.Equals(1)) ? PlayerRace.Mage :
                            (raceNumber.Equals(2)) ? PlayerRace.Warrior :
                            (raceNumber.Equals(3)) ? PlayerRace.Rogue :
                            (raceNumber.Equals(4)) ? PlayerRace.Paladin :
                            (raceNumber.Equals(5)) ? PlayerRace.Warlock :
                            PlayerRace.Archer));
                    }

                    if (map[row, col] == 'B')
                    {
                        database.AddBoss(bossFactory.CreateBoss(
                            new Position(row, col), 'B', "Boss",
                            (raceNumber.Equals(1)) ? BossRace.Boss1 :
                            (raceNumber.Equals(2)) ? BossRace.Boss2 :
                            (raceNumber.Equals(3)) ? BossRace.Boss3 :
                            (raceNumber.Equals(4)) ? BossRace.Boss4 :
                            (raceNumber.Equals(5)) ? BossRace.Boss5 :
                            BossRace.Boss6));
                    }
                    if (map[row, col] == 'S')
                    {
                        database.AddShop(shopFactory.CreateShop(new Position(row, col), 'S', "Shop"));
                    }
                }
            }
        }

        private bool CheckForEnemies(string enemy)
        {
            bool result = false;

            if (enemy.Equals("Bot"))
            {
                for (int i = 0; i < database.Creatures.Count; i++)
                {
                    if (database.Creatures[i].Health > 0)
                    {
                        result = true;
                    }
                }
            }
            if (enemy.Equals("Boss"))
            {
                for (int i = 0; i < database.Bosses.Count; i++)
                {
                    if (database.Bosses[i].Health > 0)
                    {
                        result = true;
                    }
                }
            }
            return result;
        }

        private void CheckForBattle(ICharacter char1, string enemyType)
        {
            if (enemyType.Equals("Bot"))
            {
                for (int i = 0; i < database.Creatures.Count; i++)
                {
                    if (char1.Position.X == database.Creatures[i].Position.X &&
                        char1.Position.Y == database.Creatures[i].Position.Y)
                    {
                        IBot char2 = database.Creatures[i];

                        StartMusic(SoundEffects.BattleStart);
                        ViewEngine.Instance.RenderWarningScreen(ConsoleColor.Red,
                            new StringBuilder("YOU ARE ENGAGING ENEMY!!"), 3);

                        StartMusic(SoundEffects.BattleTheme);

                        var isInBattle = true;
                        var history = new StringBuilder();
                        var turnsCount = 0;

                        var playerOnTurn = 0;

                        if (char1.Reflexes > char2.Reflexes)
                        {
                            playerOnTurn = 1;
                        }
                        else if (char1.Reflexes < char2.Reflexes)
                        {
                            playerOnTurn = 2;
                        }
                        else
                        {
                            Random r = new Random();
                            int n = r.Next(1, 100);

                            if (n <= 50)
                            {
                                playerOnTurn = 1;
                            }
                            else
                            {
                                playerOnTurn = 2;
                            }
                        }

                        while (isInBattle)
                        {
                            ViewEngine.Instance.RenderBattleStats(char1, char2, history);

                            if (playerOnTurn == 1)
                            {
                                ConsoleKeyInfo keyPressed = Console.ReadKey(true);

                                if (keyPressed.Key == ConsoleKey.D1)
                                {
                                    turnsCount++;
                                    RegenerateStats(char1);
                                    ExecutePlayerAbility(char1.Abilities[0], char1, char2, turnsCount, history);

                                    ////bot AI in action
                                    //ExecuteBotDecision(turnsCount, char2, char1, history);
                                    //turnsCount++;
                                    //RegenerateStats(char2);

                                }
                                if (keyPressed.Key == ConsoleKey.D2)
                                {
                                    turnsCount++;
                                    RegenerateStats(char1);
                                    ExecutePlayerAbility(char1.Abilities[1], char1, char2, turnsCount, history);

                                    ////bot AI in action
                                    //ExecuteBotDecision(turnsCount, char2, char1, history);
                                    //turnsCount++;
                                    //RegenerateStats(char2);
                                }
                                if (keyPressed.Key == ConsoleKey.D3)
                                {
                                    turnsCount++;
                                    RegenerateStats(char1);
                                    ExecutePlayerAbility(char1.Abilities[2], char1, char2, turnsCount, history);

                                    ////bot AI in action
                                    //ExecuteBotDecision(turnsCount, char2, char1, history);
                                    //turnsCount++;
                                    //RegenerateStats(char2);
                                }
                                if (keyPressed.Key == ConsoleKey.D4)
                                {
                                    turnsCount++;
                                    RegenerateStats(char1);
                                    ExecutePlayerAbility(char1.Abilities[3], char1, char2, turnsCount, history);

                                    ////bot AI in action
                                    //ExecuteBotDecision(turnsCount, char2, char1, history);
                                    //turnsCount++;
                                    //RegenerateStats(char2);
                                }

                                playerOnTurn = 2;
                            }
                            if (playerOnTurn == 2)
                            {
                                //bot AI in action
                                //turnsCount++;
                                RegenerateStats(char2);
                                ExecuteBotDecision(turnsCount, char2, char1, history);
                                turnsCount++;
                                playerOnTurn = 1;
                                ////player move
                                //ConsoleKeyInfo keyPressed = Console.ReadKey(true);

                                //if (keyPressed.Key == ConsoleKey.D1)
                                //{
                                //    turnsCount++;
                                //    RegenerateStats(char1);
                                //    ExecutePlayerAbility(char1.Abilities[0], char1, char2, turnsCount, history);
                                //}
                                //if (keyPressed.Key == ConsoleKey.D2)
                                //{
                                //    turnsCount++;
                                //    RegenerateStats(char1);
                                //    ExecutePlayerAbility(char1.Abilities[1], char1, char2, turnsCount, history);
                                //}
                                //if (keyPressed.Key == ConsoleKey.D3)
                                //{
                                //    turnsCount++;
                                //    RegenerateStats(char1);
                                //    ExecutePlayerAbility(char1.Abilities[2], char1, char2, turnsCount, history);
                                //}
                                //if (keyPressed.Key == ConsoleKey.D4)
                                //{
                                //    turnsCount++;
                                //    RegenerateStats(char1);
                                //    ExecutePlayerAbility(char1.Abilities[3], char1, char2, turnsCount, history);
                                //}
                            }

                            //check if someone died
                            if (char1.Health <= 0 && char2.Health > 0)
                            {
                                ViewEngine.Instance.RenderBattleStats(char1, char2, history);

                                StartMusic(SoundEffects.BattleStart);
                                ViewEngine.Instance.RenderWarningScreen(ConsoleColor.Red,
                                    new StringBuilder("YOU HAVE DIED!! Give beer to admin to resurrect you :D"), 3,
                                    new StringBuilder("Press enter to continue"));

                                StartMusic(SoundEffects.DefaultTheme);
                                ViewEngine.Instance.InsideGame = false;

                                database.ClearData();
                                ReturnBack("exit");
                            }
                            if (char2.Health <= 0 && char1.Health > 0)
                            {
                                ViewEngine.Instance.RenderBattleStats(char1, char2, history);

                                StartMusic(SoundEffects.EnemyIsDestroyed);
                                ViewEngine.Instance.RenderWarningScreen(
                                    ConsoleColor.Red, new StringBuilder("YOU HAVE KILLED THE ENEMY!!"),
                                    2, new StringBuilder("Press enter to continue."));

                                StartMusic(SoundEffects.DefaultTheme);

                                Console.ForegroundColor = ConsoleColor.Green;
                                isInBattle = false;
                                //database.Bots.Remove((IBot)char2);
                            }
                            if (char1.Health <= 0 && char2.Health <= 0)
                            {
                                ViewEngine.Instance.RenderBattleStats(char1, char2, history);

                                StartMusic(SoundEffects.BattleStart);
                                ViewEngine.Instance.RenderWarningScreen(
                                    ConsoleColor.Red, new StringBuilder(
                                        "You have killed the enemy, but you have died too... Give beer to admin to resurrect you :D"),
                                    3, new StringBuilder("Press enter or escape continue"));

                                StartMusic(SoundEffects.DefaultTheme);
                                ViewEngine.Instance.InsideGame = false;

                                database.ClearData();
                                ReturnBack("exit");
                            }
                        }
                    }
                }
            }
            else if (enemyType.Equals("Boss"))
            {
                for (int i = 0; i < database.Bosses.Count; i++)
                {
                    if (char1.Position.X == database.Bosses[i].Position.X &&
                        char1.Position.Y == database.Bosses[i].Position.Y)
                    {
                        IBot char2 = database.Bosses[i];

                        StartMusic(SoundEffects.BattleStart);
                        ViewEngine.Instance.RenderWarningScreen(ConsoleColor.Red,
                            new StringBuilder("YOU ARE ENGAGING ENEMY!!"), 3);

                        StartMusic(SoundEffects.BattleTheme);

                        var isInBattle = true;
                        var history = new StringBuilder();
                        var turnsCount = 0;

                        var playerOnTurn = 0;

                        if (char1.Reflexes > char2.Reflexes)
                        {
                            playerOnTurn = 1;
                        }
                        else if (char1.Reflexes < char2.Reflexes)
                        {
                            playerOnTurn = 2;
                        }
                        else
                        {
                            Random r = new Random();
                            int n = r.Next(1, 100);

                            if (n <= 50)
                            {
                                playerOnTurn = 1;
                            }
                            else
                            {
                                playerOnTurn = 2;
                            }
                        }

                        while (isInBattle)
                        {
                            ViewEngine.Instance.RenderBattleStats(char1, char2, history);

                            if (playerOnTurn == 1)
                            {
                                ConsoleKeyInfo keyPressed = Console.ReadKey(true);

                                if (keyPressed.Key == ConsoleKey.D1)
                                {
                                    turnsCount++;
                                    RegenerateStats(char1);
                                    ExecutePlayerAbility(char1.Abilities[0], char1, char2, turnsCount, history);

                                    ////bot AI in action
                                    //ExecuteBotDecision(turnsCount, char2, char1, history);
                                    //turnsCount++;
                                    //RegenerateStats(char2);
                                }
                                if (keyPressed.Key == ConsoleKey.D2)
                                {
                                    turnsCount++;
                                    RegenerateStats(char1);
                                    ExecutePlayerAbility(char1.Abilities[1], char1, char2, turnsCount, history);

                                    ////bot AI in action
                                    //ExecuteBotDecision(turnsCount, char2, char1, history);
                                    //turnsCount++;
                                    //RegenerateStats(char2);
                                }
                                if (keyPressed.Key == ConsoleKey.D3)
                                {
                                    turnsCount++;
                                    RegenerateStats(char1);
                                    ExecutePlayerAbility(char1.Abilities[2], char1, char2, turnsCount, history);

                                    ////bot AI in action
                                    //ExecuteBotDecision(turnsCount, char2, char1, history);
                                    //turnsCount++;
                                    //RegenerateStats(char2);
                                }
                                if (keyPressed.Key == ConsoleKey.D4)
                                {
                                    turnsCount++;
                                    RegenerateStats(char1);
                                    ExecutePlayerAbility(char1.Abilities[3], char1, char2, turnsCount, history);

                                    ////bot AI in action
                                    //ExecuteBotDecision(turnsCount, char2, char1, history);
                                    //turnsCount++;
                                    //RegenerateStats(char2);
                                }

                                playerOnTurn = 2;
                            }
                            if (playerOnTurn == 2)
                            {
                                //bot AI in action
                                //turnsCount++;
                                RegenerateStats(char2);
                                ExecuteBotDecision(turnsCount, char2, char1, history);
                                turnsCount++;

                                playerOnTurn = 1;

                                ////player move
                                //ConsoleKeyInfo keyPressed = Console.ReadKey(true);

                                //if (keyPressed.Key == ConsoleKey.D1)
                                //{
                                //    turnsCount++;
                                //    RegenerateStats(char1);
                                //    ExecutePlayerAbility(char1.Abilities[0], char1, char2, turnsCount, history);
                                //}
                                //if (keyPressed.Key == ConsoleKey.D2)
                                //{
                                //    turnsCount++;
                                //    RegenerateStats(char1);
                                //    ExecutePlayerAbility(char1.Abilities[1], char1, char2, turnsCount, history);
                                //}
                                //if (keyPressed.Key == ConsoleKey.D3)
                                //{
                                //    turnsCount++;
                                //    RegenerateStats(char1);
                                //    ExecutePlayerAbility(char1.Abilities[2], char1, char2, turnsCount, history);
                                //}
                                //if (keyPressed.Key == ConsoleKey.D4)
                                //{
                                //    turnsCount++;
                                //    RegenerateStats(char1);
                                //    ExecutePlayerAbility(char1.Abilities[3], char1, char2, turnsCount, history);
                                //}
                            }

                            //check if someone died
                            if (char1.Health <= 0 && char2.Health > 0)
                            {
                                ViewEngine.Instance.RenderBattleStats(char1, char2, history);

                                StartMusic(SoundEffects.BattleStart);
                                ViewEngine.Instance.RenderWarningScreen(ConsoleColor.Red,
                                    new StringBuilder("YOU HAVE DIED!! Give beer to admin to resurrect you :D"), 3,
                                    new StringBuilder("Press enter to continue"));

                                StartMusic(SoundEffects.DefaultTheme);
                                ViewEngine.Instance.InsideGame = false;

                                database.ClearData();
                                ReturnBack("exit");
                            }
                            if (char2.Health <= 0 && char1.Health > 0)
                            {
                                ViewEngine.Instance.RenderBattleStats(char1, char2, history);

                                StartMusic(SoundEffects.EnemyIsDestroyed);
                                ViewEngine.Instance.RenderWarningScreen(
                                    ConsoleColor.Red, new StringBuilder("YOU HAVE KILLED THE ENEMY!!"),
                                    2, new StringBuilder("Press enter to continue."));

                                StartMusic(SoundEffects.DefaultTheme);

                                Console.ForegroundColor = ConsoleColor.Green;
                                isInBattle = false;
                                //database.Bots.Remove((IBot)char2);
                            }
                            if (char1.Health <= 0 && char2.Health <= 0)
                            {
                                ViewEngine.Instance.RenderBattleStats(char1, char2, history);

                                StartMusic(SoundEffects.BattleStart);
                                ViewEngine.Instance.RenderWarningScreen(
                                    ConsoleColor.Red, new StringBuilder(
                                        "You have killed the enemy, but you have died too... Give beer to admin to resurrect you :D"),
                                    3, new StringBuilder("Press enter or escape continue"));

                                StartMusic(SoundEffects.DefaultTheme);
                                ViewEngine.Instance.InsideGame = false;

                                database.ClearData();
                                ReturnBack("exit");
                            }
                        }
                    }
                }
            }
        }

        private void RegenerateStats(ICharacter player)
        {
            player.Reflexes += 5;
        }

        private void ExecuteBotDecision(int turnsCount, IBot char2, ICharacter char1, StringBuilder history)
        {
            turnsCount++;
            ExecutePlayerAbility(char2.MakeDecision(), char2, char1, turnsCount, history);
        }

        private void ExecutePlayerAbility(string ability, ICharacter player, ICharacter enemy, int turn, StringBuilder history)
        {
            abilitiesProcessor.ProcessCommand(ability, player, enemy);

            history.AppendLine($"{turn}. {player.Name} used ability {ability}");
        }

        void RemoveDead()
        {
            for (int index = 0; index < database.Creatures.Count; index++)
            {
                if (database.Creatures[index].Health <= 0)
                {
                    database.Creatures.Remove(database.Creatures[index]);
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

        private void ReturnBack(string command)
        {
            if (command == "exit")
            {
                render.Clear();
                ViewEngine.Instance.RenderMenu();
            }
        }

        private void StartMusic(SoundEffects stage)
        {
            sound.SFX(stage);
        }

        public void SaveGame(string command)
        {
            if (command == "save")
            {
                database.Date = DateTime.Now;
                SaveData(database, ViewEngine.Instance.ChooseSaveSlot());
                render.Clear();
            }
        }

        private void OpenInventory(string command, IPlayer player)
        {
            if (command == "inventory")
            {
                ViewEngine.Instance.RenderInventory(player);
            }
        }

        private void SaveData(IGameDatabase data, string slot)
        {
            FileStream fs = new FileStream($@"..\..\GameSavedData\Save-{slot}.xml", FileMode.Create);
            BinaryFormatter formatter = new BinaryFormatter();

            try
            {
                data.IsLoaded = true;
                formatter.Serialize(fs, data);
                ViewEngine.Instance.DisplayMessage("Data Saved");
                ViewEngine.Instance.StartTimer(5);
            }
            catch (SerializationException e)
            {
                ViewEngine.Instance.DisplayMessage("Failed to serialize. Reason: " + e.Message);
                //Console.WriteLine("Failed to serialize. Reason: " + e.Message);
            }
            finally
            {
                fs.Close();
            }
        }

        public void LoadGame()
        {
            string choice = ViewEngine.Instance.ChooseSavedGameSlot();
            ReturnBack(choice);
            database.ClearData();
            database.LoadData(LoadData($@"..\..\GameSavedData\Save-{choice}.xml"));
            //LoadData($@"..\..\GameSavedData\Save-{choice}.xml", database);
            StartSinglePlayer();
        }

        private IGameDatabase LoadData(string path)
        {
            try
            {
                FileStream fs = new FileStream(path, FileMode.Open);

                BinaryFormatter formatter = new BinaryFormatter();

                //IGameDatabase obj = (IGameDatabase)formatter.Deserialize(fs);
                IGameDatabase data = (IGameDatabase)formatter.Deserialize(fs);
                data.IsLoaded = true;
                //obj.IsLoaded = true;
                ViewEngine.Instance.InsideGame = true;

                fs.Close();

                return data;
                //database.LoadData(obj);
                //StartLoadedGame(data);
            }
            catch (Exception e)
            {
                ViewEngine.Instance.DisplayMessage(e.Message);
                return database;
            }
        }

        public void StartLoadedGame(IGameDatabase data)
        {
            LoadGame();
            StartSinglePlayer();
        }

        public void StartNewSinglePlayer()
        {
            database.ClearData();
            StartSinglePlayer();
        }
    }
}
