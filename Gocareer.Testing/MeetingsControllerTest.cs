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
using Gocareer_backend.Models.Meeting;

namespace Gocareer.Testing
{
    public class MeetingsControllerTest
    {
        private readonly DbContextOptionsBuilder<DbContextGocareer> _builder = new DbContextOptionsBuilder<DbContextGocareer>();
        private readonly DbContextOptions<DbContextGocareer> _options;
        private readonly List<Meeting> _meetings;

        public MeetingsControllerTest()
        {
            _builder.UseInMemoryDatabase("Test");
            _options = _builder.Options;
            _meetings = getMeetingsSession();
        }

        [Fact]
        public async Task GetMeetingAsyncReturnAIEnumerableOfMeetingModel()
        {
            using (var _context = new DbContextGocareer(_options))
            {
                //Arrange
                _context.Meetings.AddRange(_meetings);
                _context.SaveChanges();

                var controller = new MeetingsController(_context);

                //Act
                var result = await controller.GetMeetings();

                //Assert
                Assert.True(typeof(IEnumerable<MeetingModel>).IsInstanceOfType(result));
                Assert.Equal(2, result.Count());
            }
        }

        [Fact]
        public async Task GetMeetingByIdReturnAIActionResultWithArticle()
        {
            using (var _context = new DbContextGocareer(_options))
            {
                //Arrange
                _context.Meetings.AddRange(_meetings);
                _context.SaveChanges();

                var controller = new MeetingsController(_context);

                //Act
                var result = await controller.GetMeetingByid(1);

                //Assert
                Assert.True(typeof(OkObjectResult).IsInstanceOfType(result));
            }
        }

        [Fact]
        public async Task PostMeetingReturnAnOkObjectResult()
        {
            using (var _context = new DbContextGocareer(_options))
            {
                //Arrange
                CreateMeeting newMeeting = new CreateMeeting
                {
                    Date = DateTime.Now,
                    Hour = DateTime.Now,
                    UserId = 1,
                    EspecialistId = 1,
                };
                var controller = new MeetingsController(_context);

                //Act
                var result = await controller.PostMeeting(newMeeting);

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
        //        _context.Estudiantes.AddRange(_estudiantes);
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
        public async Task DeleteMeetingReturnAnOkObjectResult()
        {
            using (var _context = new DbContextGocareer(_options))
            {
                //Arrange
                _context.Meetings.AddRange(_meetings);
                _context.SaveChanges();
                var controller = new MeetingsController(_context);

                //Act
                var result = await controller.DeleteMeeting(1);

                //Assert
                Assert.True(typeof(OkObjectResult).IsInstanceOfType(result));
            }
        }

        public List<Meeting> getMeetingsSession()
        {
            var meetings = new List<Meeting>();
            meetings.Add(new Meeting
            {
                MeetingId = 1,
                Date = DateTime.Now,
                Hour = DateTime.Now,
                UserId = 1,
                EspecialistId = 1

            });
            meetings.Add(new Meeting
            {
                MeetingId = 2,
                Date = DateTime.Now,
                Hour = DateTime.Now,
                UserId = 1,
                EspecialistId = 1

            });
            return meetings;
        }

    }
}
