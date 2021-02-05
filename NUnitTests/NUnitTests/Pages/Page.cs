using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Text;

namespace NUnitTests.Pages
{
    public class Page
    {
        public IWebDriver driver;

        public Page()
        {
            driver = Base.driver;
        }
    }
}
