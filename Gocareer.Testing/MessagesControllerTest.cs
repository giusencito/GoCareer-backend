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
using Gocareer_backend.Models.Message;

namespace Gocareer.Testing
{
    public class MessagesControllerTest
    {
        private readonly DbContextOptionsBuilder<DbContextGocareer> _builder = new DbContextOptionsBuilder<DbContextGocareer>();
        private readonly DbContextOptions<DbContextGocareer> _options;
        private readonly List<Message> _messages;

        public MessagesControllerTest()
        {
            _builder.UseInMemoryDatabase("Test");
            _options = _builder.Options;
            _messages = getMessagesSession();
        }

        [Fact]
        public async Task GetMessageAsyncReturnAIEnumerableOfMessageModel()
        {
            using (var _context = new DbContextGocareer(_options))
            {
                //Arrange
                _context.Messages.AddRange(_messages);
                _context.SaveChanges();

                var controller = new MessagesController(_context);

                //Act
                var result = await controller.GetMessages();

                //Assert
                Assert.True(typeof(IEnumerable<MessageModel>).IsInstanceOfType(result));
                Assert.Equal(2, result.Count());
            }
        }

        [Fact]
        public async Task GetMessageByIdReturnAIActionResultWithArticle()
        {
            using (var _context = new DbContextGocareer(_options))
            {
                //Arrange
                _context.Messages.AddRange(_messages);
                _context.SaveChanges();

                var controller = new MessagesController(_context);

                //Act
                var result = await controller.GetMessageById(1);

                //Assert
                Assert.True(typeof(OkObjectResult).IsInstanceOfType(result));
            }
        }

        [Fact]
        public async Task PostMessageReturnAnOkObjectResult()
        {
            using (var _context = new DbContextGocareer(_options))
            {
                //Arrange
                CreateMessageModel newMessage = new CreateMessageModel
                {
                    MessageDescription = "Test2",
                    answer = "answer2",
                    UserId = 1,
                    EspecialistId = 1,
                };
                var controller = new MessagesController(_context);

                //Act
                var result = await controller.PostMessage(newMessage);

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
        //        _context.Estudiantes.AddRange(_messages);
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
        public async Task DeleteMessageReturnAnOkObjectResult()
        {
            using (var _context = new DbContextGocareer(_options))
            {
                //Arrange
                _context.Messages.AddRange(_messages);
                _context.SaveChanges();
                var controller = new MessagesController(_context);

                //Act
                var result = await controller.DeleteMessage(1);

                //Assert
                Assert.True(typeof(OkObjectResult).IsInstanceOfType(result));
            }
        }

        public List<Message> getMessagesSession()
        {
            var messages = new List<Message>();
            messages.Add(new Message
            {
                Messageid = 1,
                MessageDescription = "Test1",
                answer = "answer",
                UserId = 1,
                EspecialistId = 1

            });
            messages.Add(new Message
            {
                Messageid = 2,
                MessageDescription = "Test2",
                answer = "answer 2",
                UserId = 1,
                EspecialistId = 1

            });
            return messages;
        }
    }
}
