using System;
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

using System.IO;
using System.Xml;
using System.Runtime.Serialization;
using System.Collections.ObjectModel;
using Microsoft.Win32;
using System.Windows.Media.Animation;
using System.ComponentModel;

namespace Football
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private const string PATH_TO_FILE = "Players.xml";

        private ObservableCollection<Player> players;

        public ObservableCollection<Player> Players 
        {
            get { return players; }
            set { players = value; } 
        }

        public MainWindow()
        {
            InitializeComponent();
            
            if (File.Exists(PATH_TO_FILE))
            {
                using (FileStream fs = new FileStream(PATH_TO_FILE, FileMode.Open, FileAccess.Read))
                {
                    DataContractSerializer sr = new DataContractSerializer(typeof(ObservableCollection<Player>));
                    Players = (ObservableCollection<Player>) sr.ReadObject(fs);
                }
            }
                
            list.Focus();
            DataContext = this;
        }

        private void List_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            double time = 0.6;
            
            DoubleAnimation animatedOpacity = new DoubleAnimation(0, 1, new Duration(TimeSpan.FromSeconds(time + 0.2)));
            DoubleAnimation animatedRotate = new DoubleAnimation(0, 360, new Duration(TimeSpan.FromSeconds(time)));
            RotateTransform rotate = new RotateTransform();
            border.RenderTransform = rotate;
            border.RenderTransformOrigin = new Point(0.5, 0.5);
            
            rotate.BeginAnimation(RotateTransform.AngleProperty, animatedRotate);
            border.BeginAnimation(Image.OpacityProperty, animatedOpacity);
        }

        private void List_SourceUpdated(object sender, DataTransferEventArgs e)
        {
            lastUpdTxtBox.SetCurrentValue(TextBox.TextProperty, DateTime.Now.ToString());
            lastUpdTxtBox.GetBindingExpression(TextBox.TextProperty).UpdateSource();
        }

        private void DataGrid_RowDoubleClick(object sender, RoutedEventArgs e)
        {
            DataGridRow row = sender as DataGridRow;
            if (row != null)
            {
                row.DetailsVisibility = (row.DetailsVisibility == Visibility.Collapsed) ? Visibility.Visible : Visibility.Collapsed;
            }
        }

        private void Save_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            using (FileStream fs = new FileStream(PATH_TO_FILE, FileMode.Create, FileAccess.Write))
            {
                using (XmlWriter xmlw = XmlWriter.Create(fs, new XmlWriterSettings { Indent = true }))
                {
                    DataContractSerializer sr = new DataContractSerializer(typeof(ObservableCollection<Player>));
                    sr.WriteObject(xmlw, Players);
                }
            }
            MessageBox.Show("Всі дані записані", "Збереження", MessageBoxButton.OK, MessageBoxImage.Information);
        }

        private void Open_CanExecute(object sender, CanExecuteRoutedEventArgs e)
        {
            if (toggleBut.IsChecked == false)
            {
                e.CanExecute = false;
            }
            else
            {
                e.CanExecute = true;
            }
        }

        private void Open_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            OpenFileDialog ofd = new OpenFileDialog();
            ofd.RestoreDirectory = true;
            ofd.Title = "Вибір фото";
            ofd.Filter = "Всі графічні файли|*.jpg;*.jpeg;*.png| JPEG (*.jpg;*.jpeg)|*.jpg;*.jpeg| Portable Network Graphic (*.png)|*.png";
            if (ofd.ShowDialog() == true)
            {
                image.Source = new BitmapImage(new Uri(ofd.FileName));
            }
        }

        private void DeletePlayer_CanExecute(object sender, CanExecuteRoutedEventArgs e)
        {
            if (list.SelectedItem == null)
            {
                e.CanExecute = false;
            }
            else
            {
                e.CanExecute = true;
            }
        }

        private void DeletePlayer_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            if (MessageBox.Show("Видалити футболіста?\n" + list.SelectedItem, "Видалення", MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes)
            {
                Player temp = list.SelectedItem as Player;
                Players.Remove(temp);
            }
        }

        private void AddPlayer_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            Players.Add(new Player());
            list.SelectedIndex = list.Items.Count - 1;
            if (!GroupMenuItem.IsChecked)
            {
                list.ScrollIntoView(list.Items[list.Items.Count - 1]);
            }
            toggleBut.IsChecked = true;
            firstTxtBox.Focus();
        }
        //EDIT(if not table), ADD(if not table), DELETE (if not table) - ICOMMAND

        private void SortPlayers_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            string nameProperty = e.Parameter as string;
            ICollectionView view = CollectionViewSource.GetDefaultView(list.ItemsSource);
            view.SortDescriptions.Clear();
            if (nameProperty != null)
            {
                if (GroupMenuItem.IsChecked)
                {
                    view.SortDescriptions.Add(new SortDescription("Position", ListSortDirection.Ascending));
                }
                view.SortDescriptions.Add(new SortDescription(nameProperty, ListSortDirection.Ascending));
            }
        }

        private void GroupMenuItem_Checked(object sender, RoutedEventArgs e)
        {
            ICollectionView view = CollectionViewSource.GetDefaultView(list.ItemsSource);
            view.SortDescriptions.Clear();
            view.SortDescriptions.Add(new SortDescription("Position", ListSortDirection.Ascending));
            view.GroupDescriptions.Add(new PropertyGroupDescription("Position"));
        }

        private void GroupMenuItem_Unchecked(object sender, RoutedEventArgs e)
        {
            ICollectionView view = CollectionViewSource.GetDefaultView(list.ItemsSource);
            view.GroupDescriptions.Clear();
        }
    }
}