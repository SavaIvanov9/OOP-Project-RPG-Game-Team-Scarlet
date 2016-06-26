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
                case StateConstants.SinglePlayer:
                    BackEngine.Instance.StartSinglePlayer(database);
                    break;
                case StateConstants.LoadGame:
                    BackEngine.Instance.LoadGame(database);
                    break;
                case StateConstants.Multiplayer:
                    BackEngine.Instance.StartSinglePlayer(database);
                    break;
                case StateConstants.SurvivalMode:
                    BackEngine.Instance.StartSinglePlayer(database);
                    break;
              case StateConstants.Credits:
                    ViewEngine.Instance.RenderCredits();
                    ViewEngine.Instance.RenderMenu();
                    break;
                case StateConstants.NewGame:
                    BackEngine.Instance.StartNewGame(database);
                    break;
                case StateConstants.SaveGame:
                    BackEngine.Instance.SaveGame("save", database);
                    break;
                default:
                    break;
            }
        }
    }
}
