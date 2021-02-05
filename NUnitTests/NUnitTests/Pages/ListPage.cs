using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;

namespace NUnitTests.Pages
{
    public class ListPage : Page
    {
        public ReadOnlyCollection<IWebElement> Products => driver.FindElements(By.CssSelector("li.catalog-grid__cell"));
    }
}
