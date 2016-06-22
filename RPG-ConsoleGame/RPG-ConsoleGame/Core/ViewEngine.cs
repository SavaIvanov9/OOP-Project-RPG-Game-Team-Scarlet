namespace RPG_ConsoleGame.Core
{
    using System;
    using System.Linq;
    using System.Text;
    using Characters;
    using Interfaces;
    using Map;
    using UserInterface;

    public delegate void OnMenuClickHandler(string selectedValue);

    public class ViewEngine : IViewEngine
    {
        private readonly IInputReader reader = new ConsoleInputReader();
        private readonly IRender render = new ConsoleRender();

        public event OnMenuClickHandler OnMenuClick;

        private void OnClick(string value)
        {
            if (OnMenuClick != null)
            {
                OnMenuClick(value);
            }
        }

       public void DrawMenu()
        {
            StringBuilder screen = new StringBuilder();

            screen.AppendLine(
                "Enter number to make your choise:" + Environment.NewLine + Environment.NewLine +
                "1. Single Player" + Environment.NewLine + Environment.NewLine +
                "2. Multiplayer" + Environment.NewLine + Environment.NewLine +
                "3. Survival Mode" + Environment.NewLine + Environment.NewLine +
                "4. Load Game" + Environment.NewLine + Environment.NewLine +
                "5. Credits");

            Console.ForegroundColor = ConsoleColor.Cyan;
            render.PrintScreen(screen);
            string choice = reader.ReadLine();
            render.WriteLine("");

            string[] validChoises = { "1", "2", "3", "4" };


            while (!validChoises.Contains(choice))
            {
                render.WriteLine("Invalid choice, please re-enter.");
                choice = reader.ReadLine();
            }

            OnClick(choice);
        }

        public IPlayer GetPlayer()
        {
            var playerName = this.GetPlayerName();
            PlayerRace race = this.GetPlayerRace();
            Player newPlayer = new Player(new Position(), 'P', playerName, race);
            Console.ForegroundColor = ConsoleColor.Green;

            return newPlayer;
        }

        private PlayerRace GetPlayerRace()
        {
            render.WriteLine("Choose a race:");
            render.WriteLine("1. Mage (damage: 50, health: 100, defense: 10)");
            render.WriteLine("2. Warrior (damage: 20, health: 300, defense: 20)");
            render.WriteLine("3. Archer (damage: 40, health: 150, defense: 15)");
            render.WriteLine("4. Rogue (damage: 30, health: 200, defense: 10)");
            render.WriteLine("5. Paladin (damage: 20, health: 180, defense: 20)");
            render.WriteLine("6. Warlock (damage: 10, health: 200, defense: 0");
            string choice = reader.ReadLine();

            string[] validChoises = { "1", "2", "3", "4", "5", "6" };

            while (!validChoises.Contains(choice))
            {
                render.WriteLine("Invalid choice of race, please re-enter.");
                choice = reader.ReadLine();
            }

            PlayerRace race = (PlayerRace)int.Parse(choice);

            return race;
        }

        private string GetPlayerName()
        {
            render.WriteLine("Please enter your name:");

            string playerName = reader.ReadLine();
            while (string.IsNullOrWhiteSpace(playerName))
            {
                render.WriteLine("Player name cannot be empty. Please re-enter.");
                playerName = reader.ReadLine();
            }

            return playerName;
        }
    }
}
