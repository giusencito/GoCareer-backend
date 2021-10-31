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
using Gocareer_backend.Models.Especialist;

namespace Gocareer.Testing
{
    public class EspecialistsControllerTest
    {
        private readonly DbContextOptionsBuilder<DbContextGocareer> _builder = new DbContextOptionsBuilder<DbContextGocareer>();
        private readonly DbContextOptions<DbContextGocareer> _options;
        private readonly List<Especialist> _especialists;

        public EspecialistsControllerTest()
        {
            _builder.UseInMemoryDatabase("Test");
            _options = _builder.Options;
            _especialists = getEspecialistsSession();
        }

        [Fact]
        public async Task GetEspecialitAsyncReturnAIEnumerableOfEspecialistModel()
        {
            using (var _context = new DbContextGocareer(_options))
            {
                //Arrange
                _context.Especialists.AddRange(_especialists);
                _context.SaveChanges();

                var controller = new EspecialistsController(_context);

                //Act
                var result = await controller.GetEspecialist();

                //Assert
                Assert.True(typeof(IEnumerable<EspecialistModel>).IsInstanceOfType(result));
                Assert.Equal(2, result.Count());
            }
        }

        [Fact]
        public async Task GetEspecialistByIdReturnAIActionResultWithArticle()
        {
            using (var _context = new DbContextGocareer(_options))
            {
                //Arrange
                _context.Especialists.AddRange(_especialists);
                _context.SaveChanges();

                var controller = new EspecialistsController(_context);

                //Act
                var result = await controller.GetEspecialistById(1);

                //Assert
                Assert.True(typeof(OkObjectResult).IsInstanceOfType(result));
            }
        }

        [Fact]
        public async Task PostEspecialistReturnAnOkObjectResult()
        {
            using (var _context = new DbContextGocareer(_options))
            {
                //Arrange
                CreateEspecialistModel newEspecialist = new CreateEspecialistModel
                {
                    EspecialistName = "Test2",
                    EspecialistLastName = "Two",
                    EspecialistEmail = "testing@gmail.com",
                    EspecialistPassword = "12345",
                    EspecialistInformation = "a information testing" 
                };
                var controller = new EspecialistsController(_context);

                //Act
                var result = await controller.PostEspecialist(newEspecialist);

                //Assert
                Assert.True(typeof(OkObjectResult).IsInstanceOfType(result));
            }
        }

        [Fact]
        public async Task PutEspecialistReturnAnOkObjectResult()
        {
            using (var _context = new DbContextGocareer(_options))
            {
                //Arrange
                _context.Especialists.AddRange(_especialists);
                _context.SaveChanges();
                UptadeEspecialistModel updateEspecialist = new UptadeEspecialistModel
                {
                    EspecialistName = "test",
                    EspecialistLastName = "test",
                    EspecialistEmail = "test@gmail.com",
                    EspecialistPassword = "98765",
                    EspecialistInformation = "info testing"
                };
                var controller = new EspecialistsController(_context);

                //Act
                var result = await controller.PutEspecialist(1, updateEspecialist);

                //Assert
                Assert.True(typeof(OkObjectResult).IsInstanceOfType(result));
            }
        }

        [Fact]
        public async Task DeleteEspecialistReturnAnOkObjectResult()
        {
            using (var _context = new DbContextGocareer(_options))
            {
                //Arrange
                _context.Especialists.AddRange(_especialists);
                _context.SaveChanges();
                var controller = new EspecialistsController(_context);

                //Act
                var result = await controller.Delete(1);

                //Assert
                Assert.True(typeof(OkObjectResult).IsInstanceOfType(result));
            }
        }

        public List<Especialist> getEspecialistsSession()
        {
            var especialists = new List<Especialist>();
            especialists.Add(new Especialist
            {
                EspecialistId = 1,
                EspecialistName = "Test1",
                EspecialistLastName = "One",
                EspecialistEmail = "testing@gmail.com",
                EspecialistPassword = "123456",
                EspecialistInformation = "testinginfo"

            });
            especialists.Add(new Especialist
            {
                EspecialistId = 2,
                EspecialistName = "Test2",
                EspecialistLastName = "Two",
                EspecialistEmail = "testing2@gmail.com",
                EspecialistPassword = "1234567",
                EspecialistInformation = "testinginfo2"

            });
            return especialists;
        }

    }
}
