using System;
using System.ComponentModel.DataAnnotations;

namespace KWin.Models
{
    public class Player
    {
        public Player()
        {
            this.Id =  Guid.NewGuid().ToString();

        }

        public string Id { get; set; }

        [Required]
        [MaxLength(30)]
        public string FullName { get; set; }

        public string? TeamId { get; set; }

        #nullable enable
        public Team Team { get; set; }
    }
}
