using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Gocareer_backend.Models.Work
{
    public class WorkModel
    {
        public int Workid { set; get; }
        public string WorkName { set; get; }
        public string WorkDescription { set; get; }
        public int Careerid { set; get; }
    }
}
