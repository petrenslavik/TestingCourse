using System;
using System.Collections.Generic;
using System.Text;

namespace NUnitTests.Pages
{
    public class Pages
    {
        public static MainPage MainPage => new MainPage();

        public static ListPage ListPage => new ListPage();

        public static BasketPopup BasketPopup => new BasketPopup();

        public static DetailsPage DetailsPage => new DetailsPage();

        public static WishListPage WishListPage => new WishListPage();

        public static LoginPopup LoginPopup => new LoginPopup();
    }
}
