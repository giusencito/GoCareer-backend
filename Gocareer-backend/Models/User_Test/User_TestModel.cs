using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Gocareer_backend.Models.User_Test
{
    public class User_TestModel
    {
        public int UserId { set; get; }
        public int Testid { set; get; }
        public DateTime releasedate { set; get; }
        public int Points { set; get; }
        public int Careerid { set; get; }
    }
}
