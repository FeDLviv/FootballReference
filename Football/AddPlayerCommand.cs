using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using System.Windows.Input;

namespace Football
{
    class AddPlayerCommand
    {
        private static RoutedUICommand command;

        public static RoutedUICommand Command
        {
            get { return command; }
        }

        static AddPlayerCommand()
        {
            //InputGestureCollection inputs = new InputGestureCollection();
            //inputs.Add(new KeyGesture(Key.A, ModifierKeys.Control, "Ctrl + A"));
            command = new RoutedUICommand("Додати футболіста", "AddPlayer", typeof(AddPlayerCommand));
        }
    }
}