namespace RPG_ConsoleGame.Models.Characters.AI.Boss
{
    using System;
    using Interfaces;
    using Map;
    using Items;
    using AI;

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
                    Abilities.Add("Arcane Blast"); //direct dmg
                    Abilities.Add("Hellfire"); //DOT dmg
                    Abilities.Add("Mana Shield"); //shield + some hp
                    Abilities.Add("Recharge energy"); //add mana
                    //passives
                    //stats
                    this.Health = 600;
                    this.Damage = 150;
                    this.Defence = 0;
                    this.Energy = 200;
                    this.Reflexes = 80;
                    break;
                case BossRace.Boss2:
                    //abilities
                    Abilities.Add("Slash"); //direct dmg
                    Abilities.Add("Bleeding Wounds"); //dot dmg
                    Abilities.Add("Enrage"); //add dmg
                    Abilities.Add("Regenerate"); //recover some hp + some energy
                    //passeives
                    //stats
                    this.Health = 1500;
                    this.Damage = 50;
                    this.Defence = 0;
                    this.Energy = 100;
                    this.Reflexes = 50;
                    break;
                case BossRace.Boss3:
                    //abilities
                    Abilities.Add("Heavyshot"); //direct dmg
                    Abilities.Add("Venomous Arrow"); //dot dmg
                    Abilities.Add("Aim"); //add dmg
                    Abilities.Add("Activate Critical Shot"); //add chance to do 1.5x dmg
                    //passives
                    //stats
                    this.Health = 800;
                    this.Damage = 100;
                    this.Defence = 0;
                    this.Energy = 100;
                    this.Reflexes = 70;
                    break;
                case BossRace.Boss4:
                    //abilities
                    Abilities.Add("Backstab"); //direct dmg
                    Abilities.Add("SharpenBlades"); //add dmg + energy
                    Abilities.Add("Execute"); //if enemy under 50% hp, deal dmg x2
                    Abilities.Add("Disable"); //enemy deal -30% dmg
                    //passive                   
                    //stats
                    this.Health = 900;
                    this.Damage = 80;
                    this.Defence = 0;
                    this.Energy = 100;
                    this.Reflexes = 70;
                    break;
                case BossRace.Boss5:
                    //abilities
                    Abilities.Add("Smite"); //direct dmg
                    Abilities.Add("Righteous Strike"); //direct dmg + heal
                    Abilities.Add("Heal"); //heal
                    Abilities.Add("Divine Shield"); //add a lot hp for 1 turn
                    //passive                   
                    //stats
                    this.Health = 1000;
                    this.Damage = 50;
                    this.Defence = 0;
                    this.Energy = 100;
                    this.Reflexes = 60;
                    break;
                case BossRace.Boss6:
                    //abilities
                    Abilities.Add("ShadowBolt"); //direct dmg, -hp, -en
                    Abilities.Add("Shadow Curse"); //dot
                    Abilities.Add("LifeDrain"); //dmg + heal
                    Abilities.Add("LifeTap"); //convert hp to en
                    //passive                   
                    //stats
                    this.Health = 1200;
                    this.Damage = 50;
                    this.Defence = 0;
                    this.Energy = 100;
                    this.Reflexes = 60;
                    break;

                default:
                    throw new ArgumentException("Unknown player race.");
            }
        }

        //public override string MakeDecision()
        //{
        //    return this.Abilities[0];
        //}

        public override void UseItem(int i)
        {
            
        }
    }
}