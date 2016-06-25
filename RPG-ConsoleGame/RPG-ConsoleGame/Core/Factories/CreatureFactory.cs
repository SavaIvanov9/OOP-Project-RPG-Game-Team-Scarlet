namespace RPG_ConsoleGame.Core.Factories
{
    using Characters;
    using Interfaces;
    using Map;
    using Models.Characters.Bots.Creature;

    //Factory Pattern
    public class CreatureFactory : ICreatureFactory
    {
        public ICreature CreateCreature(Position position, char objectSymbol, string name, PlayerRace race)
        {
            var creature = new Creature(position, objectSymbol, name, race);

            return creature;
        }
    }
}
