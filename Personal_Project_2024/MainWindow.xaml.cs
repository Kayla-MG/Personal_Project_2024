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

namespace Personal_Project_2024
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private List<Match> matches = new List<Match>();
        private List<Team> teams = new List<Team>();
        public MainWindow()
        {
            InitializeComponent();
            LoadMatches();
            LoadTeams();
            teamComboBox.ItemsSource = teams;
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
        private void LoadMatches()
        {
            // Example loading method
            matches.Add(new Match { MatchId = 1, TeamA = "Team 1", TeamB = "Team 2", MatchDate = DateTime.Now, Score = "0-0" });
            matchesListView.ItemsSource = matches;
        }

        private void UpdateScore_Click(object sender, RoutedEventArgs e)
        {
            if (matchesListView.SelectedItem is Match selectedMatch)
            {
                selectedMatch.Score = scoreTextBox.Text;
                matchesListView.Items.Refresh();
            }
        }
        private void LoadTeams()
        {
            // Mock data
            teams.Add(new Team { TeamId = 1, TeamName = "FC Example", Players = new List<Player> { new Player { Name = "John Doe", Position = "Forward" } } });
            teams.Add(new Team { TeamId = 2, TeamName = "Team Sample", Players = new List<Player>() });
        }
        private void TeamComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            UpdateTeamMembersView();
        }

        private void UpdateTeamMembersView()
        {
            if (teamComboBox.SelectedItem is Team selectedTeam)
            {
                teamMembersListView.ItemsSource = selectedTeam.Players;
            }
        }

        private void AddPlayerButton_Click(object sender, RoutedEventArgs e)
        {
            // Simple form or selection to add a player
            if (teamComboBox.SelectedItem is Team selectedTeam)
            {
                // Example addition, ideally open a dialog to select or create a player
                selectedTeam.Players.Add(new Player { Name = "New Player", Position = "Midfielder" });
                UpdateTeamMembersView();
            }
        }

        private void RemovePlayerButton_Click(object sender, RoutedEventArgs e)
        {
            if (teamComboBox.SelectedItem is Team selectedTeam && teamMembersListView.SelectedItem is Player selectedPlayer)
            {
                selectedTeam.Players.Remove(selectedPlayer);
                UpdateTeamMembersView();
            }
        }
    }
}
