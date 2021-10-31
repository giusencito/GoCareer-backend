using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Gocareer_backend.Models.Question
{
    public class QuestionModel
    {
        public int QuestionId { set; get; }
        public string QuestionName { set; get; }
        public int Score { set; get; }
        public int Testid { set; get; }
    }
}
