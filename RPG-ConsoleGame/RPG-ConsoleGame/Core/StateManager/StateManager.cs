using RPG_ConsoleGame.Interfaces;

namespace RPG_ConsoleGame.Core.StateManager
{
    using Engines;

    public class StateManager
    {
        private readonly IGameDatabase database = new GameDatabase();

        public StateManager()
        {
            ViewEngine.Instance.OnMenuClick += StartState;
        }

        //Singleton patern
        private static StateManager instance;

        public static StateManager Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new StateManager();
                }

                return instance;
            }
        }

        public void StartState(string state)
        {
            switch (state)
            {
                case StateConstants.BeginGame:
                    ViewEngine.Instance.RenderMenu();
                    break;
                case StateConstants.NewSinglePlayer:
                    //ViewEngine.Instance.InsideGame = true;
                    BackEngine.Instance.StartNewSinglePlayer(database);
                    break;
                case StateConstants.NewMultiplayer:
                    //ViewEngine.Instance.InsideGame = true;
                    BackEngine.Instance.StartMultiPlayer(database);
                    break;
                case StateConstants.NewSurvival:
                    //ViewEngine.Instance.InsideGame = true;
                    BackEngine.Instance.StartNewSinglePlayer(database);
                    break;
                case StateConstants.SinglePlayer:
                    //ViewEngine.Instance.InsideGame = true;
                    BackEngine.Instance.StartSinglePlayer(database);
                    break;
                case StateConstants.Multiplayer:
                    //ViewEngine.Instance.InsideGame = true;
                    BackEngine.Instance.StartLoadedGame(database);
                    break;
                case StateConstants.SurvivalMode:
                    //ViewEngine.Instance.InsideGame = true;
                    BackEngine.Instance.StartSinglePlayer(database);
                    break;
                case StateConstants.SaveGame:
                    BackEngine.Instance.SaveGame("save", database);
                    break;
                case StateConstants.LoadGame:
                    //ViewEngine.Instance.InsideGame = true;
                    BackEngine.Instance.LoadGame(database);
                    break;
                case StateConstants.Credits:
                    ViewEngine.Instance.RenderCredits();
                    ViewEngine.Instance.RenderMenu();
                    break;
                default:
                    break;
            }
        }
    }
}
