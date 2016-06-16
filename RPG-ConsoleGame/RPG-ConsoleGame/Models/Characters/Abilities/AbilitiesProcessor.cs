using RPG_ConsoleGame.Interfaces;

namespace RPG_ConsoleGame.Models.Characters.Abilities.Mage
{
    public class AbilitiesProcessor : IAbilitiesProcessor
    {
        public void ProcessCommand(string command)
        {
            switch (command)
            {
                case "FireBall":
                    this.Fireball();
                    break;
                    //Other mage abilities goes here
               default:
                    break;
            }
        }

        private void Fireball()
        {
            //FireBall logic goes here
        }
    }
}
