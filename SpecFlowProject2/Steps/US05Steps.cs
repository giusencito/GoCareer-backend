using System;
using TechTalk.SpecFlow;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using OpenQA.Selenium.Interactions;
using OpenQA.Selenium.Firefox;
using NUnit.Framework;
using Gocareer_backend.Controllers;
using Gocareer.Infrastructure;
using Gocareer.Domain;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;
using System.Collections.Generic;
using System.Linq;
using Gocareer_backend.Models.Message;
using Microsoft.AspNetCore.Mvc;
namespace SpecFlowProject2.Steps
{
    [Binding]
    public class US05Steps
    {
        private readonly DbContextOptionsBuilder<DbContextGocareer> _builder = new DbContextOptionsBuilder<DbContextGocareer>();
        private readonly DbContextOptions<DbContextGocareer> _options;
        private ChromeDriver chromeDriver;
        public US05Steps()
        {
            _builder.UseInMemoryDatabase("Test");
            _options = _builder.Options;
            
        }
        [Given(@"el estudiante ingrese a la plataforma")]
        public void GivenElEstudianteIngreseALaPlataforma()
        {
            chromeDriver.Navigate().GoToUrl("https://gocareer.herokuapp.com/");
            Assert.IsTrue(chromeDriver.Title.ToLower().Contains("GocareerFrontend"));
        }
        [When(@"seleccione un especialista")]
        public void WhenSeleccioneUnEspecialista()
        {
            chromeDriver.Navigate().GoToUrl("https://gocareer.herokuapp.com/User/1/especialistSelect/1");
            Assert.IsTrue(chromeDriver.Title.ToLower().Contains("GocareerFrontend"));
        }
        
        [Then(@"se podrá escribir un mensaje a este")]
        public async Task ThenSePodraEscribirUnMensajeAEste()
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
    }
}
