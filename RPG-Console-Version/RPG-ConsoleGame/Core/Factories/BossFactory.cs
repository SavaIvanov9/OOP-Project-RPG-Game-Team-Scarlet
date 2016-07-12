using RPG_ConsoleGame.Models.Characters.AI.Boss;

namespace RPG_ConsoleGame.Core.Factories
{
    using Interfaces;
    using Map;

    //Factory Pattern
    public class BossFactory : IBossFactory
    {
        public IBoss CreateBoss(Position position, char objectSymbol, string name, BossRace race)
        {
            var boss = new Boss(position, objectSymbol, name, race);

            return boss;
        }
    }
}