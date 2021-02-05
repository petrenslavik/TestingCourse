using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Text;

namespace NUnitTests.Pages
{
    public class WishListPage : Page
    {
        public IReadOnlyCollection<IWebElement> Lines => driver.FindElements(By.CssSelector(".wish-grid__cell"));
    }
}
