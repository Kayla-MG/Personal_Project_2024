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
        private List<Team> teams; // Declare teams at the class level
        public MainWindow()
        {
            InitializeComponent();
            // Load football data from json file
            LoadFootballData();
            // Bind the ComboBox to the teams collection
            teamComboBox.ItemsSource = teams;
        }


            private void LoadFootballData()
        {
            try
            {
                string jsonFilePath = "C:/Users/35383/OneDrive - Atlantic TU/Year 2/Semester 4/OOD/Personal_Project_2024/football_data.json";
                string jsonData = File.ReadAllText(jsonFilePath);

                //deserialize json data into a football data object
                FootballData footballData = JsonConvert.DeserializeObject<FootballData>(jsonData);

                //Populate allPlayers list with players from FootballData
                allPlayers = footballData.Players;

                //update the player list view with all players data
                playerListView.ItemsSource = allPlayers;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error loading football data: {ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
        private void TeamComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            UpdateTeamMembersView();
        }
        private void UpdateTeamMembersView()
        {
            if (teamComboBox.SelectedItem is Team selectedTeam)
            {
                // Display players of the selected team in the teamMembersListView
                teamMembersListView.ItemsSource = selectedTeam.Players;
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
            if (teamComboBox.SelectedItem is Team selectedTeam)
            {
                // Create a new player (you can use a dialog to input player details)
                Player newPlayer = new Player { Name = "New Player", Position = "Midfielder" };

                // Add the new player to the selected team
                selectedTeam.Players.Add(newPlayer);

                // Refresh the team members view
                UpdateTeamMembersView();
            }
        }
      

        private void RemovePlayerButton_Click(object sender, RoutedEventArgs e)
        {
            if (teamComboBox.SelectedItem is Team selectedTeam && teamMembersListView.SelectedItem is Player selectedPlayer)
            {
                // Remove the selected player from the team
                selectedTeam.Players.Remove(selectedPlayer);

                // Refresh the team members view
                UpdateTeamMembersView();
            }
        }
      

    }
}
