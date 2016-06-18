namespace RPG_ConsoleGame.Core.StateManager
{
    public class StateManager
    {
        public void ProcessCommand(string command)
        {
            switch (command)
            {
                case StateConstants.SinglePlayer:
                    StartSinglePlayer();
                    break;
                default:
                    break;
            }
        }

        private void DoMenuAction(string menuAction)
        {
            // Do some stuff based on user choice
        }
        
        private void StartSinglePlayer()
        {
            var ve = new ViewEngine();

            ve.OnMenuClick += DoMenuAction;
        }
    }
}
