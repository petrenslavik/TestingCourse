using NUnitTests.Pages;
using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using OpenQA.Selenium.Interactions;
using T = NUnitTests.Pages.Pages;

namespace NUnitTests
{
    public class Helpers : Base
    {
        private static bool IsCartOpened { get; set; }
        public static void OpenPage(string url)
        {
            driver.Navigate().GoToUrl(url);
            Thread.Sleep(2000);
            IsCartOpened = false;
        }

        public static void Login(string login = "pslavindiks@gmail.com", string pass = "sQqTDXM7yrxsk5t")
        {
            Thread.Sleep(2000);
            T.MainPage.LoginButton.Click();
            Thread.Sleep(2000);
            T.LoginPopup.EmailField.SendKeys(login);
            T.LoginPopup.PasswordField.SendKeys(pass);
            T.LoginPopup.EnterButton.Click();
            Thread.Sleep(2000);
            IsCartOpened = false;
        }

        public static void Logout()
        {
            Actions action = new Actions(driver);
            var menuElement = driver.FindElement(By.CssSelector(".header-topline__user-link"));
            var logoutMenuItem = driver.FindElement(By.CssSelector(".header-dropdown__list-item:last-child"));
            var logoutButton = logoutMenuItem.FindElement(By.CssSelector(".header-dropdown__list-link"));
            action.MoveToElement(menuElement).MoveToElement(logoutMenuItem).MoveToElement(logoutButton).Click().Build().Perform();
            Thread.Sleep(2000);
            IsCartOpened = false;
        }

        public static void OpenCart()
        {
            var cartElement = driver.FindElement(By.CssSelector(".header-actions__button_type_basket"));
            cartElement.Click();
            Thread.Sleep(2000);
            IsCartOpened = true;
        }

        public static void ClearCart()
        {
            if (!IsCartOpened)
            {
                OpenCart();
            }

            foreach (var basketPopupBasketLine in T.BasketPopup.BasketLines)
            {
                var menu = basketPopupBasketLine.FindElement(By.CssSelector("button[id^='cartProductActions']"));
                Actions action = new Actions(driver);
                action.MoveToElement(menu).Click().Build().Perform();
                Thread.Sleep(500);
                var deleteButton = driver.FindElement(By.CssSelector("rz-trash-icon"));
                action = new Actions(driver);
                action.MoveToElement(deleteButton).Click().Build().Perform();
                Thread.Sleep(2000);
            }

            IsCartOpened = false;
        }
    }
}
