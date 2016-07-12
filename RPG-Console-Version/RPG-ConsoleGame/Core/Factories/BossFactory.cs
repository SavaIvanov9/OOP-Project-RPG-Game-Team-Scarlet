namespace RPG_ConsoleGame.Core.Factories
{
    using Models.Characters.AI.Boss;
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