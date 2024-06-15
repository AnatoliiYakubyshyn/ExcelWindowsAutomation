using System;

using BoDi;

using TechTalk.SpecFlow;

using OpenQA.Selenium.Appium;
using OpenQA.Selenium.Appium.Windows;
using System.Text.RegularExpressions;
using OpenQA.Selenium;
using ExcelTesting.Pages;
using WordPadWindowsAutomation.Configuration;

namespace WordPadTesting.Hooks
{
    [Binding]
    public class Hooks
    {

        private WindowsDriver<WindowsElement> driver;

        private readonly IObjectContainer _container;

        public Hooks(IObjectContainer container)
        {
            _container = container;
        }

        [AfterScenario]
        public void TearDown()
        {
            try
            {
                var closeButton = driver.FindElementByName("Close");
                if (closeButton.Displayed)
                {
                    closeButton.Click();
                }

                var dontSaveButton = driver.FindElementByName("Don't Save");
                if (dontSaveButton.Displayed)
                {
                    dontSaveButton.Click();
                }
            }
            catch (NoSuchElementException)
            {
                Console.WriteLine("No dialog or elements to close were found.");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"An error occurred during teardown: {ex.Message}");
            }
            finally
            {
                driver.Quit();
            }
        }

        [BeforeScenario(Order = 1)]
        public void FirstBeforeScenario(ScenarioContext scenarioContext)
        {
            string baseUrl = ConfigurationHelper.GetBaseUrl(); 
            
            var appiumOptions = new AppiumOptions();
            appiumOptions.AddAdditionalCapability("app", @"C:\Program Files\Windows NT\Accessories\wordpad.exe");
            Uri url = new Uri(baseUrl);

            // Initialize the WindowsDriver
            driver = new WindowsDriver<WindowsElement>(url, appiumOptions);
            driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(20);

            // Register the driver instance
            _container.RegisterInstanceAs<WindowsDriver<WindowsElement>>(driver);
        }

    }
}
