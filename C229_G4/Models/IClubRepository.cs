using C229_G4.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace C229_G4.Models
{
    public interface IClubRepository
    {
        IQueryable<Club> Clubs { get; }
        IQueryable<Player> Players { get; }
        void SaveClub(Club club);
        void SavePlayer(Player player);
        Club DeleteClub(int clubId);
        Player DeletePlayer(int playerId);
    }
}
