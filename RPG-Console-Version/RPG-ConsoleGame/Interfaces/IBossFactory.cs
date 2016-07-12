using RPG_ConsoleGame.Models.Characters.AI.Boss;

namespace RPG_ConsoleGame.Interfaces
{
    using Characters;
    using Map;

    public interface IBossFactory
    {
        IBoss CreateBoss(Position position, char objectSymbol, string name, BossRace race);
    }
}
