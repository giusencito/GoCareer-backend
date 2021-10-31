using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gocareer.Domain
{
    public class Article
    {
        public int Articleid { set; get; }

        public string ArticleName { set; get; }
        public string ArticleDescription { set; get; }

        public virtual Career Career{set;get;}

        public int Careerid { set; get; }
    }
}
