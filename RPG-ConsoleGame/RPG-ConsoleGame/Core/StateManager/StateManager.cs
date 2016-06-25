namespace RPG_ConsoleGame.Core.StateManager
{
    using Engines;

    public class StateManager 
    {
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
                    BackEngine.Instance.StartSinglePlayer();
                    break;
                case StateConstants.Multiplayer:
                    BackEngine.Instance.StartSinglePlayer();
                    break;
                case StateConstants.SurvivalMode:
                    BackEngine.Instance.StartSinglePlayer();
                    break;
                case StateConstants.LoadGame:
                    BackEngine.Instance.LoadGame();
                    break;
                case StateConstants.Credits:
                    ViewEngine.Instance.RenderCredits();
                    ViewEngine.Instance.RenderMenu();
                    break;
                case StateConstants.NewGame:
                    BackEngine.Instance.StartNewGame();
                    break;
                default:
                    break;
            }
        }
    }
}
