using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gocareer.Domain
{
    public class User_Test
    {
        public virtual Estudiante Estudiante { set; get; }
        public int UserId { set; get; }
        public virtual Test Test { set; get; }
        public int Testid { set; get; }
        public DateTime releasedate { set; get; }
        public int Points { set; get; }

        public virtual Career Career { set; get; }
        public int Careerid { set; get; }
    }
}
