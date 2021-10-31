using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gocareer.Domain
{
    public class Test
    {  
        public int Testid { set; get; }
        public bool Personalized { set; get; }
        public Especialist Especialist { set; get; }
        public int EspecialistId { set; get; }
        public string Testname { set; get; }
        public virtual ICollection<Question> Questions { set; get; }


        public virtual ICollection<User_Test> User_Tests { set; get; }

    }
}
