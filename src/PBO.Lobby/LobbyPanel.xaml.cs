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
using System.Windows.Shapes;
using LightStudio.PokemonBattle.PBO.UIElements;

namespace LightStudio.PokemonBattle.PBO.Lobby
{
  /// <summary>
  /// Interaction logic for LobbyWindow.xaml
  /// </summary>
  public partial class LobbyPanel : UserControl
  {
    public event Action<Room.IUserController> EnterSucceed;
    
    public LobbyPanel()
    {
      InitializeComponent();
      login.LoginComplete += (plc) =>
        {
          plc.EnterSucceed += EnterSucceed;
          plc.Disconnected += (sender, e) => UIDispatcher.Invoke(() =>
            {
              lobby.Init(null);
              login.Visibility = Visibility.Visible;
            });
          login.Visibility = Visibility.Hidden;
          lobby.Init(new LobbyVM(plc));
        };
    }

    public bool Window_Closing()
    {
      return lobby.Window_Closing();
    }
    public void Window_Closed()
    {
      lobby.Window_Closed();
    }
  }
}
