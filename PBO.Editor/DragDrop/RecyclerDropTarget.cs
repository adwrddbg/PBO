﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using LightStudio.PokemonBattle.PBO.UIElements.Interactivity;

namespace LightStudio.PokemonBattle.PBO.Editor
{
    public class RecyclerDropTarget : IDragDropTarget
    {

        public void HandleDrop(UIElement sender, DragEventArgs e)
        {
            DragDropState.SetDragDropData(sender, null);
            e.Effects = DragDropEffects.None;

            var data = e.Data.GetData(typeof(IDragDropData)) as PokemonDragDropData;
            if (data == null)
                return;
            data.Actions = RecyclerDropTarget.GetDragDropAction(sender, data);
            if (data.Actions == DragDropActions.MoveTo)
                data.Pokemon.RemoveCommand.Execute(null);
        }

        public void HandleDragOver(UIElement sender, DragEventArgs e)
        {
            var data = e.Data.GetData(typeof(IDragDropData)) as PokemonDragDropData;
            if (data == null)
                return;
            DragDropState.SetDragDropData(sender, data);
            data.Actions = RecyclerDropTarget.GetDragDropAction(sender, data);
        }

        public void HandleDragLeave(UIElement sender, DragEventArgs e)
        {
            DragDropState.SetDragDropData(sender, null);
        }

        public void HandleDragEnter(UIElement sender, DragEventArgs e)
        {
            DragDropState.SetDragDropData(sender,
                e.Data.GetData(typeof(IDragDropData)) as IDragDropData);
        }

        private static DragDropActions GetDragDropAction(UIElement target, PokemonDragDropData data)
        {
            if ((target as FrameworkElement).DataContext != data.SourceFolder)
            {
                return DragDropActions.MoveTo;
            }
            else
            {
                return DragDropActions.None;
            }
        }

    }
}
