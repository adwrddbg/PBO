﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel;
using System.Collections.ObjectModel;
using System.Windows.Data;
using LightStudio.Tactic.Messaging.Lobby;
using LightStudio.PokemonBattle.Messaging;

namespace LightStudio.PokemonBattle.PBO.Lobby
{
  class LobbyVM
  {
    public readonly PokemonLobbyClient Model;
    Dictionary<int, UserVM> usersDictionary;
    ObservableCollection<UserVM> users;
    ReadOnlyObservableCollection<UserVM> readonlyUsers;

    public LobbyVM(PokemonLobbyClient model)
    {
      this.Model = model;
      model.UserChanged += model_UserChanged;
      //if it's possible to relogin, better Disconnected+=()=>Model.Dispose();

      var _us = model.Users;
      usersDictionary = new Dictionary<int, UserVM>(_us.Count());
      users = new ObservableCollection<UserVM>();
      foreach (User u in _us) AddUser(u);
      User = new UserVM(model, Model.User);
      usersDictionary.Add(User.Id, User); users.Add(User);
      readonlyUsers = new ReadOnlyObservableCollection<UserVM>(users);

      UsersView = CollectionViewSource.GetDefaultView(Users);
      UsersView.SortDescriptions.Add(new SortDescription("State", ListSortDirection.Descending));
    }

    public UserVM User { get; private set; }
    public ICollectionView UsersView { get; private set; }
    public ReadOnlyObservableCollection<UserVM> Users
    { get { return readonlyUsers; } }

    void AddUser(User user)
    {
      UserVM u = new UserVM(Model, user);
      usersDictionary.Add(u.Id, u);
      users.Add(u);
    }
    void model_UserChanged(int userId)
    {
      //thread safe
      UIDispatcher.Invoke(delegate
        {
          var uinfo = Model.GetUser(userId);
          if (uinfo != null)
          {
            if (usersDictionary.ContainsKey(userId)) usersDictionary[userId].RefreshProperties(uinfo);
            else AddUser(uinfo);
          }
          else
          { //Remove user
            UserVM u;
            usersDictionary.TryGetValue(userId, out u);
            users.Remove(u);
            usersDictionary.Remove(userId);
          }
        });
    }
    public void Exit()
    {
      Model.Logout();
      Model.Dispose();
    }
  }
}
