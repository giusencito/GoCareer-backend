using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gocareer.Domain
{
    public class Work
    {
        public int Workid { set; get; }

        public string WorkName { set; get; }
        public string WorkDescription { set; get; }

        public virtual Career Career { set; get; }
        public int Careerid { set; get; }

    }
}
