using System;
using TechTalk.SpecFlow;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using OpenQA.Selenium.Interactions;
using OpenQA.Selenium.Firefox;
using NUnit.Framework;
namespace SpecFlowProject2.Steps
{
    [Binding]
    public class US02Steps
    {

        private ChromeDriver chromeDriver;
        [Given(@"el estudiante ingrese a la plataforma")]
        public void GivenElEstudianteIngreseALaPlataforma()
        {
            chromeDriver.Navigate().GoToUrl("https://gocareer.herokuapp.com/");
            Assert.IsTrue(chromeDriver.Title.ToLower().Contains("GocareerFrontend"));
        }

        [When(@"dé click al botón de especialistas")]
        public void WhenDeClickAlBotonDeEspecialistas()
        {
            chromeDriver.Navigate().GoToUrl("https://gocareer.herokuapp.com/");
        }
        
        [When(@"elija una especialista y horario")]
        public void WhenElijaUnaEspecialistaYHorario()
        {
            ScenarioContext.Current.Pending();
        }
        
        [Then(@"programará una cita con el estudiante")]
        public void ThenProgramaraUnaCitaConElEstudiante()
        {
            ScenarioContext.Current.Pending();
        }
    }
}
