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
    public class US06Steps
    {
        private ChromeDriver chromeDriver;
        [Given(@"el estudiante ingrese a la plataforma")]
        public void GivenElEstudianteIngreseALaPlataforma()
        {
            chromeDriver.Navigate().GoToUrl("https://gocareer.herokuapp.com/");
            Assert.IsTrue(chromeDriver.Title.ToLower().Contains("GocareerFrontend"));
        }
        [When(@"termine el test vocacional")]
        public void WhenTermineElTestVocacional()
        {
            chromeDriver.Navigate().GoToUrl("https://gocareer.herokuapp.com/User/1/Test_Vocacional");
            Assert.IsTrue(chromeDriver.Title.ToLower().Contains("GocareerFrontend"));
        }
        
        [Then(@"tse podrá visualizar los trabajos disponibles según el resultado del test")]
        public void ThenTsePodraVisualizarLosTrabajosDisponiblesSegunElResultadoDelTest()
        {
            ScenarioContext.Current.Pending();
        }
    }
}
