using Basket.API.Interfaces;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System;
using System.Collections.Generic;
using System.Globalization;

namespace Basket.API.Test
{
    [TestClass]
    public class BasketServiceTest
    {

        private Mock<IBasketService> _mockBasketService;

        [TestInitialize]
        public void Initialize()
        {
            _mockBasketService =  new Mock<IBasketService>();
        }

        [TestMethod]
        public void IsNotBlackFriday()
        {
            
            var basketService = _mockBasketService.Object;
            DateTime date = DateTime.ParseExact("25/11/2022", "dd/MM/yyyy", null);
            bool actual = basketService.IsBlackFriday(date);

            Assert.IsFalse(actual);
        }

        [TestMethod]
        public void IsMatch_InvalidProduct()
        {
            var expected = false;
            var basketService = _mockBasketService.Object;

            IEnumerable<Entities.Product> products = new List<Entities.Product>(){
                    new Entities.Product {
                         Amount = 5,
                         Description = "",
                         IdProduct= 1,
                         Is_gift = false,
                         Title = ""
                    },
                    new Entities.Product {
                         Amount = 5,
                         Description = "",
                         IdProduct= 2,
                         Is_gift = false,
                         Title = ""
                    }
                };
            var actual = basketService.IsMatch(products, 3);

            Assert.AreEqual(expected, actual);
        }


        public bool IsBlackFriday2(DateTime blackFriday)
        {
            return DateTime.Compare(DateTime.Now.Date, blackFriday) == 0 ? true : false;
        }


    }
}