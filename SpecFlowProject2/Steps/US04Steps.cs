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
using Gocareer_backend.Models.Test;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;
using System.Collections.Generic;
using System.Linq;
namespace SpecFlowProject2.Steps
{
    [Binding]
    public class US04Steps
    {
        private readonly DbContextOptionsBuilder<DbContextGocareer> _builder = new DbContextOptionsBuilder<DbContextGocareer>();
        private readonly DbContextOptions<DbContextGocareer> _options;
        private readonly List<Test> _tests;
        private ChromeDriver chromeDriver;
        public US04Steps()
        {
            _builder.UseInMemoryDatabase("Test");
            _options = _builder.Options;
            _tests = getTestsSession();
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
        [Given(@"el estudiante ingrese a la plataforma")]
        public void GivenElEstudianteIngreseALaPlataforma()
        {
            chromeDriver.Navigate().GoToUrl("https://gocareer.herokuapp.com/");
            Assert.IsTrue(chromeDriver.Title.ToLower().Contains("GocareerFrontend"));
        }
        [When(@"le dé click al botón de test vocacional")]
        public void WhenLeDeClickAlBotonDeTestVocacional()
        {
            chromeDriver.Navigate().GoToUrl("https://gocareer.herokuapp.com/User/1/Test_Vocacional");
        }
        
        [When(@"haya tenido una cita con un especialista")]
        public void WhenHayaTenidoUnaCitaConUnEspecialista()
        {
            
        }
        
        [Then(@"se mostrará un test personalizado")]
        public async Task ThenSeMostraraUnTestPersonalizado()
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
                Assert.AreEqual(2, result.Count());
            }
        }
    }
}
