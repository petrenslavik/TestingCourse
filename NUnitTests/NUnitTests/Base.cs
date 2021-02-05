using Allure.Commons;
using Microsoft.Extensions.Configuration;
using NUnit.Framework;
using NUnit.Framework.Interfaces;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Remote;
using System;
using System.IO;

namespace NUnitTests
{
    public class Base
    {
        [OneTimeSetUp]
        public void Init()
        {
            Environment.CurrentDirectory = Path.GetDirectoryName(GetType().Assembly.Location);
        }

        public static IWebDriver driver;

        public IConfigurationRoot config = new ConfigurationBuilder().AddJsonFile(Path.GetFullPath("appsettings.json")).Build();


        [SetUp]
        public void Setup()
        {
            driver = new ChromeDriver();
            driver.Manage().Window.Maximize();
            driver.Url = config.GetSection("AppSettings")["URL"];
        }
        

        [TearDown]
        public void TearDown()
        {
            if (TestContext.CurrentContext.Result.Outcome != ResultState.Success)
            {
                string path = config.GetSection("AppSettings")["TestResults"];
                var imagename = $"results{DateTime.Now:yyyy-MM-dd_HH-mm-ss.fffff}.png";
                var screenshot = ((ITakesScreenshot)driver).GetScreenshot();
                screenshot.SaveAsFile(path + "/" + imagename);
                AllureLifecycle.Instance.AddAttachment(path + "/" + imagename);
            }
            driver.Close();
            driver.Quit();
        }
    }
}