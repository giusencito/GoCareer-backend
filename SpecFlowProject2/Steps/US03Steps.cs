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
    public class US03Steps
    {

        private ChromeDriver chromeDriver;
        [Given(@"el estudiante ingrese a la plataforma")]
        public void GivenElEstudianteIngreseALaPlataforma()
        {
            chromeDriver.Navigate().GoToUrl("https://gocareer.herokuapp.com/");
            Assert.IsTrue(chromeDriver.Title.ToLower().Contains("GocareerFrontend"));
        }
        [When(@"click al botón de carreras universitarias")]
        public void WhenClickAlBotonDeCarrerasUniversitarias()
        {
            chromeDriver.Navigate().GoToUrl("https://gocareer.herokuapp.com/User/1/Careers");
        }
        
        [When(@"Y elija una carrera")]
        public void WhenYElijaUnaCarrera()
        {
            chromeDriver.Navigate().GoToUrl("https://gocareer.herokuapp.com/User/1/Careers/1");
        }
        
        [Then(@"se mostrará información acerca de la carrera seleccionada")]
        public void ThenSeMostraraInformacionAcercaDeLaCarreraSeleccionada()
        {
            chromeDriver.Navigate().GoToUrl("https://gocareer.herokuapp.com/User/1/Careers/1");
        }
    }
}
