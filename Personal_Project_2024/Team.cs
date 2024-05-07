using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.IO;
using System.Net.Http;

namespace Personal_Project_2024
{
    public class Team
    {
       
        public string TeamName { get; set; }
        public List<Player> Players { get; set; } = new List<Player>();

    }

}
