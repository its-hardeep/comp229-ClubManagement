using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace C229_G4.Models
{
    public class Player
    {
        public int PlayerId { get; set; }
        [Required(ErrorMessage = "Please enter player name")]
        [RegularExpression(@"^[a-zA-Z\s]+$", ErrorMessage = "Player Name cannot have numbers & special characters")]
        public string PlayerName { get; set; }
        [Required(ErrorMessage = "Please enter player age")]
        [Range(12, 50, ErrorMessage = "Please enter age in range of 12-50")]
        public int? Age { get; set; }
        [Required(ErrorMessage = "Please enter player country")]
        [RegularExpression(@"^[a-zA-Z]+$", ErrorMessage = "Country cannot have numbers & special characters")]
        public string Country { get; set; }
        [Required(ErrorMessage = "Please select player clubId")]
        public int? ClubId { get; set; }
    }
}
