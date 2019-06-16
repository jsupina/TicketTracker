using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TicketTracker.Models
{
    public class UserViewModel
    {
        public List<ExpandedUserDTO> users { get; set; }
    }
}