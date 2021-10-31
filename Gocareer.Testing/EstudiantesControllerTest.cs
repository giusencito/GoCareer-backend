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
using Gocareer_backend.Models.Estudiantes;

namespace Gocareer.Testing
{
    public class EstudiantesControllerTest
    {
        private readonly DbContextOptionsBuilder<DbContextGocareer> _builder = new DbContextOptionsBuilder<DbContextGocareer>();
        private readonly DbContextOptions<DbContextGocareer> _options;
        private readonly List<Estudiante> _estudiantes;

        public EstudiantesControllerTest()
        {
            _builder.UseInMemoryDatabase("Test");
            _options = _builder.Options;
            _estudiantes = getEstudiantesSession();
        }

        [Fact]
        public async Task GetEstudianteAsyncReturnAIEnumerableOfEstudianteModel()
        {
            using (var _context = new DbContextGocareer(_options))
            {
                //Arrange
                _context.Estudiantes.AddRange(_estudiantes);
                _context.SaveChanges();

                var controller = new EstudiantesController(_context);

                //Act
                var result = await controller.GetEstudiante();

                //Assert
                Assert.True(typeof(IEnumerable<EstudianteModel>).IsInstanceOfType(result));
                Assert.Equal(2, result.Count());
            }
        }

        [Fact]
        public async Task GetEstudianteByIdReturnAIActionResultWithArticle()
        {
            using (var _context = new DbContextGocareer(_options))
            {
                //Arrange
                _context.Estudiantes.AddRange(_estudiantes);
                _context.SaveChanges();

                var controller = new EstudiantesController(_context);

                //Act
                var result = await controller.GetEstudianteById(1);

                //Assert
                Assert.True(typeof(OkObjectResult).IsInstanceOfType(result));
            }
        }

        [Fact]
        public async Task PostEstudianteReturnAnOkObjectResult()
        {
            using (var _context = new DbContextGocareer(_options))
            {
                //Arrange
                CreateEstudianteModel newEstudiante = new CreateEstudianteModel
                {
                    UserName = "Test2",
                    UserLastname = "Two",
                    Useremail = "testing@gmail.com",
                    UserPassword = "123456",
                };
                var controller = new EstudiantesController(_context);

                //Act
                var result = await controller.PostEstudiante(newEstudiante);

                //Assert
                Assert.True(typeof(OkObjectResult).IsInstanceOfType(result));
            }
        }

        [Fact]
        public async Task PutEstudianteReturnAnOkObjectResult()
        {
            using (var _context = new DbContextGocareer(_options))
            {
                //Arrange
                _context.Estudiantes.AddRange(_estudiantes);
                _context.SaveChanges();
                UpdateEstudianteModel updateEstudiante = new UpdateEstudianteModel
                {
                    UserName = "test",
                    UserLastname = "test",
                    Useremail = "test@gmail.com",
                    UserPassword = "9876567"
                };
                var controller = new EstudiantesController(_context);

                //Act
                var result = await controller.PutEstudiante(1, updateEstudiante);

                //Assert
                Assert.True(typeof(OkObjectResult).IsInstanceOfType(result));
            }
        }

        [Fact]
        public async Task DeleteEstudianteReturnAnOkObjectResult()
        {
            using (var _context = new DbContextGocareer(_options))
            {
                //Arrange
                _context.Estudiantes.AddRange(_estudiantes);
                _context.SaveChanges();
                var controller = new EstudiantesController(_context);

                //Act
                var result = await controller.Delete(1);

                //Assert
                Assert.True(typeof(OkObjectResult).IsInstanceOfType(result));
            }
        }

        public List<Estudiante> getEstudiantesSession()
        {
            var estudiantes = new List<Estudiante>();
            estudiantes.Add(new Estudiante
            {
                UserId = 1,
                UserName = "Test1",
                UserLastname = "One",
                Useremail = "testing@gmail.com",
                UserPassword = "123456"

            });
            estudiantes.Add(new Estudiante
            {
                UserId = 2,
                UserName = "Test2",
                UserLastname = "Two",
                Useremail = "testing2@gmail.com",
                UserPassword = "12345678"

            });
            return estudiantes;
        }
    }
}
