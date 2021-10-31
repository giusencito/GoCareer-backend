
using System;
using TechTalk.SpecFlow;
//using Xunit;
using NUnit;
using NUnit.Framework;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using OpenQA.Selenium.Interactions;
using OpenQA.Selenium.Firefox;
using Gocareer_backend.Controllers;
using Gocareer.Infrastructure;
using Gocareer.Domain;
using Gocareer_backend.Models.Estudiantes;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;
namespace SpecFlowProject2.Steps
{
    [Binding]
    public class US01Steps 
    {

        private ChromeDriver chromeDriver;
        private readonly DbContextOptionsBuilder<DbContextGocareer> _builder = new DbContextOptionsBuilder<DbContextGocareer>();
        private readonly DbContextOptions<DbContextGocareer> _options;
        CreateEstudianteModel newEstudiante;

        public US01Steps()
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
        
        [When(@"elija una opción de registro")]
        public void WhenElijaUnaOpcionDeRegistro()
        {
            chromeDriver.Navigate().GoToUrl("https://gocareer.herokuapp.com/studentregister");
        }
        
        [When(@"llene sus datos")]
        public void WhenLleneSusDatos()
        {
            newEstudiante = new CreateEstudianteModel
            {
                UserName = "Test2",
                UserLastname = "Two",
                Useremail = "testing@gmail.com",
                UserPassword = "123456",
            };
        }
        
        [Then(@"se creara la cuenta")]
        public async Task ThenSeCrearaLaCuenta()
        {
            using (var _context = new DbContextGocareer(_options))
            {
                //Arrange
                
                var controller = new EstudiantesController(_context);

                //Act
                var result = await controller.PostEstudiante(newEstudiante);

                //Assert
                Assert.True(typeof(OkObjectResult).IsInstanceOfType(result));
            }
        }
    }
}
