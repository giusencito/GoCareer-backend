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
using Gocareer_backend.Models.Question;

namespace Gocareer.Testing
{
    public class QuestionsControllerTest
    {
        private readonly DbContextOptionsBuilder<DbContextGocareer> _builder = new DbContextOptionsBuilder<DbContextGocareer>();
        private readonly DbContextOptions<DbContextGocareer> _options;
        private readonly List<Question> _questions;

        public QuestionsControllerTest()
        {
            _builder.UseInMemoryDatabase("Test");
            _options = _builder.Options;
            _questions = getQuestionsSession();
        }

        [Fact]
        public async Task GetQuestionAsyncReturnAIEnumerableOfQuestionModel()
        {
            using (var _context = new DbContextGocareer(_options))
            {
                //Arrange
                _context.Questions.AddRange(_questions);
                _context.SaveChanges();

                var controller = new QuestionsController(_context);

                //Act
                var result = await controller.GetQuestions();

                //Assert
                Assert.True(typeof(IEnumerable<QuestionModel>).IsInstanceOfType(result));
                Assert.Equal(2, result.Count());
            }
        }

        [Fact]
        public async Task GetQuestionByIdReturnAIActionResultWithQuestion()
        {
            using (var _context = new DbContextGocareer(_options))
            {
                //Arrange
                _context.Questions.AddRange(_questions);
                _context.SaveChanges();

                var controller = new QuestionsController(_context);

                //Act
                var result = await controller.GetQuestionById(1);

                //Assert
                Assert.True(typeof(OkObjectResult).IsInstanceOfType(result));
            }
        }

        [Fact]
        public async Task PostQuestionReturnAnOkObjectResult()
        {
            using (var _context = new DbContextGocareer(_options))
            {
                //Arrange
                CreateQuestionModel newQuestion = new CreateQuestionModel
                {
                    QuestionName = "Test2",
                    Score = 2,
                    Testid = 1,
                };
                var controller = new QuestionsController(_context);

                //Act
                var result = await controller.PostQuestion(newQuestion);

                //Assert
                Assert.True(typeof(OkObjectResult).IsInstanceOfType(result));
            }
        }

        [Fact]
        public async Task PutQuestionReturnAnOkObjectResult()
        {
            using (var _context = new DbContextGocareer(_options))
            {
                //Arrange
                _context.Questions.AddRange(_questions);
                _context.SaveChanges();
                UpdateQuestionModel updateQuestion = new UpdateQuestionModel
                {
                    Score = 5,
                };
                var controller = new QuestionsController(_context);

                //Act
                var result = await controller.PutQuestion(1, updateQuestion);

                //Assert
                Assert.True(typeof(OkObjectResult).IsInstanceOfType(result));
            }
        }

        [Fact]
        public async Task DeleteQuestionReturnAnOkObjectResult()
        {
            using (var _context = new DbContextGocareer(_options))
            {
                //Arrange
                _context.Questions.AddRange(_questions);
                _context.SaveChanges();
                var controller = new QuestionsController(_context);

                //Act
                var result = await controller.DeleteQuestion(1);

                //Assert
                Assert.True(typeof(OkObjectResult).IsInstanceOfType(result));
            }
        }

        public List<Question> getQuestionsSession()
        {
            var questions = new List<Question>();
            questions.Add(new Question
            {
                QuestionId = 1,
                QuestionName = "Test1",
                Score = 4,
                Testid = 1,

            });
            questions.Add(new Question
            {
                QuestionId = 2,
                QuestionName = "Test2",
                Score = 2,
                Testid = 1,

            });
            return questions;
        }
    }
}
