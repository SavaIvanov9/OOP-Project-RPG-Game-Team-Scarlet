namespace RPG_ConsoleGame.Models.Characters.Bots
{
    using System;
    using RPG_ConsoleGame.Characters;
    using Interfaces;
    using Map;

    [Serializable()]
    public abstract class Bot : Character, IBot
    {
        protected Bot(Position position, char objectSymbol, string name, int health,
            int damage, int defence, int energy, int reflexes)
            : base(position, objectSymbol, name, health, damage, defence, energy, reflexes)
        {
        }

        public virtual string MakeDecision()
        {
            string decision = "";
            return decision;
        }
    }
}
