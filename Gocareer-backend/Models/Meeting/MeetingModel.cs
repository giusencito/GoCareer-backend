using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Gocareer_backend.Models.Meeting
{
    public class MeetingModel
    {
        public int MeetingId { set; get; }
        public DateTime Date { set; get; }

        public DateTime Hour { set; get; }

        public int UserId { set; get; }

        public int EspecialistId { set; get; }
    }
}
