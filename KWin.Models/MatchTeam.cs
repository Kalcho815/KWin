using System.ComponentModel.DataAnnotations;

namespace KWin.Models
{
    public class MatchTeam
    {
        [Required]
        public string MatchId { get; set; }
        public Match Match { get; set; }

        [Required]
        public string TeamId { get; set; }
        public Team Team { get; set; }
    }
}
