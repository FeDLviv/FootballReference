using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using System.Windows.Input;

namespace Football
{
    class DeletePlayerCommand
    {
        private static RoutedUICommand command;

        public static RoutedUICommand Command
        {
            get { return command; }
        }

        static DeletePlayerCommand()
        {
            command = new RoutedUICommand("Видалити футболіста", "DeletePlayer", typeof(DeletePlayerCommand));
        }
    }
}