using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Text;

namespace NUnitTests.Pages
{
    public class DetailsPage : Page
    {
        public IWebElement AddToBasketButton => driver.FindElement(By.CssSelector(".product-trade app-buy-button"));

        public IWebElement Title => driver.FindElement(By.CssSelector(".product__title"));

        public IWebElement AddToWishListButton => driver.FindElement(By.CssSelector(".js-wish-button"));
    }
}
