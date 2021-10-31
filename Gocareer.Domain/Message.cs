using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gocareer.Domain
{
    public class Message
    {
        public int Messageid { set; get; }
        public string MessageDescription { set; get; }

        public string? answer { set; get; }

        public virtual Estudiante Estudiante { set; get; }

        public virtual Especialist Especialist { set; get; }

        public int UserId { set; get; }

        public int EspecialistId { set; get; }

    }
}
