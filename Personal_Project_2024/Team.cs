using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Personal_Project_2024
{
    public class Team
    {
        public int TeamId { get; set; }
        public string TeamName { get; set; }
        public List<Player> Players { get; set; } = new List<Player>();

        public string DisplayInfo => $"{TeamName} - {Players.Count} Players";
    }

}
