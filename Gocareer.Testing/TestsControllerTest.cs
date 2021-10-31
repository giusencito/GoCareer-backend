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
using Gocareer_backend.Models.Test;

namespace Gocareer.Testing
{
    public class TestsControllerTest
    {
        private readonly DbContextOptionsBuilder<DbContextGocareer> _builder = new DbContextOptionsBuilder<DbContextGocareer>();
        private readonly DbContextOptions<DbContextGocareer> _options;
        private readonly List<Test> _tests;

        public TestsControllerTest()
        {
            _builder.UseInMemoryDatabase("Test");
            _options = _builder.Options;
            _tests = getTestsSession();
        }

        [Fact]
        public async Task GetTestAsyncReturnAIEnumerableOfTestModel()
        {
            using (var _context = new DbContextGocareer(_options))
            {
                //Arrange
                _context.Tests.AddRange(_tests);
                _context.SaveChanges();

                var controller = new TestsController(_context);

                //Act
                var result = await controller.GetTests();

                //Assert
                Assert.True(typeof(IEnumerable<TestModel>).IsInstanceOfType(result));
                Assert.Equal(2, result.Count());
            }
        }

        [Fact]
        public async Task GetTestByIdReturnAIActionResultWithTest()
        {
            using (var _context = new DbContextGocareer(_options))
            {
                //Arrange
                _context.Tests.AddRange(_tests);
                _context.SaveChanges();

                var controller = new TestsController(_context);

                //Act
                var result = await controller.GetTestById(1);

                //Assert
                Assert.True(typeof(OkObjectResult).IsInstanceOfType(result));
            }
        }

        [Fact]
        public async Task PostTestReturnAnOkObjectResult()
        {
            using (var _context = new DbContextGocareer(_options))
            {
                //Arrange
                CreateTestModel newTest = new CreateTestModel
                {
                    Personalized = true,
                    EspecialistId = 1,
                    Testname = "Test 1"
                };
                var controller = new TestsController(_context);

                //Act
                var result = await controller.PostTest(newTest);

                //Assert
                Assert.True(typeof(OkObjectResult).IsInstanceOfType(result));
            }
        }

        [Fact]
        public async Task PutTestReturnAnOkObjectResult()
        {
            using (var _context = new DbContextGocareer(_options))
            {
                //Arrange
                _context.Tests.AddRange(_tests);
                _context.SaveChanges();
                UpdateTestModel updateTest = new UpdateTestModel
                {
                    Personalized = false,
                };
                var controller = new TestsController(_context);

                //Act
                var result = await controller.PutTest(1, updateTest);

                //Assert
                Assert.True(typeof(OkObjectResult).IsInstanceOfType(result));
            }
        }

        [Fact]
        public async Task DeleteTestReturnAnOkObjectResult()
        {
            using (var _context = new DbContextGocareer(_options))
            {
                //Arrange
                _context.Tests.AddRange(_tests);
                _context.SaveChanges();
                var controller = new TestsController(_context);

                //Act
                var result = await controller.DeleteTest(1);

                //Assert
                Assert.True(typeof(OkObjectResult).IsInstanceOfType(result));
            }
        }

        public List<Test> getTestsSession()
        {
            var tests = new List<Test>();
            tests.Add(new Test
            {
                Testid = 1,
                Personalized = true,
                EspecialistId = 1,
                Testname = "Test 1"

            });
            tests.Add(new Test
            {
                Testid = 2,
                Personalized = false,
                EspecialistId = 1,
                Testname = "Test 2"

            });
            return tests;
        }

    }
}
