using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;

namespace NUnitTests.Pages
{
    public class MainPage : Page
    {
        public IWebElement SearchBox => driver.FindElement(By.CssSelector(".search-form__input"));
        
        public ReadOnlyCollection<IWebElement> Products => driver.FindElements(By.CssSelector(".catalog-grid__cell"));

        public IWebElement LoginButton => driver.FindElement(By.CssSelector(".header-topline__user-link"));
    }
}
