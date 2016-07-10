
namespace RPG_ConsoleGame.Models.Characters.Bots.Boss
{
    using System;
    using Interfaces;
    using Map;
    using RPG_ConsoleGame.Characters;
    using Models.Items;

    [Serializable()]
    public class Boss : Bot, IBoss
    {

        public Boss(Position position, char objectSymbol, string name, BossRace race)
            : base(position, objectSymbol, name, 0, 0, 0, 0, 0)
        {
            this.Race = race;
            this.SetPlayerStats();
        }

        public BossRace Race { get; private set; }

        public void AddItemToInventory(Item item)
        {
            Inventory.Add(item);
        }

        public override string ToString()
        {
            return string.Format(
                "Player {0} ({1}): Health ({2}),  Damage ({3}), Defence({4}), Energy ({5}), Reflexes: ({6})",
                this.Name,
                this.Race,
                this.Health,
                this.Damage,
                this.Defence,
                this.Energy,
                this.Reflexes);
        }

        private void SetPlayerStats()
        {
            switch (this.Race)
            {
                case BossRace.Boss1:
                    //abilities
                    Abilities.Add("Ability1");
                    Abilities.Add("Ability2");
                    Abilities.Add("Ability3");
                    //passives
                    //stats
                    this.Health = 800;
                    this.Damage = 100;
                    this.Defence = 10;
                    this.Energy = 100;
                    this.Reflexes = 70;
                    break;
                case BossRace.Boss2:
                    //abilities
                    Abilities.Add("Slash");
                    Abilities.Add("Enrage");
                    Abilities.Add("ShieldWall");
                    //passeives
                    //stats
                    this.Health = 800;
                    this.Damage = 100;
                    this.Defence = 10;
                    this.Energy = 100;
                    this.Reflexes = 70;
                    break;
                case BossRace.Boss3:
                    //abilities
                    Abilities.Add("MarkTarget");
                    Abilities.Add("Heavyshot");
                    Abilities.Add("Venomousarrow");
                    //passives
                    //stats
                    this.Health = 800;
                    this.Damage = 100;
                    this.Defence = 10;
                    this.Energy = 100;
                    this.Reflexes = 70;
                    break;
                case BossRace.Boss4:
                    //abilities
                    Abilities.Add("Backstab");
                    Abilities.Add("SharpenBlades");
                    Abilities.Add("Execute");
                    //passive                   
                    //stats
                    this.Health = 800;
                    this.Damage = 100;
                    this.Defence = 10;
                    this.Energy = 100;
                    this.Reflexes = 70;
                    break;
                case BossRace.Boss5:
                    //abilities
                    Abilities.Add("Smite");
                    Abilities.Add("Exorcism");
                    Abilities.Add("Heal");
                    //passive                   
                    //stats
                    this.Health = 800;
                    this.Damage = 100;
                    this.Defence = 10;
                    this.Energy = 100;
                    this.Reflexes = 70;
                    break;
                case BossRace.Boss6:
                    //abilities
                    Abilities.Add("LifeDrain");
                    Abilities.Add("LifeTap");
                    Abilities.Add("ShadowBolt");
                    //passive                   
                    //stats
                    this.Health = 800;
                    this.Damage = 100;
                    this.Defence = 10;
                    this.Energy = 100;
                    this.Reflexes = 70;
                    break;

                default:
                    throw new ArgumentException("Unknown player race.");
            }
        }

        public override string MakeDecision()
        {
            return this.Abilities[0];
        }

        public override void UseItem(int i)
        {
            
        }
    }
}