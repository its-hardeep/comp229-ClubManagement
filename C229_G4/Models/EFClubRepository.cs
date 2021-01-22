using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace C229_G4.Models
{
    public class EFClubRepository : IClubRepository
    {
        private ApplicationDbContext context;
        public EFClubRepository(ApplicationDbContext ctx)
        {
            context = ctx;
        }
        public IQueryable<Club> Clubs => context.Clubs.Include(c => c.Players);
        public IQueryable<Player> Players => context.Players;
        public void SaveClub(Club club)
        {
            if (club.ClubId == 0)
            {
                context.Clubs.Add(club);
            }
            else
            {
                Club dbEntry = context.Clubs
                .FirstOrDefault(c => c.ClubId == club.ClubId);
                if (dbEntry != null)
                {
                    dbEntry.ClubName = club.ClubName;
                    dbEntry.StadiumName = club.StadiumName;
                    dbEntry.YearFounded = club.YearFounded;
                    dbEntry.Country = club.Country;
                }
            }
            context.SaveChanges();
        }
        public Club DeleteClub(int clubId)
        {
            Club dbEntry = context.Clubs.FirstOrDefault(c => c.ClubId == clubId);
            if (dbEntry != null)
            {
                context.Players.RemoveRange(context.Players.Where(p => p.ClubId == dbEntry.ClubId));
                context.Clubs.Remove(dbEntry);
                context.SaveChanges();
            }
            return dbEntry;
        }
        public void SavePlayer(Player player)
        {
            if (player.PlayerId == 0)
            {
                context.Players.Add(player);
            }
            else
            {
                Player dbEntry = context.Players.FirstOrDefault(p => p.PlayerId == player.PlayerId);
                if (dbEntry != null)
                {
                    dbEntry.PlayerName = player.PlayerName;
                    dbEntry.Age = player.Age;
                    dbEntry.Country = player.Country;
                    dbEntry.ClubId = player.ClubId;
                }
            }
            context.SaveChanges();
        }
        public Player DeletePlayer(int playerId)
        {
            Player dbEntry = context.Players.FirstOrDefault(p => p.PlayerId == playerId);
            if (dbEntry != null)
            {
                context.Players.Remove(dbEntry);
                context.SaveChanges();
            }
            return dbEntry;
        }
    }
}
