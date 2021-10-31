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
    public class US10Steps
    {
        private ChromeDriver chromeDriver;
        [Given(@"el especialista ingrese a la plataforma")]
        public void GivenElEspecialistaIngreseALaPlataforma()
        {
            chromeDriver.Navigate().GoToUrl("https://gocareer.herokuapp.com/");
            Assert.IsTrue(chromeDriver.Title.ToLower().Contains("GocareerFrontend"));
        }
        
        [When(@"esté en su perfil")]
        public void WhenEsteEnSuPerfil()
        {
            chromeDriver.Navigate().GoToUrl("https://gocareer.herokuapp.com/Especialist/1/Profile");
            Assert.IsTrue(chromeDriver.Title.ToLower().Contains("GocareerFrontend"));
        }
        
        [When(@"selecciona la opción de reserva de consultas")]
        public void WhenSeleccionaLaOpcionDeReservaDeConsultas()
        {
            chromeDriver.Navigate().GoToUrl("https://gocareer.herokuapp.com/Especialist/1/Profile/Meetings");
            Assert.IsTrue(chromeDriver.Title.ToLower().Contains("GocareerFrontend"));
        }
        
        [When(@"seleccione la cita acordada")]
        public void WhenSeleccioneLaCitaAcordada()
        {
            chromeDriver.Navigate().GoToUrl("https://gocareer.herokuapp.com/Especialist/1/Profile/Meetings");
            Assert.IsTrue(chromeDriver.Title.ToLower().Contains("GocareerFrontend"));
        }
        
        [Then(@"podrá estar en una reunión con el estudiante")]
        public void ThenPodraEstarEnUnaReunionConElEstudiante()
        {
            chromeDriver.Navigate().GoToUrl("https://gocareer.herokuapp.com/Especialist/1/Profile/Meetings/1/2");
            Assert.IsTrue(chromeDriver.Title.ToLower().Contains("GocareerFrontend"));
        }
    }
}
