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
    public class US09Steps
    {
        private ChromeDriver chromeDriver;
        [Given(@"el especialista ingrese a la plataforma")]
        public void WhenElEspecialistaIngreseALaPlataforma()
        {
            chromeDriver.Navigate().GoToUrl("https://gocareer.herokuapp.com/");
            Assert.IsTrue(chromeDriver.Title.ToLower().Contains("GocareerFrontend"));
        }
        [When(@"elija crear test personalizado")]
        public void WhenElijaCrearTestPersonalizado()
        {
            chromeDriver.Navigate().GoToUrl("https://gocareer.herokuapp.com/Especialist/1/Testselector");
            Assert.IsTrue(chromeDriver.Title.ToLower().Contains("GocareerFrontend"));
        }
        
        [Then(@"se podrá crear las preguntas y respuestas del test")]
        public void ThenSePodraCrearLasPreguntasYRespuestasDelTest()
        {
            chromeDriver.Navigate().GoToUrl("https://gocareer.herokuapp.com/Especialist/1/Testselector/perzo");
            Assert.IsTrue(chromeDriver.Title.ToLower().Contains("GocareerFrontend"));
        }
    }
}
