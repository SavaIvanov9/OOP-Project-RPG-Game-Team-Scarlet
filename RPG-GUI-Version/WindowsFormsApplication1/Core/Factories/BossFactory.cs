namespace WindowsFormsApplication1.Core.Factories
{
    using Interfaces;
    using Map;
    using Models.Characters.AI.Boss;

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