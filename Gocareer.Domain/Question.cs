using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gocareer.Domain
{
    public class Question
    {

        public int QuestionId { set; get; }

        public string QuestionName { set; get; }

        public int Score { set; get; }

        public Test Test { set; get; }

        public int Testid { set; get; }
        public virtual ICollection<Option> Options { set; get; }

    }
}
