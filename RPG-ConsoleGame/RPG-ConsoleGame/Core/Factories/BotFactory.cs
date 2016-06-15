namespace RPG_ConsoleGame.Core.Factories
{
    using Characters;
    using Interfaces;
    using Map;

    public class BotFactory : IBotFactory
    {
        public IBot CreateBot(Position position, char objectSymbol, string name, PlayerRace race)
        {
            var bot = new Bot(position, objectSymbol, name, race);

            return bot;
        }
    }
}
