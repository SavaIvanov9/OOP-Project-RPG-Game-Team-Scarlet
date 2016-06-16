namespace RPG_ConsoleGame.Models.Characters.Abilities.Mage
{
    public abstract class MageAbilitiesProcessor
    {
        public void ProcessCommand(MageAbilitiesConstants command)
        {
            switch (command)
            {
                case MageAbilitiesConstants.Fire_Ball:
                    this.FireBall();
                    break;
                    //Other mage abilities goes here
               default:
                    break;
            }
        }

        private void FireBall()
        {
            //FireBall logic goes here
        }
    }
}
