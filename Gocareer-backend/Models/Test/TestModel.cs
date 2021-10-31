using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Gocareer_backend.Models.Test
{
    public class TestModel
    {
        public int Testid { set; get; }
        public bool Personalized { set; get; }
        public int EspecialistId { set; get; }
        public string Testname { set; get; }
    }
}
