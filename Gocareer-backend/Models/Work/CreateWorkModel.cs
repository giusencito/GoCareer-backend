using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Gocareer_backend.Models.Work
{
    public class CreateWorkModel
    {
        public string WorkName { set; get; }
        public string WorkDescription { set; get; }
        public int Careerid { set; get; }
    }
}
