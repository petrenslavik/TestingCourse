using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Text;

namespace NUnitTests.Pages
{
    public class LoginPopup : Page
    {
        public IWebElement EmailField => driver.FindElement(By.CssSelector("#auth_email"));
        
        public IWebElement PasswordField => driver.FindElement(By.CssSelector("#auth_pass"));
        
        public IWebElement EnterButton => driver.FindElement(By.CssSelector(".auth-modal__submit"));
    }
}
