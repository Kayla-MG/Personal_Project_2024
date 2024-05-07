using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
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

using System.IO;
using Newtonsoft.Json;

namespace Personal_Project_2024
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {

        //  private List<Team> teams = new List<Team>();
        private List<Player> allPlayers;
        public MainWindow()
        {
            InitializeComponent();
            LoadFootballData();
            
        }
        private void LoadFootballData()
        {
            try
            {
                string jsonFilePath = "C:/Users/35383/OneDrive - Atlantic TU/Year 2/Semester 4/OOD/Personal_Project_2024/football_data.json";
                string jsonData = File.ReadAllText(jsonFilePath);

                FootballData footballData = JsonConvert.DeserializeObject<FootballData>(jsonData);

                // Assuming FootballData.Players is the list of players
                allPlayers = footballData.Players;

                //update the player list view with all players data
                playerListView.ItemsSource = allPlayers;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error loading football data: {ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
        private void txtSearch_TextChanged(object sender, System.Windows.Controls.TextChangedEventArgs e)
        {
            string searchText = playerSearch.Text.ToLower();

            if (allPlayers != null)
            {
                List<Player> filteredPlayers = allPlayers.FindAll(p =>
                    p.Name.ToLower().Contains(searchText) || p.Position.ToLower().Contains(searchText));

                playerListView.ItemsSource = filteredPlayers;
            }
        }
        private void AddPlayerButton_Click(object sender, RoutedEventArgs e)
        {
            // Example: Adding a new player to the list
            if (teamComboBox.SelectedItem is Team selectedTeam)
            {
                selectedTeam.Players.Add(new Player { Name = "New Player", Position = "Midfielder" });

                // Refresh the teamMembersListView to reflect changes
                teamMembersListView.ItemsSource = null;
                teamMembersListView.ItemsSource = selectedTeam.Players;
            }
        }
        private void RemovePlayerButton_Click(object sender, RoutedEventArgs e)
        {
            // Example: Removing a player from the selected team
            if (teamComboBox.SelectedItem is Team selectedTeam && teamMembersListView.SelectedItem is Player selectedPlayer)
            {
                selectedTeam.Players.Remove(selectedPlayer);

                // Refresh the teamMembersListView to reflect changes
                teamMembersListView.ItemsSource = null;
                teamMembersListView.ItemsSource = selectedTeam.Players;
            }
        }
        private void TeamComboBox_SelectionChanged(object sender, System.Windows.Controls.SelectionChangedEventArgs e)
        {
            // Update the teamMembersListView when a new team is selected
            if (teamComboBox.SelectedItem is Team selectedTeam)
            {
                teamMembersListView.ItemsSource = selectedTeam.Players;
            }
        }
    }
}
