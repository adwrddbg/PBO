﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using LightStudio.PokemonBattle.Interactive;

namespace LightStudio.PokemonBattle.Game
{
  /// <summary>
  /// 只能有一个Listner卡住线程，建议是Subtitle
  /// </summary>
  public interface IGameEventListener
  {
    void EventOccurred(GameEvent e);
  }
  public class GameOutward
  {
    /// <summary>
    /// game start, or an observer
    /// </summary>
    public event Action LeapTurn;
    public readonly GameSettings Settings;
    public readonly BoardOutward Board;
    public readonly TeamOutward[] Teams;
    private readonly List<IGameEventListener> listeners;

    public GameOutward(GameSettings settings)
    {
      Settings = settings;
      Board = new BoardOutward(Settings);
      Teams = new TeamOutward[Settings.TeamCount];
      for (int t = 0; t < Settings.TeamCount; t++)
        Teams[t] = new TeamOutward(6, 0, 0);
      listeners = new List<IGameEventListener>();
    }

    public void Update(Turn turn)
    {
      if (turn.Teams != null)
      {
        for (int t = 0; t < Settings.TeamCount; t++)
        {
          Teams[t].Update(turn.Teams[t]);
          for (int x = 0; x < Settings.XBound; x++)
            Board[t, x] = turn[t, x];
          Board.Weather = turn.Weather;
        }
        LeapTurn();
      }
#warning GameEvents
      foreach (GameEvent e in turn.Events)
      {
        e.Update(this);
        foreach (IGameEventListener l in listeners)
          l.EventOccurred(e);
      }
    }

    public void AddListner(IGameEventListener listener)
    {
      if (listener != null) listeners.Add(listener);
    }
  }
}
