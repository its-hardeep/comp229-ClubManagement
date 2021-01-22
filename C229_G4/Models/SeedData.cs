using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace C229_G4.Models
{
    public static class SeedData
    {
        public static void EnsurePopulated(IApplicationBuilder app)
        {
            ApplicationDbContext context = app.ApplicationServices
            .GetRequiredService<ApplicationDbContext>();
            context.Database.Migrate();
            if (!context.Clubs.Any())
            {
                context.Clubs.AddRange(
                    new Club { ClubName = "Barcelona", StadiumName = "Camp Nou", YearFounded = 1899, Country = "Spain" },
                    new Club { ClubName = "Real Madrid", StadiumName = "Santiago Bernabeu", YearFounded = 1902, Country = "Spain" },
                    new Club { ClubName = "Manchester City", StadiumName = "City of Manchester Stadium", YearFounded = 1880, Country = "England" },
                    new Club { ClubName = "Bayern Munich", StadiumName = "Allianz Arena", YearFounded = 1900, Country = "Germany" },
                    new Club { ClubName = "Paris Saint-Germain", StadiumName = "Parc des Princes", YearFounded = 1970, Country = "France" },
                    new Club { ClubName = "Arsenal", StadiumName = "Emirates Stadium", YearFounded = 1886, Country = "England" },
                    new Club { ClubName = "Atlético Madrid", StadiumName = "Wanda Metropolitano", YearFounded = 1903, Country = "Spain" },
                    new Club { ClubName = "Liverpool", StadiumName = "Anfield", YearFounded = 1892, Country = "England" }
                );
                context.SaveChanges();
            }
            if(!context.Players.Any())
            {
                context.Players.AddRange(
                    new Player { PlayerName = "Lionel Messi", Age = 31, Country = "Argentina", ClubId = 1 },
                    new Player { PlayerName = "Luis Suárez", Age = 32, Country = "Uruguay", ClubId = 1 },
                    new Player { PlayerName = "Karim Benzema", Age = 31, Country = "France", ClubId = 2 },
                    new Player { PlayerName = "Sergio Ramos", Age = 33, Country = "Spain", ClubId = 2 },
                    new Player { PlayerName = "Vincent Kompany", Age = 33, Country = "Belgium", ClubId = 3 },
                    new Player { PlayerName = "Manuel Neuer", Age = 33, Country = "Germany", ClubId = 4 },
                    new Player { PlayerName = "Thiago Silva", Age = 34, Country = "Brazil", ClubId = 5 },
                    new Player { PlayerName = "Laurent Koscielny", Age = 33, Country = "France", ClubId = 6 },
                    new Player { PlayerName = "Diego Godín", Age = 33, Country = "Uruguay", ClubId = 7 },
                    new Player { PlayerName = "Jordan Henderson", Age = 28, Country = "England", ClubId = 8 }
                );
                context.SaveChanges();
            }
        }
    }
}
