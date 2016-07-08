namespace WindowsFormsApplication1.Core.StateManager
{
    using Engines;

    public class StateManager
    {
        //private static readonly IGameDatabase database = new GameDatabase();

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
                    BackEngine.Instance.StartNewSinglePlayer();
                    break;
                case StateConstants.NewMultiplayer:
                    //ViewEngine.Instance.InsideGame = true;
                    BackEngine.Instance.StartMultiPlayer();
                    break;
                case StateConstants.NewSurvival:
                    //ViewEngine.Instance.InsideGame = true;
                    BackEngine.Instance.StartNewSinglePlayer();
                    break;
                case StateConstants.SinglePlayer:
                    //ViewEngine.Instance.InsideGame = true;
                    BackEngine.Instance.StartSinglePlayer();
                    break;
                case StateConstants.Multiplayer:
                    //ViewEngine.Instance.InsideGame = true;
                    BackEngine.Instance.StartMultiPlayer();
                    break;
                case StateConstants.SurvivalMode:
                    //ViewEngine.Instance.InsideGame = true;
                    BackEngine.Instance.StartSinglePlayer();
                    break;
                case StateConstants.SaveGame:
                    BackEngine.Instance.SaveGame("save");
                    break;
                case StateConstants.LoadGame:
                    //ViewEngine.Instance.InsideGame = true;
                    BackEngine.Instance.LoadGame();
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
