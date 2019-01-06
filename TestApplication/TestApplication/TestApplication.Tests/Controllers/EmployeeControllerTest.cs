using System;
using System.Threading;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Support.UI;

namespace TestApplication.Tests.Controllers
{
    [TestClass]
    public class EmployeeControllerTest
    {
        private const string BrowserDriverPath = @"C:\SeleniumDriver";
        private const string ScreenShotLocation = @"C:\ScreenShot";

        [TestMethod]
        public void GetComboBox()
        {
            IWebDriver driver = null;
            try
            {
                driver = new ChromeDriver(BrowserDriverPath);
                driver.Navigate().GoToUrl("http://tickets.vueling.com/");

                var kids = driver.FindElement(By.Id("AvailabilitySearchInputSearchView_DropDownListPassengerType_CHD"));
                var selectElement = new SelectElement(kids);
                selectElement.SelectByValue("2");

                driver.FindElement(By.Id("AvailabilitySearchInputSearchView_TextBoxMarketOrigin1")).Click();
                var stationListOrigin = driver.FindElement(By.Id("stationsList"));
                var barcelona = stationListOrigin.FindElement(By.LinkText("Barcelona, España (BCN)"));
                barcelona.Click();

                var stationListDestination = driver.FindElement(By.Id("stationsList"));
                var amsterdam = stationListDestination.FindElement(By.LinkText("Ámsterdam, Países Bajos (AMS)"));
                amsterdam.Click();
            }
            catch (Exception ex)
            {
                // ignored
            }
            finally
            {
                driver?.Quit();
            }
        }

        [TestMethod]
        public void Get()
        {
            IWebDriver driver = null;
            try
            {
                driver = new ChromeDriver(BrowserDriverPath);
                driver.Navigate().GoToUrl("http://localhost:60006/");

                // Find the text input element by its name
                var enter = driver.FindElement(By.TagName("a"));
                enter.Click();

                TestEmployeeListScreen(driver, "Launch");
                TestAddEmployee(driver);
                TestEmployeeListScreen(driver, "Add");
                TestEditEmployee(driver);
                TestEmployeeListScreen(driver, "Edit");
                TestDeleteEmployees(driver);
                TestEmployeeListScreen(driver, "AfterDelete");
            }
            catch (Exception)
            {
                // ignored
            }
            finally
            {
                driver?.Quit();
            }
        }

        private void TestAddEmployee(IWebDriver driver)
        {
            driver.FindElement(By.LinkText("Add")).Click();
            var wait = new WebDriverWait(driver, TimeSpan.FromSeconds(10));
            var name = wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementIsVisible(By.Id("Name")));
            var sal = driver.FindElement(By.Id("Sal"));
            var submit = driver.FindElement(By.Id("Submit"));

            name.SendKeys("Natasha");
            sal.SendKeys("5000");
            submit.Click();

            IAlert alert = null;
            alert = wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.AlertIsPresent());
            alert.Accept();
        }

        private void TestEditEmployee(IWebDriver driver)
        {
            //Find the element anchor tag for the employee having name as 'Anderson'
            var anchorTag = (IWebElement)((IJavaScriptExecutor)driver).ExecuteScript("return $('a',$(\"td:contains('Anderson')\").parent())[0]");
            anchorTag.Click();

            //Wait and then check until the control with id=Name is available
            var wait = new WebDriverWait(driver, TimeSpan.FromSeconds(10));
            var name = wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementIsVisible(By.Id("Name")));
            var submit = driver.FindElement(By.Id("Submit"));

            //Set the data (Change the name)
            name.SendKeys(" James");
            submit.Click();

            IAlert alert = null;
            alert = wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.AlertIsPresent());
            alert.Accept();
        }

        private void TestDeleteEmployees(IWebDriver driver)
        {
            //Find the last employee
            var checkBox = (IWebElement)((IJavaScriptExecutor)driver).ExecuteScript("return $('input:checkbox:last')[0]");
            checkBox.Click();

            //Find delete button and click
            var delBtn = driver.FindElement(By.Id("Delete"));
            Thread.Sleep(1000);
            delBtn.Click(); // Perform delete operation
            Thread.Sleep(1000);
        }

        public void TestEmployeeListScreen(IWebDriver driver, string type)
        {
            var wait = new WebDriverWait(driver, TimeSpan.FromSeconds(10));
            wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.TitleIs("EmployeeList"));
            var ss = ((ITakesScreenshot)driver).GetScreenshot();
            ss.SaveAsFile(ScreenShotLocation + "\\" + type + "EmpList.jpeg", ScreenshotImageFormat.Jpeg);
        }
    }
}