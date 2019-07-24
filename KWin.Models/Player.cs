using System;
using System.Collections.Generic;
using System.Text;

namespace KWin.Models
{
    public class Player
    {
        public Player()
        {
            this.Id = new Guid().ToString();
        }

        public string Id { get; set; }
        
        public string FullName { get; set; }

        public int Name { get; set; }

        public string TeamId { get; set; }
        public Team Team { get; set; }
    }
}
