﻿using System;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;

namespace WebDriverLab.Pages.Bases
{
    public class BasePage
    {
        protected IWebDriver _driver;
        protected WebDriverWait _wait;

        public BasePage(IWebDriver driver)
        {
            _driver = driver;
            _wait = new WebDriverWait(driver, TimeSpan.FromSeconds(60));
        }


        #region Private and protected methods
        protected DefaultWait<IWebDriver> FluentWait()
        {
            var fluentWait = new DefaultWait<IWebDriver>(_driver)
            {
                Timeout = TimeSpan.FromSeconds(60),
                PollingInterval = TimeSpan.FromSeconds(1)
            };
            return fluentWait;
        }

        protected IWebElement WaitUntilClickable(By by)
        {
            var fluentWait = FluentWait();
            fluentWait.IgnoreExceptionTypes(typeof(Exception));
            return fluentWait.Until(driver =>
            {
                IWebElement tempElement = _driver.FindElement(by);
                return (tempElement.Displayed && tempElement.Enabled) ? tempElement : null;
            });
        }

        protected void WaitAndClick(By by)
        {
            WaitUntilClickable(by).Click();
        }

        #endregion
    }
}
