using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Personal_Project_2024
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }
        //list of players
        List<Player> allPlayers = new List<Player>();
        private void txtSearch_TextChanged(object sender, TextChangedEventArgs e)
        {
            string searchText = playerSearch.Text.ToLower();

           
            //new list called filteredPlayers of object type player
            //put it to lower to avoid issues with cp or lower
            //search to see if search text matches anyone on list
            //.Where is LINQ extension allows us to filter elements
            // p=> is a lambda expression , p represents each Player object in allPlayers collection
            List<Player> filteredPlayers = allPlayers.Where(p => p.Name.ToLower().Contains(searchText) || p.Position.ToLower().Contains(searchText)).ToList();

            // Update players list box with the filtered player list
            playerListView.ItemsSource = filteredPlayers;
        }
    }
}
