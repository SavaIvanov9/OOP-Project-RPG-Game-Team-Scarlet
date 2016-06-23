using System.Collections.Generic;
using RPG_ConsoleGame.Models.Buildings;
using RPG_ConsoleGame.Items;
namespace RPG_ConsoleGame.Core.StateManager
{
    using Interfaces;
    using Engines;

    public class StateManager : IStateManager
    {
        private readonly IViewEngine viewEngine = new ViewEngine();
        private readonly IBackEngine backEngine = new BackEngine();

        public StateManager()
        {
            viewEngine.OnMenuClick += StartState;
        }

        public void StartState(string state)
        {
            switch (state)
            {
                case StateConstants.BeginGame:
                    viewEngine.RenderMenu();
                    break;
                case StateConstants.SinglePlayer:
                    backEngine.StartSinglePlayer();
                    break;
                case StateConstants.Multiplayer:
                    backEngine.StartSinglePlayer();
                    break;
                case StateConstants.SurvivalMode:
                    backEngine.StartSinglePlayer();
                    break;
                case StateConstants.LoadGame:
                    backEngine.StartSinglePlayer();
                    break;
                case StateConstants.Credits:
                    viewEngine.RenderCredits();
                    viewEngine.RenderMenu();
                    break;
                default:
                    break;
            }
        }
    }
}
