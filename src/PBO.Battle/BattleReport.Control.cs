﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using LightStudio.PokemonBattle.Data;
using LightStudio.PokemonBattle.Game;
using LightStudio.PokemonBattle.Interactive;

namespace LightStudio.PokemonBattle.PBO.Battle
{
  /// <summary>
  /// Interaction logic for BattleReport.xaml
  /// </summary>
  public partial class BattleReport : UserControl
  {
    private class Control : IGameEventListener
    {
      IDictionary<string, object> dictionary;
      //BlockTranslator translator;
      BattleReport nest;

      public Control(BattleReport battlereport)
      {
        this.nest = battlereport;
      }

      void IGameEventListener.EventOccurred(GameEvent e)
      {
      //  string expression = e.GetGameLogExpression();
      //  if (!string.IsNullOrEmpty(expression))
      //  {
      //    Block b = new Paragraph(new Run(expression + e.GetGameLogArgs().ToString()));
      //    nest.AddBlock(b);
      //    if (e.GetType().Name == "BeginTurn")
      //    {
      //      nest.nowTurn = new LinkedListNode<Block>(b);
      //      nest.turnsBookmark.AddLast(nest.nowTurn);
      //    }
      //  }
      }
    }
  }
}