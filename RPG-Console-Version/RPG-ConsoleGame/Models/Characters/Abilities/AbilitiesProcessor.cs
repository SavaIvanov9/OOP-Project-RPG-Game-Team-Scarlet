using RPG_ConsoleGame.Exceptions;

namespace RPG_ConsoleGame.Models.Characters.Abilities
{
    using Interfaces;

    public class AbilitiesProcessor : IAbilitiesProcessor
    {
        public void ProcessCommand(string command, ICharacter player, ICharacter enemy)
        {
            switch (command)
            {
                //Mage abilities
                case "Arcane Blast":
                    this.ArcaneBlast(player, enemy);
                    break;
                case "Hellfire":
                    this.Hellfire(player, enemy);
                    break;
                case "Mana Shield":
                    this.ManaShield(player);
                    break;
                case "Recharge energy":
                    this.RechargeEnergy(player);
                    break;

                //Warrior abilities
                case "Slash":
                    this.Slash(player, enemy);
                    break;
                case "Bleeding Wounds":
                    this.BleedingWounds(player, enemy);
                    break;
                case "Enrage":
                    this.Enrage(player);
                    break;
                case "Regenerate":
                    this.Regenerate(player);
                    break;

                //Archer abilities
                case "Heavyshot":
                    this.Heavyshot(player, enemy);
                    break;
                case "Venomous Arrow":
                    this.VenomousArrow(player, enemy);
                    break;
                case "Aim":
                    this.Aim(enemy);
                    break;
                case "Activate Critical Shot":
                    this.ActivateCriticalShot();
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
                case "Disable":
                    this.Disable(player, enemy);
                    break;

                //Paladin abilities
                case "Smite":
                    this.Smite(player, enemy);
                    break;
                case "Righteous Strike":
                    this.RighteousStrike(player, enemy);
                    break;
                case "Heal":
                    this.Heal(player);
                    break;
                case "Divine Shield":
                    this.DivineShield();
                    break;

                //Warlock abilities
                case "ShadowBolt":
                    this.ShadowBolt(player, enemy);
                    break;
                case "Shadow Curse":
                    ShadowCurse();
                    break;
                case "LifeDrain":
                    this.LifeDrain(player, enemy);
                    break;
                case "LifeTap":
                    this.LifeTap(player);
                    break;

                //BOSS1 abilities
                case "Ability1":
                    this.Ability1(player, enemy);
                    break;
                case "Ability2":
                    this.Ability2(player, enemy);
                    break;
                case "Ability3":
                    this.Ability3(player, enemy);
                    break;
                //TODO more bosses

                default:
                    break;
            }
        }

        //Mage
        private void ArcaneBlast(ICharacter player, ICharacter enemy)
        {
            if (player.Energy >= 20)
            {
                player.Energy -= 20;
                enemy.Health -= (player.Damage + 50);
            }
            else
            {
                throw new OutOfAmountException("Energy not enough to cast ability");
            }
        }

        private void Hellfire(ICharacter player, ICharacter enemy)
        {
            if (player.Energy >= 20)
            {
                player.Energy -= 20;
                enemy.Health -= (player.Damage + 25);
            }
            else
            {
                throw new OutOfAmountException("Energy not enough to cast ability");
            }
        }

        private void ManaShield(ICharacter player)
        {
            if (player.Energy >= 20)
            {
                player.Energy -= 20;
                player.Health += 100;
            }
            else
            {
                throw new OutOfAmountException("Energy not enough to cast ability");
            }
        }

        private void RechargeEnergy(ICharacter character)
        {
            character.Energy += 100;
        }

        //Warrior
        private void Slash(ICharacter player, ICharacter enemy)
        {
            if (player.Energy >= 20)
            {
                player.Energy -= 20;
                enemy.Health -= player.Damage + 25;
            }
            else
            {
                throw new OutOfAmountException("Energy not enough to cast ability");
            }
        }

        private void BleedingWounds(ICharacter player, ICharacter enemy)
        {
            if (player.Energy >= 20)
            {
                player.Energy -= 20;
                enemy.Health -= player.Damage / 2;
                //TO DO add dot dmg
            }
            else
            {
                throw new OutOfAmountException("Energy not enough to cast ability");
            }
        }

        private void Enrage(ICharacter player)
        {
            if (player.Energy >= 10)
            {
                player.Energy -= 10;
                player.Health -= 50;
                player.Damage *= 2;
            }
            else
            {
                throw new OutOfAmountException("Energy not enough to cast ability");
            }
        }

        private void Regenerate(ICharacter player)
        {
            player.Energy += 50;
            player.Health += 100;
        }
        //TO ADD WARRIOR PASSIVE(LAST STAND)

        //Archer
        private void Heavyshot(ICharacter player, ICharacter enemy)
        {
            if (player.Energy >= 20)
            {
                player.Energy -= 20;
                enemy.Health -= (player.Damage + 100);
            }
            else
            {
                throw new OutOfAmountException("Energy not enough to cast ability");
            }
        }

        private void VenomousArrow(ICharacter player, ICharacter enemy)
        {
            if (player.Energy >= 20)
            {
                player.Energy -= 20;
                enemy.Health -= player.Damage / 2;
                //TO DO POISON EFFECT
            }
            else
            {
                throw new OutOfAmountException("Energy not enough to cast ability");
            }
        }

        private void Aim(ICharacter character)
        {
            character.Energy += 50;
            character.Damage += 50;
        }

        private void ActivateCriticalShot()
        {
            //TODO: add crit buff
        }
        //TO ADD ARCHER PASSIVE(HEADSHOT)

        // Rogue
        private void Backstab(ICharacter player, ICharacter enemy)
        {
            if (player.Energy >= 40)
            {
                player.Energy -= 40;
                enemy.Health -= (player.Damage * 2);
            }
            else
            {
                throw new OutOfAmountException("Energy not enough to cast ability");
            }
        }

        private void SharpenBlades(ICharacter player)
        {
                player.Energy += 20;
                player.Damage += 50;
        }

        private void Execute(ICharacter player, ICharacter enemy)
        {
            if (player.Energy >= 20)
            {
                player.Energy -= 20;

                if (enemy.Health <= 300)
                {
                    enemy.Health -= player.Damage * 3;
                }
                else
                {
                    enemy.Health -= player.Damage;
                }
            }
            else
            {
                throw new OutOfAmountException("Energy not enough to cast ability");
            }
        }

        private void Disable(ICharacter player, ICharacter enemy)
        {
            if (player.Energy >= 20)
            {
                player.Energy -= 20;
                //TODO add -30% debuff on enemy
            }
            else
            {
                throw new OutOfAmountException("Energy not enough to cast ability");
            }
        }
        //TO ADD ROGUE PASSIVE (POISON)

        //Paladin
        private void Smite(ICharacter player, ICharacter enemy)
        {
            if (player.Energy >= 20)
            {
                player.Energy -= 20;

                enemy.Health -= (player.Damage + 50);
            }
            else
            {
                throw new OutOfAmountException("Energy not enough to cast ability");
            }
        }

        private void RighteousStrike(ICharacter player, ICharacter enemy)
        {
            enemy.Health -= (player.Damage / 2);

            player.Energy += player.Damage;
        }

        private void Heal(ICharacter player)
        {
            if (player.Energy >= 30)
            {
                player.Health += player.Damage * 2;

                player.Energy -= 30;
            }
            else
            {
                throw new OutOfAmountException("Energy not enough to cast ability");
            }
        }

        private void DivineShield()
        {
            //TODO add buff
        }
        //TO ADD PASSIVE ABILITY(HolyRegeneration)

        //Warlock
        private void LifeDrain(ICharacter player, ICharacter enemy)
        {
            if (player.Energy >= 20)
            {
                player.Energy -= 20;

                player.Health += player.Damage;

                enemy.Health -= player.Damage;
            }
            else
            {
                throw new OutOfAmountException("Energy not enough to cast ability");
            }
        }

        private void ShadowBolt(ICharacter player, ICharacter enemy)
        {
            if (player.Energy >= 20)
            {
                enemy.Health -= (player.Damage + 200);

                player.Health -= player.Damage;

                player.Energy -= 20;
            }
            else
            {
                throw new OutOfAmountException("Energy not enough to cast ability");
            }
        }

        private void ShadowCurse()
        {
            //todo add dot dmg
        }

        private void LifeTap(ICharacter player)
        {
            player.Health -= player.Damage;

            player.Energy += player.Damage * 2;
        }
        //TO ADD PASSIVE ABILITY (ImmortalImp)

        //Boss1
        private void Ability1(ICharacter player, ICharacter enemy)
        {
            if (player.Energy >= 20)
            {
                player.Energy -= 20;
                enemy.Health -= (player.Damage + 40);
            }
            else
            {
                throw new OutOfAmountException("Energy not enough to cast ability");
            }
        }

        private void Ability2(ICharacter player, ICharacter enemy)
        {
            if (player.Energy >= 20)
            {
                player.Energy -= 20;
                enemy.Health -= (player.Damage + 15);
            }
            else
            {
                throw new OutOfAmountException("Energy not enough to cast ability");
            }
        }

        private void Ability3(ICharacter player, ICharacter enemy)
        {
            if (player.Energy >= 20)
            {
                player.Energy -= 20;
                enemy.Health -= enemy.Damage;
                player.Health += enemy.Damage;
            }
            else
            {
                throw new OutOfAmountException("Energy not enough to cast ability");
            }
        }
    }
}
