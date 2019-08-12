using System;

namespace KWin.Models
{
    public class Player
    {
        public Player()
        {
            this.Id =  Guid.NewGuid().ToString();

        }

        public string Id { get; set; }

        public string FullName { get; set; }

        public string? TeamId { get; set; }

        #nullable enable
        public Team Team { get; set; }
    }
}
