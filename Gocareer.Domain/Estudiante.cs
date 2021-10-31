using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gocareer.Domain
{
    public class Estudiante
    {
        public int UserId { set; get; }
        public string UserName { set; get; }

        public string UserLastname { set; get; }

        public string Useremail { set; get; }

        public string UserPassword { set; get; }
        
        public virtual ICollection<Meeting> Meetings { set; get; }

        public virtual ICollection<Message> Messages { set; get; }

        public virtual ICollection<User_Test> User_Tests { set; get; }

    }
}
