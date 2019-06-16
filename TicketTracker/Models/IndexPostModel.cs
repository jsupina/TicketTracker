using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TicketTracker.Models
{
    public class IndexPostModel
    {
        public List<Ticket> Open { get; set; }
        public List<Ticket> Closed { get; set; }
    }
}