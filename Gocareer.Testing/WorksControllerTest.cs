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
using Gocareer_backend.Models.Work;

namespace Gocareer.Testing
{
    public class WorksControllerTest
    {
        private readonly DbContextOptionsBuilder<DbContextGocareer> _builder = new DbContextOptionsBuilder<DbContextGocareer>();
        private readonly DbContextOptions<DbContextGocareer> _options;
        private readonly List<Work> _works;

        public WorksControllerTest()
        {
            _builder.UseInMemoryDatabase("Test");
            _options = _builder.Options;
            _works = getWorksSession();
        }

        [Fact]
        public async Task GetWorkAsyncReturnAIEnumerableOfWorkModel()
        {
            using (var _context = new DbContextGocareer(_options))
            {
                //Arrange
                _context.Works.AddRange(_works);
                _context.SaveChanges();

                var controller = new WorksController(_context);

                //Act
                var result = await controller.GetWorks();

                //Assert
                Assert.True(typeof(IEnumerable<WorkModel>).IsInstanceOfType(result));
                Assert.Equal(2, result.Count());
            }
        }

        [Fact]
        public async Task GetWorkByIdReturnAIActionResultWithWork()
        {
            using (var _context = new DbContextGocareer(_options))
            {
                //Arrange
                _context.Works.AddRange(_works);
                _context.SaveChanges();

                var controller = new WorksController(_context);

                //Act
                var result = await controller.GetWorkById(1);

                //Assert
                Assert.True(typeof(OkObjectResult).IsInstanceOfType(result));
            }
        }

        [Fact]
        public async Task PostWorkReturnAnOkObjectResult()
        {
            using (var _context = new DbContextGocareer(_options))
            {
                //Arrange
                CreateWorkModel newWork = new CreateWorkModel
                {
                    WorkName = "Test2",
                    WorkDescription = "Two",
                    Careerid = 1,
                };
                var controller = new WorksController(_context);

                //Act
                var result = await controller.PostWork(newWork);

                //Assert
                Assert.True(typeof(OkObjectResult).IsInstanceOfType(result));
            }
        }

        //[Fact]
        //public async Task PutEstudianteReturnAnOkObjectResult()
        //{
        //    using (var _context = new DbContextGocareer(_options))
        //    {
        //        //Arrange
        //        _context.Estudiantes.AddRange(_works);
        //        _context.SaveChanges();
        //        UpdateEstudianteModel updateEstudiante = new UpdateEstudianteModel
        //        {
        //            UserName = "test",
        //            UserLastname = "test",
        //            Useremail = "test@gmail.com",
        //            UserPassword = "9876567"
        //        };
        //        var controller = new EstudiantesController(_context);

        //        //Act
        //        var result = await controller.PutEstudiante(1, updateEstudiante);

        //        //Assert
        //        Assert.True(typeof(OkObjectResult).IsInstanceOfType(result));
        //    }
        //}

        [Fact]
        public async Task DeleteWorkReturnAnOkObjectResult()
        {
            using (var _context = new DbContextGocareer(_options))
            {
                //Arrange
                _context.Works.AddRange(_works);
                _context.SaveChanges();
                var controller = new WorksController(_context);

                //Act
                var result = await controller.DeleteWork(1);

                //Assert
                Assert.True(typeof(OkObjectResult).IsInstanceOfType(result));
            }
        }

        public List<Work> getWorksSession()
        {
            var works = new List<Work>();
            works.Add(new Work
            {
                Workid = 1,
                WorkName = "Test1",
                WorkDescription = "One",
                Careerid = 1

            });
            works.Add(new Work
            {
                Workid = 2,
                WorkName = "Test2",
                WorkDescription = "Two",
                Careerid = 1

            });
            return works;
        }
    }
}
