using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Personal_Project_2024
{
    public class Match
    {
            public int MatchId { get; set; }
            public string TeamA { get; set; }
            public string TeamB { get; set; }
            public DateTime MatchDate { get; set; }
            public string Score { get; set; } // Format: "0-0"
    }

    
}
