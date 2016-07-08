namespace WindowsFormsApplication1.Models.Characters.AI
{
    using System;
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
