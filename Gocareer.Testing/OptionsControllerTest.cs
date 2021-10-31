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
using Gocareer_backend.Models.Option;

namespace Gocareer.Testing
{
    public class OptionsControllerTest
    {
        private readonly DbContextOptionsBuilder<DbContextGocareer> _builder = new DbContextOptionsBuilder<DbContextGocareer>();
        private readonly DbContextOptions<DbContextGocareer> _options;
        private readonly List<Option> _optionsgo;

        public OptionsControllerTest()
        {
            _builder.UseInMemoryDatabase("Test");
            _options = _builder.Options;
            _optionsgo = getOptionsSession();
        }

        [Fact]
        public async Task GetOptionAsyncReturnAIEnumerableOfOptionModel()
        {
            using (var _context = new DbContextGocareer(_options))
            {
                //Arrange
                _context.Options.AddRange(_optionsgo);
                _context.SaveChanges();

                var controller = new OptionsController(_context);

                //Act
                var result = await controller.GetOptions();

                //Assert
                Assert.True(typeof(IEnumerable<OptionModel>).IsInstanceOfType(result));
                Assert.Equal(2, result.Count());
            }
        }

        [Fact]
        public async Task GetOptionByIdReturnAIActionResultWithOption()
        {
            using (var _context = new DbContextGocareer(_options))
            {
                //Arrange
                _context.Options.AddRange(_optionsgo);
                _context.SaveChanges();

                var controller = new OptionsController(_context);

                //Act
                var result = await controller.GetOptionById(1);

                //Assert
                Assert.True(typeof(OkObjectResult).IsInstanceOfType(result));
            }
        }

        [Fact]
        public async Task PostOptionReturnAnOkObjectResult()
        {
            using (var _context = new DbContextGocareer(_options))
            {
                //Arrange
                CreateOptionModel newOption = new CreateOptionModel
                {
                    OptionName = "Test2",
                    Points = 1,
                    QuestionId = 1,
                };
                var controller = new OptionsController(_context);

                //Act
                var result = await controller.PostOption(newOption);

                //Assert
                Assert.True(typeof(OkObjectResult).IsInstanceOfType(result));
            }
        }

        [Fact]
        public async Task PutOptionReturnAnOkObjectResult()
        {
            using (var _context = new DbContextGocareer(_options))
            {
                //Arrange
                _context.Options.AddRange(_optionsgo);
                _context.SaveChanges();
                UpdateOptionModel updateOption = new UpdateOptionModel
                {
                    Points = 2,
                };
                var controller = new OptionsController(_context);

                //Act
                var result = await controller.PutOption(1, updateOption);

                //Assert
                Assert.True(typeof(OkObjectResult).IsInstanceOfType(result));
            }
        }

        [Fact]
        public async Task DeleteOptionReturnAnOkObjectResult()
        {
            using (var _context = new DbContextGocareer(_options))
            {
                //Arrange
                _context.Options.AddRange(_optionsgo);
                _context.SaveChanges();
                var controller = new OptionsController(_context);

                //Act
                var result = await controller.DeleteOption(1);

                //Assert
                Assert.True(typeof(OkObjectResult).IsInstanceOfType(result));
            }
        }

        public List<Option> getOptionsSession()
        {
            var options = new List<Option>();
            options.Add(new Option
            {
                Optionid = 1,
                OptionName = "Test1",
                Points = 1,
                QuestionId = 1,

            });
            options.Add(new Option
            {
                Optionid = 2,
                OptionName = "Test2",
                Points = 2,
                QuestionId = 1,

            });
            return options;
        }
    }
}
