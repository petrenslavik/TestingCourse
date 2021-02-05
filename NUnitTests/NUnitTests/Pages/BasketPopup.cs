using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;

namespace NUnitTests.Pages
{
    public class BasketPopup : Page
    {
        public ReadOnlyCollection<IWebElement> BasketLines => driver.FindElements(By.CssSelector(".cart-list__item"));
        
        public IWebElement ClearBasketButton => driver.FindElement(By.CssSelector(".empty_cart.dotted.dotted-blue.dotted-remove"));
    }
}
