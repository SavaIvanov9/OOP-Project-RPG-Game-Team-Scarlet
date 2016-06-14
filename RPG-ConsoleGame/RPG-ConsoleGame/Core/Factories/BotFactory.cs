namespace RPG_ConsoleGame.Core.Factories
{
    using Characters;
    using Interfaces;
    using Map;

    public class BotFactory : IBotFactory
    {
        public ICharacter CreateBot(Position position, char objectSymbol, string name, PlayerClass race)
        {
            ICharacter bot = new Bot(position, objectSymbol, name, race);

            return bot;
        }
    }
}
