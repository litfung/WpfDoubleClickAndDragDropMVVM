using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Media;
using DoubleClickAndDragDrop.MVVM;
using System.Windows.Input;
using System.Windows.Controls;
using System.Windows;

namespace DoubleClickAndDragDrop
{
    public class ViewModel : ViewModelBase
    {
        private string labelText;
        public string LabelText
        {
            get { return labelText; }
            set
            {
                labelText = value;
                NotifyPropertyChanged("LabelText");
            }
        }

        public ICommand MoveItemCommand
        {
            get { return new RelayCommand(ItemByEventArgs); }
        }

        private void ItemByEventArgs(object arg)
        {
            if (arg is MouseEventArgs)
            {
                var mouseArg = arg as MouseEventArgs;
                var lbl = mouseArg.Source as Label;

                LabelText = lbl.Content.ToString();
            }
            else
            {
                var dragArg = arg as DragEventArgs;
                var label = dragArg.Data.GetData(typeof(Label)) as Label;

                LabelText = label.Content.ToString();
            }
        }

        public ICommand DragItemCommand
        {
            get { return new RelayCommand(DragItem); }
        }

        private void DragItem(object arg)
        {
            var mouseArg = arg as MouseEventArgs;

            var label = mouseArg.Source as Label;

            DragDrop.DoDragDrop(label, label, DragDropEffects.Copy);
        }
    }
}
