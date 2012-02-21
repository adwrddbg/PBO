﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using LightStudio.PokemonBattle.Data;
using LightStudio.PokemonBattle.Interactive;

namespace LightStudio.PokemonBattle.Game
{
  public interface IPControllerEvents
  {
    void RequireInput();
    void InputSucceeded();
    void InputFailed();
    void TieRequested();
    void TieRejected();
    void TimeElapsed(int remainingSeconds);
    void TimeUp(); //这个应该是只告诉玩家本人的
  }
  public interface IPlayerController : IDisposable
  {
    Player Player { get; }
    IList<SimPokemon> ActivePokemons { get; }
    void SetSimulator(int[] ids);

    bool UseMove(Move move, Position target = null);
    bool Switch(SimPokemon withdraw, Pokemon sendout);
    bool Struggle(SimPokemon pm);
    //bool TurnLeft();
    //bool TurnRight();
    //bool MoveToCenter(OnboardPokemon pm);

    void Quit();
    bool RequestTie();
    bool AcceptTie();
    bool RejectTie();

    void AddEventsListener(IPControllerEvents listner);
  }
}
