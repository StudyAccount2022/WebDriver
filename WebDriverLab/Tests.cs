using System;
using System.Threading;
using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Edge;
using WebDriverLab.Helpers;
using WebDriverLab.Pages;

namespace WebDriverLab
{
    [TestFixture]
    class Test
    {
        WebDriver driver;

        [SetUp]
        public void Startup()
        {
            driver = new EdgeDriver();
            driver.Manage().Window.Maximize();
            driver.Manage().Timeouts().PageLoad = TimeSpan.FromSeconds(200);
            driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(200);
        }

        [Test]
        public void IsInaccessibilityMessageShownCorrectly_ReturnsTrue()
        {
            var tradingPage = new CartPage(driver);
            tradingPage.OpenPage();
            tradingPage.OpenLoginWindow();
            tradingPage.InputLogin();
            tradingPage.InputPasswordAndConfirm();
            tradingPage.GoToCart();

            Assert.IsTrue(tradingPage.IsInaccessibilityMessageShownCorrectly);
        }

        [Test]
        public void TradeUp_DefaultValidValues_ReturnsTrue()
        {
            var tradingPage = new CartPage(driver);
            tradingPage.OpenPage();
            tradingPage.OpenLoginWindow();
            tradingPage.InputLogin();
            tradingPage.InputPasswordAndConfirm();
            tradingPage.GoToCart();

            var expectedTotal = tradingPage.CurrentSize;
            var expectedDateTime = DateTime.Now.TruncateSecond().TruncateMillisecond();
 
            tradingPage.Buy();
            Thread.Sleep(5000);            
            tradingPage.InitializeBoots();

            var assertAccumulator = new AssertAccumulator();
            assertAccumulator.Accumulate(() => Assert.AreEqual(expectedTotal, tradingPage.BootsTotalLabel.Text));
            assertAccumulator.Accumulate(() => Assert.AreEqual(expectedDateTime, tradingPage.BootsDateDelivery));
            assertAccumulator.Release();
        }
    }
}
