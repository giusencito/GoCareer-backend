using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Gocareer_backend.Models.Option
{
    public class OptionModel
    {
        public int Optionid { set; get; }
        public string OptionName { set; get; }
        public int Points { set; get; }
        public int QuestionId { set; get; }
    }
}
