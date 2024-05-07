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
        private Team selectedTeam;
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
                teams = footballData.Teams;

                //update the player list view with all players data
                playerListView.ItemsSource = allPlayers;

                //update combobox with team names 
                teamComboBox.ItemsSource = teams;
                teamComboBox.DisplayMemberPath = "TeamName";
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error loading football data: {ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
        private void TeamComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            UpdateTeamDetails();
        }
        private void UpdateTeamDetails()
        {
            if (teamComboBox.SelectedItem is Team selectedTeam)
            {
                teamMembersListView.ItemsSource = selectedTeam.Players;

                // Display the LatestResult of the selected team
                latestResultTextBox.Text = selectedTeam.LatestResult;
            }
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
            // Set the image source to an image file path when "Add Player" button is clicked
            displayedImage1.Source = new BitmapImage(new Uri("pack://application:,,,/Personal_Project_2024;component/images/image1.jpg"));
        }

        private void RemovePlayerButton_Click(object sender, RoutedEventArgs e)
        {
            // Set the image source to another image file path when "Remove Player" button is clicked
            displayedImage2.Source = new BitmapImage(new Uri("pack://application:,,,/Personal_Project_2024;component/images/image2.jpg"));
        }


    }
}
