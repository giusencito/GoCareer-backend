using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gocareer.Domain
{
    public class Especialist
    {
        public int EspecialistId { set; get; }
        public string EspecialistName { set; get; }
        public string EspecialistLastName { set; get; }
        public string EspecialistEmail { set; get; }
        public string EspecialistPassword { set; get; }
        public string EspecialistInformation { set; get; }

        public virtual ICollection<Meeting> Meetings { set; get; }
        public virtual ICollection<Message> Messages { set; get; }
        public virtual ICollection<Test> Tests { set; get; }
    }
}
