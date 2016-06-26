using System.Collections;
using System.IO;
using System.Runtime.InteropServices;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;

//***********************************************
//ima eksepshyn za fiksvane, no sa mi se spi.
//kato bota ima pove4e refleksi i udrq pryv, udrq dokato ne se izrawnqt refleksite na dwamata...
//dobawi hod na pleyra sled toq slu4ai
//************************************************

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

    public class BackEngine
    {
        private readonly IInputReader reader = new ConsoleInputReader();
        private readonly IRender render = new ConsoleRender();
        private readonly IPlayerFactory playerFactory = new PlayerFactory();
        private readonly ICreatureFactory creatureFactory = new CreatureFactory();
        private readonly IBossFactory bossFactory = new BossFactory();
        private readonly IShopFactory shopFactory = new ShopFactory();
        
        private readonly IAbilitiesProcessor abilitiesProcessor = new AbilitiesProcessor();
        private readonly ISound sound = new Sound();

        public bool IsRunning { get; private set; }

        //static Map mapMatrix = new Map();

        //char[,] map = mapMatrix.ReadMap("../../../Map1.txt");

        
        //Singleton patern
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

        public void StartSinglePlayer(IGameDatabase database)
        {
            //database.ClearData();
            
            if (database.Players.Count == 0)
            {
                database.Players.Add(ViewEngine.Instance.GetPlayer());
            }

            if (database.IsLoaded == false)
            {
                database.AddMap(new Map().ReadMap("../../../Map1.txt"));
                PopulateMap(database.Maps[0], database);
            }

            //For testing purposes 
            //**********************************************************************************
            database.Players[0].Health += 1000000;
            //**********************************************************************************

            this.IsRunning = true;

            render.Clear();

            ViewEngine.Instance.RenderMap(database.Maps[0]);
            ViewEngine.Instance.RenderPlayerStats(database.Players[0]);

            while (this.IsRunning)
            {
                if (Console.KeyAvailable)
                {
                    render.Clear();
                    string command = reader.ReadKey();
                    ReturnBack(command);
                    SaveGame(command, database);

                    database.Players[0].Move(database.Maps[0], command);

                    ViewEngine.Instance.RenderMap(database.Maps[0]);
                    ViewEngine.Instance.RenderPlayerStats(database.Players[0]);

                    if (CheckForEnemies(database, "Bot"))
                    {
                        CheckForBattle(database.Players[0], database, "Bot");
                    }
                    if (CheckForEnemies(database, "Boss"))
                    {
                        CheckForBattle(database.Players[0], database, "Boss");
                    }

                    RemoveDead(database);
                }
            }
        }

        private void PopulateMap(char[,] map, IGameDatabase database)
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

        private bool CheckForEnemies(IGameDatabase database, string enemy)
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

        private void CheckForBattle(ICharacter char1, IGameDatabase database, string enemyType)
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

                        while (isInBattle)
                        {
                            ViewEngine.Instance.RenderBattleStats(char1, char2, history);

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

                        while (isInBattle)
                        {
                            ViewEngine.Instance.RenderBattleStats(char1, char2, history);

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

            history.AppendLine($"{turn}. {player.Name} used ability {ability} on {enemy.Name}");
        }

        private void RemoveDead(IGameDatabase database)
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

        public void SaveGame(string command, IGameDatabase data)
        {
            if (command == "save")
            {
                data.Date = DateTime.Now;
                SaveData(data, ViewEngine.Instance.ChooseSaveSlot());
                render.Clear();
            }
        }

        public void LoadGame(IGameDatabase database)
        {
            string choice = ViewEngine.Instance.ChooseSavedGameSlot();
            ReturnBack(choice);
            database.ClearData();
            //database.LoadData(LoadData($@"..\..\GameSavedData\Save-{choice}.xml"));
            LoadData($@"..\..\GameSavedData\Save-{choice}.xml", database);
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

        private void LoadData(string path, IGameDatabase database)
        {
            try
            {
                FileStream fs = new FileStream(path, FileMode.Open);

                BinaryFormatter formatter = new BinaryFormatter();
                
                //IGameDatabase obj = (IGameDatabase)formatter.Deserialize(fs);
                database = (IGameDatabase)formatter.Deserialize(fs);
                database.IsLoaded = true;
                //obj.IsLoaded = true;
                ViewEngine.Instance.InsideGame = true;
                
                fs.Close();

                //database.LoadData(obj);
                StartSinglePlayer(database);
            }
            catch (Exception e)
            {
                ViewEngine.Instance.DisplayMessage(e.Message);
            }
        }

        public void StartNewGame(IGameDatabase database)
        {
            database.ClearData();
            database.IsLoaded = false;
            StartSinglePlayer(database);
        }
    }
}
