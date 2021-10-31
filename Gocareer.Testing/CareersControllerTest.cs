using Gocareer_backend.Controllers;
using Gocareer.Infrastructure;
using Gocareer.Domain;
using Gocareer_backend.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using Moq;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;
using Gocareer_backend.Models.Career;

namespace Gocareer.Testing
{
    public class CareersControllerTest
    {
        private readonly DbContextOptionsBuilder<DbContextGocareer> _builder = new DbContextOptionsBuilder<DbContextGocareer>();
        private readonly DbContextOptions<DbContextGocareer> _options;
        private readonly List<Career> _careers;

        public CareersControllerTest()
        {
            _builder.UseInMemoryDatabase("Test");
            _options = _builder.Options;
            _careers = getCareersSession();
        }

        [Fact]
        public async Task GetCareerAsyncReturnAIEnumerableOfCareerModel()
        {
            using (var _context = new DbContextGocareer(_options))
            {
                //Arrange
                _context.Careers.AddRange(_careers);
                _context.SaveChanges();

                var controller = new CareersController(_context);

                //Act
                var result = await controller.GetCareers();

                //Assert
                Assert.True(typeof(IEnumerable<CareerModel>).IsInstanceOfType(result));
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

        [Fact]
        public async Task PostCareerReturnAnOkObjectResult()
        {
            using (var _context = new DbContextGocareer(_options))
            {
                //Arrange
                CreateCareerModel newCareer = new CreateCareerModel
                {
                    CareerName = "Test2",
                    CareerDescription = "Two"
                };
                var controller = new CareersController(_context);

                //Act
                var result = await controller.PostCareer(newCareer);

                //Assert
                Assert.True(typeof(OkObjectResult).IsInstanceOfType(result));
            }
        }

        [Fact]
        public async Task PutCareerReturnAnOkObjectResult()
        {
            using (var _context = new DbContextGocareer(_options))
            {
                //Arrange
                _context.Careers.AddRange(_careers);
                _context.SaveChanges();
                UpdateCareerModel updateCareer= new UpdateCareerModel
                {
                    CareerName = "test",
                    CareerDescription = "test"
                };
                var controller = new CareersController(_context);

                //Act
                var result = await controller.PutPaymentType(1, updateCareer);

                //Assert
                Assert.True(typeof(OkObjectResult).IsInstanceOfType(result));
            }
        }

        [Fact]
        public async Task DeleteCareerReturnAnOkObjectResult()
        {
            using (var _context = new DbContextGocareer(_options))
            {
                //Arrange
                _context.Careers.AddRange(_careers);
                _context.SaveChanges();
                var controller = new CareersController(_context);

                //Act
                var result = await controller.Delete(1);

                //Assert
                Assert.True(typeof(OkObjectResult).IsInstanceOfType(result));
            }
        }



        public List<Career> getCareersSession()
        {
            var careers = new List<Career>();
            careers.Add(new Career
            {
                Careerid = 1,
                CareerName = "Test1",
                CareerDescription = "One",

            });
            careers.Add(new Career
            {
                Careerid = 2,
                CareerName = "Test2",
                CareerDescription = "Two",

            });
            return careers;
        }
    }
}
