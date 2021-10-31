using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Gocareer_backend.Models.Especialist
{
    public class CreateEspecialistModel
    {
       
        public string EspecialistName { set; get; }
        public string EspecialistLastName { set; get; }
        public string EspecialistEmail { set; get; }
        public string EspecialistPassword { set; get; }
        public string EspecialistInformation { set; get; }

    }
}
