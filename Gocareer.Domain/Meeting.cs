using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gocareer.Domain
{
    public class Meeting
    {
        public int MeetingId { set; get; }
        public DateTime Date { set; get; }

        public DateTime Hour { set; get; }
        public virtual Estudiante estudiante { set; get; }

        public int UserId { set; get; }
        public virtual Especialist especialist { set; get; }

        public int EspecialistId { set; get; }

    }
}
