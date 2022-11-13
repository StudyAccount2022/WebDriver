using System;
using OpenQA.Selenium;
using WebDriverLab.Helpers;

namespace WebDriverLab.Pages
{
    class CartPage : Bases.BasePage
    {
        private const string _homepage = @"https://www.reebok.pl/";
        private const string _login = "myMail778023@gmail.com";
        private const string _password = "khJKGadas99HKJ3khl)";

        private string _inaccessibilityMessageBuffer;

        public int BootsId { get; set; }
        public DateTime BootsDateDelivery { get; set; }
        public DateTime SalesDate { get; set; }

        public IWebElement LogInButton => _wait.Until(_driver => _driver.FindElement(By.CssSelector(".main-header__btn.btn.btn--black.p-open")));
        public IWebElement GoToCartButton => _wait.Until(_driver => _driver.FindElement(By.ClassName("welcome__btn")));
        public IWebElement BuyButton => _wait.Until(_driver => _driver.FindElement(By.CssSelector(".trading-btn.trading-btn2.trading-btn--up.content_btn_call.content_btn_call2")));

        public IWebElement LoginInput => _wait.Until(_driver => _driver.FindElement(By.Id("input-log-in1")));
        public IWebElement PassordInput => _wait.Until(_driver => _driver.FindElement(By.Id("input-log-in2")));
        public IWebElement TotalInput => _wait.Until(_driver => _driver.FindElement(By.Id("boots")));

        public IWebElement SizeLabel => _wait.Until(_driver => _driver.FindElement(By.ClassName("size-label")));
        public IWebElement Boots => _wait.Until(_driver => _driver.FindElement(By.XPath("/html/body/div[5]/div/div[3]/div[5]/div[2]/div[1]/table/tbody/tr[1]/th[2]"))); 
        public IWebElement BootsTotalLabel => _wait.Until(_driver => _driver.FindElement(By.Id($"total_{BootsId}")));

        public bool IsInaccessibilityMessageShown => TryToGetBoots(out _inaccessibilityMessageBuffer) && !string.IsNullOrEmpty(_inaccessibilityMessageBuffer);
        public bool IsShopWorking =>  IsTodayIsWorkDay && IsNowIsWorkTime;
        public bool IsInaccessibilityMessageShownCorrectly => IsInaccessibilityMessageShown ^ IsShopWorking;
        public string CurrentSize => string.Concat(TotalInput.GetAttribute("value"), ' ', SizeLabel.Text);


        public CartPage(IWebDriver driver) : base(driver)
        {
        }

        public void OpenPage()
        {
            _driver.Url = _homepage;
        }

        public void RefreshPage()
        {
            _driver.Navigate().Refresh();
        }

        public void OpenLoginWindow()
        {
            LogInButton.Click();
        }

        public void InputLogin()
        {
            LoginInput.SendKeys(_login);
        }

        public void InputPasswordAndConfirm()
        {
            PassordInput.SendKeys(_password + Keys.Enter);
        }

        public void GoToCart()
        {
            GoToCartButton.Click();
        }

        public bool TryToGetBoots(out string _inaccessibilityMessage)
        {
            try
            {
                IWebElement bootsUnavalable = _wait.Until(_driver => _driver.FindElement(By.CssSelector("#modal_close_time > .pop-up.pop-up-log-in.pop-up-setting > .pop-up-setting__title")));
                _inaccessibilityMessage = bootsUnavalable.Text;
                return true;
            }
            catch(OpenQA.Selenium.WebDriverException e)
            {
                _inaccessibilityMessage = string.Empty;
                return false;
            }
        }

        public void Buy()
        {
            BuyButton.Click();            
        }

        public void InitializeBoots()
        {
            var boots = Boots.Text;

            BootsId = int.Parse(boots.Substring(0, 9));
            BootsDateDelivery = DateTime.Parse(boots.Substring(11, 19)).TruncateSecond().TruncateMillisecond();
            SalesDate = DateTime.Parse(boots.Substring(32, 19)).TruncateSecond().TruncateMillisecond();
        }

        private bool IsTodayIsWorkDay => DateTime.Now.DayOfWeek >= (DayOfWeek)1 && DateTime.Now.DayOfWeek <= (DayOfWeek)5;
        private bool IsNowIsWorkTime => DateTime.Now.Hour >= 4 && DateTime.Now.Hour <= 23;
    }
}
