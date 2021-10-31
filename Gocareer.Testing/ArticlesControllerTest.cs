using Gocareer_backend.Controllers;
using Gocareer.Infrastructure;
using Gocareer.Domain;
using Gocareer_backend.Models;
using Gocareer_backend.Models.Article;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using Moq;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace Gocareer.Testing
{
    public class ArticlesControllerTest
    {
        private readonly DbContextOptionsBuilder<DbContextGocareer> _builder = new DbContextOptionsBuilder<DbContextGocareer>();  
        private readonly DbContextOptions<DbContextGocareer> _options; 
        private readonly List<Article> _articles;

        public ArticlesControllerTest()
        {
            _builder.UseInMemoryDatabase("Test"); 
            _options = _builder.Options;
            _articles = getArticlesSession();  
        }

        [Fact]
        public async Task GetArticleAsyncReturnAIEnumerableOfArticleModel()
        {
            using (var _context = new DbContextGocareer(_options))
            {
                //Arrange
                _context.Articles.AddRange(_articles); 
                _context.SaveChanges(); 

                var controller = new ArticlesController(_context); 

                //Act
                var result = await controller.GetArticles(); 

                //Assert
                Assert.True(typeof(IEnumerable<ArticleModel>).IsInstanceOfType(result)); 
                Assert.Equal(2, result.Count()); 
            }
        }

        //[Fact]
        //public async Task GetArticleByIdReturnAIActionResultWithArticle()
        //{
        //    using (var _context = new DbContextGocareer(_options))
        //    {
        //        //Arrange
        //        _context.Articles.AddRange(_articles);
        //        _context.SaveChanges();

        //        var controller = new ArticlesController(_context);

        //        //Act
        //        var result = await controller.GetArticlesBySectionId(1);

        //        //Assert
        //        Assert.True(typeof(OkObjectResult).IsInstanceOfType(result));
        //    }
        //}

        //[Fact]
        //public async Task PostArticleReturnAnOkObjectResult()
        //{
        //    using (var _context = new DbContextGocareer(_options))
        //    {
        //        //Arrange
        //        CreateArticleModel newArticle = new CreateArticleModel
        //        {
        //            ArticleName = "Test2",
        //            ArticleDescription = "Two",
        //            Careerid = 1,
        //        };
        //        var controller = new ArticlesController(_context);

        //        //Act
        //        var result = await controller.PostAssignment(1,newArticle);

        //        //Assert
        //        Assert.True(typeof(OkObjectResult).IsInstanceOfType(result));
        //    }
        //}

        [Fact]
        public async Task DeleteArticleReturnAnOkObjectResult()
        {
            using (var _context = new DbContextGocareer(_options))
            {
                //Arrange
                _context.Articles.AddRange(_articles);
                _context.SaveChanges();
                var controller = new ArticlesController(_context);

                //Act
                var result = await controller.DeleteArticle(1);

                //Assert
                Assert.True(typeof(OkObjectResult).IsInstanceOfType(result));
            }
        }



        public List<Article> getArticlesSession()
        {
            var articles = new List<Article>();
            articles.Add(new Article
            {
                Articleid = 1,
                ArticleName = "Test1",
                ArticleDescription = "One",
                Careerid = 1,

            });
            articles.Add(new Article
            {
                Articleid = 2,
                ArticleName = "Test2",
                ArticleDescription = "Two",
                Careerid = 1,

            });
            return articles;
        }
    }
}
