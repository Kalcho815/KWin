using System;
using System.Collections.Generic;
using System.Text;

namespace KWin.Models
{
    public class User
    {
        public User()
        {
            this.Id = new Guid().ToString();
            this.Bets = new HashSet<Bet>();
        }
        public string Id { get; set; }

        public string Username { get; set; }

        public string Email { get; set; }

        public string FullName { get; set; }

        public string PhoneNumber { get; set; }

        public ICollection<Bet> Bets { get; set; }
    }
}
