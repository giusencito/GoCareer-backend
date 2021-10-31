using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gocareer.Domain
{
    public class Career
    { 

        public int Careerid { set; get; }

        public string CareerName { set; get; }
        public string CareerDescription { set; get; }

        public virtual ICollection<Article> Articles { set; get; }

        public virtual ICollection<Work> Works { set; get; }

        public virtual ICollection<User_Test> User_Tests { set; get; }

    }
}
