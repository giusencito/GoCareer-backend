using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gocareer.Domain
{
    public class Option
    {
        public int Optionid { set; get; }
        public string OptionName { set; get; }
        public int Points { set; get; }
        public virtual Question Question { set; get; }

        public int QuestionId { set; get; }

    }
}
