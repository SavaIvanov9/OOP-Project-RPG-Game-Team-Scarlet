using RPG_ConsoleGame.Interfaces;

namespace RPG_ConsoleGame.Models.Characters.Abilities.Mage
{
    public class AbilitiesProcessor : IAbilitiesProcessor
    {
        public void ProcessCommand(string command, ICharacter player, ICharacter enemy)
        {
            switch (command)
            {
                //Mage abilities
                case "Fireball":
                    this.Fireball(player, enemy);
                    break;
                case "Hellfire":
                    this.Hellfire(player, enemy);
                    break;
                case "Reflect":
                    this.Reflect(player, enemy);
                    break;

                //Warrior abilities
                case "Slash":
                    this.Slash(player, enemy);
                    break;
                case "Enrage":
                    this.Enrage(player);
                    break;
                case "ShieldWall":
                    this.ShieldWall(player);
                    break;

                //Archer abilities
                case "Firearrows":
                    this.MarkTarget(enemy);
                    break;
                case "Heavyshot":
                    this.Heavyshot(player, enemy);
                    break;
                case "Venomousarrow":
                    this.Venomousarrow(player, enemy);
                    break;

                //Rogue abilities
                case "Backstab":
                    this.Backstab(player, enemy);
                    break;
                case "SharpenBlades":
                    this.SharpenBlades(player);
                    break;
                case "Execute":
                    this.Execute(player, enemy);
                    break;

                    //Paladin abilities
                case "Smite":
                    this.Smite(player, enemy);
                    break;
                case "Exorcism":
                    this.Exorcism(player, enemy);
                    break;
                case "Heal":
                    this.Heal(player);
                    break;

                    //Warlock abilities
                case "LifeDrain":
                    this.LifeDrain(player, enemy);
                    break;
                case "LifeTap":
                    this.LifeTap(player);
                    break;
                case "ShadowBolt":
                    this.ShadowBolt(player, enemy);
                    break;

                default:
                    break;
            }
        }
        //Mage
        private void Fireball(ICharacter player, ICharacter enemy)
        {
            player.Reflexes -= 20;
            enemy.Health -= (player.Damage + 40);
        }

        private void Hellfire(ICharacter player, ICharacter enemy)
        {
            player.Reflexes -= 20;
            enemy.Health -= (player.Damage + 15);
            // TO ADD BURN EFFECT
        }

        private void Reflect(ICharacter player, ICharacter enemy)
        {
            player.Reflexes -= 20;
            enemy.Health -= enemy.Damage;
            player.Health += enemy.Damage;
        }
        //TO ADD MAGE PASSIVE(MANA SHIELD)

        //Warrior
        private void Slash(ICharacter player, ICharacter enemy)
        {
            player.Reflexes -= 20;
            enemy.Health -= player.Damage + 10 - enemy.Defense;
        }

        private void Enrage(ICharacter player)
        {
            player.Reflexes -= 10;
            player.Damage *= 2;
        }

        private void ShieldWall(ICharacter player)
        {
            player.Reflexes -= 10;
            player.Defense += 10;
        }
        //TO ADD WARRIOR PASSIVE(LAST STAND)
        //Archer
        private void MarkTarget(ICharacter enemy)
        {
            enemy.Defense -= 15;
        }

        private void Heavyshot(ICharacter player, ICharacter enemy)
        {
            enemy.Health -= (player.Damage + 10);
        }

        private void Venomousarrow(ICharacter player, ICharacter enemy)
        {
            enemy.Health -= player.Damage;
            //TO DO POISON EFFECT
        }
        //TO ADD ARCHER PASSIVE(HEADSHOT)
        // Rogue
        private void Backstab(ICharacter player, ICharacter enemy)
        {
            enemy.Health -= (player.Damage * 2);
        }

        private void SharpenBlades(ICharacter player)
        {
            player.Damage += 15;
        }

        private void Execute(ICharacter player, ICharacter enemy)
        {
            //enemy.Health -= player.Damage*Round;
        }
        //TO ADD ROGUE PASSIVE (POISON)

        //Paladin
        private void Smite(ICharacter player, ICharacter enemy)
        {
            player.Health += 20;
            enemy.Health -= (player.Damage + 10);
            if (player.Health > 180)
                player.Health = 180;
        }
        private void Exorcism(ICharacter player, ICharacter enemy)
        {
            //Aura spell
            enemy.Health -= (player.Damage / 2 + 5);
            //TO ADD SELF DMG PER ROUND(To nullify the effect of the passive aura)
        }
        private void Heal(ICharacter player)
        {
            player.Health += 70;
            if (player.Health > 180)
                player.Health = 180;
        }
        //TO ADD PASSIVE ABILITY(HolyRegeneration)

        //Warlock
        private void LifeDrain(ICharacter player, ICharacter enemy)
        {
            //PER ROUND ENEMY DAMAGE AND SELF HEAL
        }
        private void LifeTap(ICharacter player)
        {
            player.Health -= 10;
            //TO ADD MANA REGEN
        }
        private void ShadowBolt(ICharacter player, ICharacter enemy)
        {
            enemy.Health -= (player.Damage + 40);
        }
        //TO ADD PASSIVE ABILITY (ImmortalImp)
    }
}
