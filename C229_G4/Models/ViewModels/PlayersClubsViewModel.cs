using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace C229_G4.Models.ViewModels
{
    public class PlayersClubsViewModel
    {
        public IEnumerable<Club> Clubs { get; set; }
        public IEnumerable<Player> Players { get; set; }
        public Player Player { get; set; }
    }
}
