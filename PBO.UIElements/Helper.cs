﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using LightStudio.PokemonBattle.Data;

namespace LightStudio.PokemonBattle.PBO
{
  public static class Helper
  {
    public static readonly Random Random = new Random();
    public static readonly UserData DataMainInstance = new UserData();

    public static SolidColorBrush NewBrush(uint colorcode)
    {
      Color c = new Color();
      c.B = (byte)colorcode;
      colorcode >>= 8;
      c.G = (byte)colorcode;
      colorcode >>= 8;
      c.R = (byte)colorcode;
      colorcode >>= 8;
      c.A = (byte)colorcode;
      SolidColorBrush scb = new SolidColorBrush(c);
      scb.Freeze();
      return scb;
    }
    public static BitmapImage GetImage(string filename)
    {
      return new BitmapImage(new Uri(@"pack://application:,,,/PBO.UIElements;component/images/" + filename, UriKind.Absolute));
    }
    internal static ResourceDictionary GetDictionary(string group, string name)
    {
      return (ResourceDictionary)Application.LoadComponent(
        new Uri(string.Format(@"/PBO.UIElements;component/{0}/{1}.xaml", group, name), UriKind.Relative));
    }
    internal static object GetObject(string group, string filename, string key)
    {
      return GetDictionary(group, filename)[key];
    }
  }
}
