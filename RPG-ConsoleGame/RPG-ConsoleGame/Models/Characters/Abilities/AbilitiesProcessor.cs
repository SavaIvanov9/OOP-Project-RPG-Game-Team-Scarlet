using RPG_ConsoleGame.Interfaces;

namespace RPG_ConsoleGame.Models.Characters.Abilities.Mage
{
    public class AbilitiesProcessor : IAbilitiesProcessor
    {
        public void ProcessCommand(string command, ICharacter enemy)
        {
            switch (command)
            {
                //Mage abilities
                case "Fireball":
                    this.Fireball(enemy);
                    break;
                case "Fireblast":
                    this.FireBlast(enemy);
                    break;
                case "Freeze":
                    this.Freeze(enemy);
                    break;

                //Warrior abilities
                case "Slash":
                    this.Slash(enemy);
                    break;
                case "Strike":
                    this.Strike(enemy);
                    break;
                case "Stab":
                    this.Stab(enemy);
                    break;

                //Archer abilities
                case "Firearrows":
                    this.Firearrows(enemy);
                    break;
                case "Heavyshot":
                    this.Heavyshot(enemy);
                    break;
                case "Venomousarrow":
                    this.Venomousarrow(enemy);
                    break;

                //Rogue abilities
                case "Backstab":
                    this.Backstab(enemy);
                    break;
                case "Ambush":
                    this.Ambush(enemy);
                    break;
                case "Kick":
                    this.Kick(enemy);
                    break;

               default:
                    break;
            }
        }


        //Mage
        private void Fireball(ICharacter enemy)
        {
            //FireBall logic goes here
        }

        private void FireBlast(ICharacter enemy)
        {
            // TO DO
        }

        private void Freeze(ICharacter enemy)
        {
            // TO DO
        }

        //Warrior
        private void Slash(ICharacter enemy)
        {
            //TO DO
        }

        private void Strike(ICharacter enemy)
        {
            //TO DO
        }

        private void Stab(ICharacter enemy)
        {
            //TO DO
        }

        //Archer
        private void Firearrows(ICharacter enemy)
        {
            // to do
        }

        private void Heavyshot(ICharacter enemy)
        {
            // to do
        }

        private void Venomousarrow(ICharacter enemy)
        {
            // to do
        }

        private void Backstab(ICharacter enemy)
        {
            // to do
        }

        private void Ambush(ICharacter enemy)
        {
            // to do
        }

        private void Kick(ICharacter enemy)
        {
            // to do
        }

    }
}
