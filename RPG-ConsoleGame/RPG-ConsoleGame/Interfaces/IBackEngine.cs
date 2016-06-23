namespace RPG_ConsoleGame.Interfaces
{
    using System.Text;
    using Sound;

    public interface IBackEngine
    {
        void CheckForBattle(ICharacter char1, ICharacter char2);
        void RegenerateStats(ICharacter player);
        void ExecuteBotDecision(int turnsCount, ICharacter char2, ICharacter char1, StringBuilder history);
        void ExecutePlayerAbility(string ability, ICharacter player, ICharacter enemy, int turn, StringBuilder history);
        void RemoveDead(IGameDatabase database);
        void ReturnBack(string command);
        void StartMusic(SoundEffects stage);
        void StartSinglePlayer();
        //void StartTimer(int seconds);
    }
}
