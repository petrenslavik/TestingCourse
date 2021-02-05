using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Text;
using NUnitTests.Pages;
using System.Linq;
using OpenQA.Selenium;
using System.Net.Http.Headers;
using T = NUnitTests.Pages.Pages;
using System.Threading;
using NUnit.Allure.Attributes;
using NUnit.Allure.Core;

namespace NUnitTests
{
    [TestFixture]
    [AllureNUnit]
    public class TestCases : Base
    {
        [Test]
        [AllureStory]
        [AllureTag("NUnit")]
        [Category("Search")]
        [Description("Check search functionality")]
        public void CheckSearchFunctionality()
        {
            T.MainPage.SearchBox.SendKeys("відеокарта");
            T.MainPage.SearchBox.SendKeys(Keys.Enter);
            Thread.Sleep(2000);
            Assert.IsTrue(T.MainPage.Products.Any());
        }

        [Test]
        [AllureStory]
        [AllureTag("NUnit")]
        [Category("Basket")]
        [Description("Checking that users can add a products to the basket from the list page")]
        public void AddToBasketFromListPage()
        {
            Helpers.Login();
            Helpers.OpenPage("https://hard.rozetka.com.ua/ua/videocards/c80087/");
            Thread.Sleep(2000);
            Helpers.ClearCart();
            driver.FindElement(By.ClassName("modal__close")).Click();
            var product = T.ListPage.Products.First();
            var selectedProductTitle = product.FindElement(By.CssSelector(".goods-tile__title")).Text.Trim();
            product.FindElement(By.CssSelector(".goods-tile__buy-button")).Click();
            Helpers.OpenCart();
            var lineTitles = T.BasketPopup.BasketLines.Select(l => l.FindElement(By.CssSelector(".cart-product__title")).Text.Trim());
            Assert.IsTrue(lineTitles.Any((productTitle)=>productTitle == selectedProductTitle));
            Helpers.ClearCart();
        }

        [Test]
        [AllureStory]
        [AllureTag("NUnit")]
        [Category("Basket")]
        [Description("Checking that users can add a products to the basket from the details page")]
        public void AddToBasketFromDetailsPage()
        {
            Helpers.OpenPage("https://hard.rozetka.com.ua/msi_gtx_1660_super_ventus_oc/p272261426/");
            var selectedProductTitle = T.DetailsPage.Title.Text.Trim();
            T.DetailsPage.AddToBasketButton.Click();
            Helpers.OpenCart();
            var lineTitles = T.BasketPopup.BasketLines.Select(l => l.FindElement(By.CssSelector(".cart-product__title")).Text.Trim());
            Assert.IsTrue(lineTitles.Any((productTitle) => productTitle == selectedProductTitle));
            Helpers.ClearCart();
        }

        [Test]
        [AllureStory]
        [AllureTag("NUnit")]
        [Category("Wishlist")]
        [Description("Checking that users can add products to the wishlist")]
        public void AddToWishlistFromDetailsPage()
        {
            Helpers.Login();
            Helpers.OpenPage("https://hard.rozetka.com.ua/msi_gtx_1660_super_ventus_oc/p272261426/");
            var selectedProductTitle = T.DetailsPage.Title.Text.Trim();
            T.DetailsPage.AddToWishListButton.Click();
            Helpers.OpenPage("https://rozetka.com.ua/cabinet/wishlist/");
            var lineTitles = T.WishListPage.Lines.Select(l => l.FindElement(By.CssSelector(".goods-tile__title")).Text.Trim());
            Assert.IsTrue(lineTitles.Any((productTitle) => productTitle == selectedProductTitle));
        }

        [Test]
        [AllureStory]
        [AllureTag("NUnit")]
        [Category("Products")]
        [Description("Checking the correct storage of products in the last viewed section")]
        public void LastViewedProducts()
        {
            Helpers.Login();
            Helpers.OpenPage("https://hard.rozetka.com.ua/msi_gtx_1660_super_ventus_oc/p272261426/");
            var firtsProductTitle = T.DetailsPage.Title.Text.Trim();
            Helpers.OpenPage("https://rozetka.com.ua/acer_nh_q7peu_00l/p217184287/");
            var secondProductTitles = T.DetailsPage.Title.Text.Trim();
            Helpers.OpenPage("https://rozetka.com.ua/cabinet/recently-viewed/");
            Thread.Sleep(1000);
            var lastViewedTitles = driver.FindElements(By.CssSelector(".viewed-grid__cell")).Select(p => p.FindElement(By.CssSelector(".goods-tile__title")).Text.Trim()).ToList();
            Console.WriteLine(lastViewedTitles);
            Assert.IsTrue(lastViewedTitles.Any(title => title==firtsProductTitle) && lastViewedTitles.Any(title => title == secondProductTitles));
        }

        [Test]
        [AllureStory]
        [AllureTag("NUnit")]
        [Category("Filters")]
        [Description("Сheck filters functionality works properly")]
        public void FilteringProducts()
        {
            Helpers.Login();
            Helpers.OpenPage("https://hard.rozetka.com.ua/videocards/c80087/price=43-3974;producer=asus;sort=cheap/");
            Thread.Sleep(5000);
            var titles = T.ListPage.Products.Select(p => p.FindElement(By.CssSelector(".goods-tile__title")).Text.Trim());
            var sortPrices = T.ListPage.Products.Select(p => p.FindElement(By.CssSelector(".goods-tile__price-value")).Text.Trim());
            var pricesInt = sortPrices.Select(x => int.Parse(x));
            Assert.That(sortPrices, Is.Ordered);
            Assert.IsFalse(titles.Any(t => !t.Contains("Asus", StringComparison.OrdinalIgnoreCase)));
        }

        [Test]
        [AllureStory]
        [AllureTag("NUnit")]
        [Category("Login")]
        [Description("Check the authorization")]
        public void CheckLogin()
        {
            Helpers.Login();
            Assert.IsTrue(driver.FindElement(By.CssSelector(".header-topline__user-link")).Text.Trim()== "Вячеслав Петренко");
        }

        [Test]
        [AllureStory]
        [AllureTag("NUnit")]
        [Category("Logout")]
        [Description("Check the logout functionality")]
        public void CheckLogout()
        {
            Helpers.Login();
            Helpers.Logout();
            Assert.IsTrue(driver.FindElement(By.CssSelector(".header-topline__user-link")).Text.Trim() != "Вячеслав Петренко");
        }

        [Test]
        [AllureStory]
        [AllureTag("NUnit")]
        [Category("Basket")]
        [Description("Checking that users can delete products from the basket")]
        public void DeleteFromBasket()
        {
            Helpers.OpenPage("https://hard.rozetka.com.ua/msi_gtx_1660_super_ventus_oc/p272261426/");
            Thread.Sleep(2000);
            T.DetailsPage.AddToBasketButton.Click();
            Helpers.ClearCart();
            driver.FindElement(By.ClassName("modal__close")).Click();
            Thread.Sleep(2000);
            Assert.IsTrue(!ExistsElement(".header-actions__button_type_basket .header-actions__button-counter"));
        }

        private bool ExistsElement(string selector)
        {
            try
            {
                driver.FindElement(By.CssSelector(selector));
            }
            catch (NoSuchElementException)
            {
                return false;
            }
            return true;
        }
    }
}
