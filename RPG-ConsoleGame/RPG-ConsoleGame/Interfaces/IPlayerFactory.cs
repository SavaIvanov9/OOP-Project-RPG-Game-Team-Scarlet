﻿namespace RPG_ConsoleGame.Interfaces
{
    using Characters;
    using Map;

    interface IPlayerFactory
    {
        IPlayer CreatePlayer(Position position, char objectSymbol, string name, PlayerRace race);
    }
}
