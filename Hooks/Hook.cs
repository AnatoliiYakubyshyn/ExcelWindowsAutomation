using System;
using System.Threading.Tasks;
using BoDi;
using TechTalk.SpecFlow;
using OpenQA.Selenium.Appium;
using OpenQA.Selenium.Appium.Windows;
using NLog;
using WordPadWindowsAutomation.Utilities;
using TechTalk.SpecFlow;
using OpenQA.Selenium;
using ExcelTesting.Pages;
using WordPadWindowsAutomation.Configuration;
using WordPadWindowsAutomation.Test;
using NLog;
using System.Collections.Generic;

namespace WordPadTesting.Hooks
{
    [Binding]
    public class Hooks
    {
        private WindowsDriver<WindowsElement> driver;
        private readonly IObjectContainer _container;
        private static readonly Logger Logger = LogManager.GetCurrentClassLogger();
        private static string _testRunId;
        private string _testId;
       // Update this path to the location of your auth token file
        private static string authTokenFilePath = "/Users/olhashcherbina/Downloads/workCSh/WordPadWindowsAutomation/Resources/authToken.txt"; 


        public Hooks(IObjectContainer container)
        {
            _container = container;
        }

        [BeforeScenario(Order = 1)]
        public async Task FirstBeforeScenarioAsync(ScenarioContext scenarioContext)
        {
            Logger.Info("Starting scenario: " + scenarioContext.ScenarioInfo.Title);

            // Set the auth token manually 
            string authToken = TokenHelper.GetAuthTokenFromFile(authTokenFilePath);
            ZebrunnerApiHelper.SetAuthToken(authToken);

            string baseUrl = ConfigurationHelper.GetBaseUrl();
            Logger.Info("Base URL: " + baseUrl);

            var appiumOptions = new AppiumOptions();
            appiumOptions.AddAdditionalCapability("app", @"C:\Program Files\Windows NT\Accessories\wordpad.exe");
            Uri url = new Uri(baseUrl);

            driver = new WindowsDriver<WindowsElement>(url, appiumOptions);
            driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(20);
            _container.RegisterInstanceAs<WindowsDriver<WindowsElement>>(driver);

            string projectKey = Environment.GetEnvironmentVariable("REPORTING_PROJECT_KEY");
            string environment = Environment.GetEnvironmentVariable("REPORTING_RUN_ENVIRONMENT");
            string build = Environment.GetEnvironmentVariable("REPORTING_RUN_BUILD");

            _testRunId = await ZebrunnerApiHelper.StartTestRunAsync(projectKey, scenarioContext.ScenarioInfo.Title, environment, build, DateTime.Now);
            _testId = await ZebrunnerApiHelper.StartTestAsync(_testRunId, scenarioContext.ScenarioInfo.Title, "WordPadTesting", "Scenario", "maintainer", DateTime.Now);
        }

        [AfterScenario]
        public async Task TearDownAsync(ScenarioContext scenarioContext)
        {
            Logger.Info("Ending scenario: " + scenarioContext.ScenarioInfo.Title);

            if (scenarioContext.TestError != null)
            {
                Logger.Error("Scenario failed with error: " + scenarioContext.TestError.Message);
                await ScreenshotHelper.CaptureScreenshotAsync(driver, scenarioContext.ScenarioInfo.Title + "_Error", _testRunId, _testId);
            }

            if (driver != null)
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
                    Logger.Warn("No dialog or elements to close were found.");
                }
                catch (Exception ex)
                {
                    Logger.Error($"An error occurred during teardown: {ex.Message}");
                }
                finally
                {
                    driver.Quit();
                }
            }

            bool testPassed = scenarioContext.TestError == null;
            await ZebrunnerApiHelper.FinishTestAsync(_testRunId, _testId, testPassed, scenarioContext.TestError?.Message, DateTime.Now);
            await ZebrunnerApiHelper.FinishTestRunAsync(_testRunId, DateTime.Now);
            
            ///////////////////// Collect and upload logs
            var logs = new List<WordPadWindowsAutomation.Utilities.LogEntry>
            {
                new WordPadWindowsAutomation.Utilities.LogEntry
                {
                    Level = "INFO",
                    Timestamp = DateTimeOffset.UtcNow.ToUnixTimeSeconds(),
                    Message = "Scenario finished"
                }
            };
            await ZebrunnerApiHelper.UploadLogAsync(_testRunId, _testId, logs);
        }
        
    }
}
