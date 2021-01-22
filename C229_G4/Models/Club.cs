using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace C229_G4.Models
{
    public class Club
    {
        public int ClubId { get; set; }
        [Required(ErrorMessage = "Please enter club name")]
        [RegularExpression(@"^[a-zA-Z\s]+$", ErrorMessage = "Club Name cannot have numbers & special characters")]
        public string ClubName { get; set; }
        [Required(ErrorMessage = "Please enter stadium name")]
        [RegularExpression(@"^[a-zA-Z\s]+$", ErrorMessage = "Stadium Name cannot have numbers & special characters")]
        public string StadiumName { get; set; }
        [Required(ErrorMessage = "Please enter year founded")]
        [Range(1700, 2020, ErrorMessage = "Please enter year in range of 1700-2020")]
        public int? YearFounded { get; set; }
        [Required(ErrorMessage = "Please enter club country")]
        [RegularExpression(@"^[a-zA-Z]+$", ErrorMessage = "Country Name cannot have numbers, spaces & special characters")]
        public string Country { get; set; }
        public List<Player> Players { get; set; } = new List<Player>();
    }
}
