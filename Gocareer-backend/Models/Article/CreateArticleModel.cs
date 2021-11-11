using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Gocareer_backend.Models.Article
{
    public class CreateArticleModel
    {
        public string ArticleName { set; get; }
        public string ArticleDescription { set; get; }

        public int Careerid { set; get; }


    }
}
