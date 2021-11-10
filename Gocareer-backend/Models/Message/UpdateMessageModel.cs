using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Gocareer_backend.Models.Message
{
    public class UpdateMessageModel
    {
        public string MessageDescription { set; get; }
        public string? answer { set; get; }
        public int UserId { set; get; }
        public int EspecialistId { set; get; }
    }
}
