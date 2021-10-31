using System;
using TechTalk.SpecFlow;
using NUnit.Framework;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using OpenQA.Selenium.Interactions;
using OpenQA.Selenium.Firefox;
using Gocareer_backend.Controllers;
using Gocareer.Infrastructure;
using Gocareer.Domain;
using Gocareer_backend.Models.Especialist;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;
namespace SpecFlowProject2.Steps
{
    [Binding]
    public class US07Steps
    {
        private ChromeDriver chromeDriver;
        private readonly DbContextOptionsBuilder<DbContextGocareer> _builder = new DbContextOptionsBuilder<DbContextGocareer>();
        private readonly DbContextOptions<DbContextGocareer> _options;
        CreateEspecialistModel newEspecialist;

        [Given(@"el especialista ingrese a la plataforma")]
        public void WhenElEspecialistaIngreseALaPlataforma()
        {
            chromeDriver.Navigate().GoToUrl("https://gocareer.herokuapp.com/");
            Assert.IsTrue(chromeDriver.Title.ToLower().Contains("GocareerFrontend"));
        }
        
        [When(@"elija una opcion de registro")]
        public void WhenElijaUnaOpcionDeRegistro()
        {
            chromeDriver.Navigate().GoToUrl("https://gocareer.herokuapp.com/EspecialistRegister");
        }
        
        [When(@"llene sus datos personales")]
        public void WhenLleneSusDatosPersonales()
        {
            newEspecialist = new CreateEspecialistModel
            {
                EspecialistName = "Test2",
                EspecialistLastName = "Two",
                EspecialistEmail = "testing@gmail.com",
                EspecialistPassword = "12345",
                EspecialistInformation = "a information testing"
            };
        }
        
        [Then(@"se creara la cuenta para el especialista")]
        public async Task ThenSeCrearaLaCuentaParaElEspecialista()
        {
            using (var _context = new DbContextGocareer(_options))
            {
                //Arrange

                var controller = new EspecialistsController(_context);

                //Act
                var result = await controller.PostEspecialist(newEspecialist);

                //Assert
                Assert.True(typeof(OkObjectResult).IsInstanceOfType(result));
            }
        }
    }
}
