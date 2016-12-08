using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using System.Windows.Input;

namespace Football
{
    class SortPlayersCommand
    {
        private static RoutedUICommand command;

        public static RoutedUICommand Command
        {
            get { return command; }
        }

        static SortPlayersCommand()
        {
            command = new RoutedUICommand("SortPlayers", "SortPlayers", typeof(SortPlayersCommand));
        }
    }
}