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
using Gocareer_backend.Models.Message;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;
namespace SpecFlowProject2.Steps
{
    [Binding]
    public class US08Steps
    {
        private ChromeDriver chromeDriver;
        [Given(@"el especialista ingrese a la plataforma")]
        public void WhenElEspecialistaIngreseALaPlataforma()
        {
            chromeDriver.Navigate().GoToUrl("https://gocareer.herokuapp.com/");
            Assert.IsTrue(chromeDriver.Title.ToLower().Contains("GocareerFrontend"));
        }
        [When(@"le dé click al botón de mensajería")]
        public void WhenLeDeClickAlBotonDeMensajeria()
        {
            chromeDriver.Navigate().GoToUrl("https://gocareer.herokuapp.com/Especialist/1/Messages");
        }
        
        [When(@"elija un mensaje")]
        public void WhenElijaUnMensaje()
        {
            chromeDriver.Navigate().GoToUrl("https://gocareer.herokuapp.com/Especialist/1/Messages/1");
        }
        
        [Then(@"se podrá redactar una respuesta a este")]
        public void ThenSePodraRedactarUnaRespuestaAEste()
        {
            chromeDriver.Navigate().GoToUrl("https://gocareer.herokuapp.com/Especialist/1/Messages/1");
        }
    }
}
