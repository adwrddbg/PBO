﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using LightStudio.PokemonBattle.Data;
using LightStudio.PokemonBattle.Interactive;

namespace LightStudio.PokemonBattle.Game
{
  public interface IGame
  {
    event Action<int, int> GameEnd;
    event Action<Turn> Turn;
    event Action<Player> RequireInput;

    bool Prepared { get; }
    GameSettings Settings { get; }

    bool Start();
    bool SetPlayer(int teamId, int userId, PokemonCustomInfo[] pokemons);
    Player GetPlayer(int id);
    bool InputAction(int player, ActionInput action);
    Turn GetLastLeapTurn();
  }
  public static class GameFacade
  {
    public static IGame CreateGame(GameSettings settings)
    {
      return new GameContext(settings);
    }
  }
}
