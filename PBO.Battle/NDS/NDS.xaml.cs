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
using System.Collections.ObjectModel;
using LightStudio.PokemonBattle.Data;
using LightStudio.PokemonBattle.Game;
using LightStudio.PokemonBattle.PBO.Battle.VM;

namespace LightStudio.PokemonBattle.PBO.Battle
{
  /// <summary>
  /// Interaction logic for Simulation.xaml
  /// </summary>
  public partial class NDS : UserControl
  {
    public NDS()
    {
      InitializeComponent();
    }

    internal void Init(Room.IUserController userController)
    {
      var game = userController.Game;
      opms.ItemsSource = game.Board.Teams[userController.PlayerController.Player.TeamId];
      rpms.ItemsSource = game.Board.Teams[1 - userController.PlayerController.Player.TeamId];
      IControlPanel controlPanel;
      switch (game.Settings.Mode)
      {
        case GameMode.Single:
          controlPanel = new Singles(userController);
          break;
        default:
          System.Diagnostics.Debugger.Break();
          controlPanel = null;
          break;
      }
      cp.Init(controlPanel);
      subtitle.Init(controlPanel);
      board.Init(game.Board, userController.PlayerController.Player.TeamId);

      //game.RegisterUI(subtitle);
      //cp.Init(game.ControlPanel);
      controlPanel.PropertyChanged += (sender, e) =>
        {
          const string CP = "ControllingPokemon";
          if (e.PropertyName == null || e.PropertyName == CP)
          {
            var p = (sender as IControlPanel).ControllingPokemon;
            if (p == null) opms.SelectedIndex = -1;
            else opms.SelectedIndex = p.Position.X;
          }
        };
    }
    internal void Init(IPlayerController pc)
    {
    }
  }
}
