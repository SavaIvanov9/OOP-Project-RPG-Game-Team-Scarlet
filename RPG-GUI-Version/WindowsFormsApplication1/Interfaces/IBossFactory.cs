namespace WindowsFormsApplication1.Interfaces
{
    using Map;
    using Models.Characters.AI.Boss;
                                   
    public interface IBossFactory
    {
        IBoss CreateBoss(Position position, char objectSymbol, string name, BossRace race);
    }
}
