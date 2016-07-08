namespace WindowsFormsApplication1.Core.Factories
{
    using Interfaces;
    using Map;
    using Models.Characters.PlayerControlled;
          
    //Factory Pattern
    public class PlayerFactory : IPlayerFactory
    {
        public IPlayer CreatePlayer(Position position, char objectSymbol, string name, PlayerRace race)
        {
            var player = new Player(position, objectSymbol, name, race);

            return player;
        }
    }
}
