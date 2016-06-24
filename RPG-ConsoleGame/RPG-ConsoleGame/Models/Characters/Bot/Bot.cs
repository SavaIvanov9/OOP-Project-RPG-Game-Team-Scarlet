namespace RPG_ConsoleGame.Models.Characters.Bot
{
    using RPG_ConsoleGame.Characters;
    using Interfaces;
    using Map;

    public abstract class Bot : Character, IBot
    {
        protected Bot(Position position, char objectSymbol, string name,
            int damage, int health, int defence, int reflexes)
            : base(position, objectSymbol, name, damage, health, defence, reflexes)
        {
        }

        public virtual string MakeDecision()
        {
            string decision = "";
            return decision;
        }
    }
}
