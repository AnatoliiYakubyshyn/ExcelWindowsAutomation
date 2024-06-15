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
          //  ZebrunnerApiHelper.SetAuthToken("eyJraWQiOiJsZWdhY3kiLCJhbGciOiJIUzUxMiJ9.eyJzdWIiOiIxNyIsInBhc3MiOlt7InIiOiJHIiwicGlkIjo1M30seyJyIjoiRyIsInBpZCI6NDB9LHsiciI6IkciLCJwaWQiOjI4fSx7InIiOiJHIiwicGlkIjoxM30seyJyIjoiRyIsInBpZCI6NjR9LHsiciI6IkciLCJwaWQiOjUxfSx7InIiOiJHIiwicGlkIjoxfSx7InIiOiJHIiwicGlkIjoxMn0seyJyIjoiRyIsInBpZCI6NjN9LHsiciI6IkciLCJwaWQiOjYxfSx7InIiOiJHIiwicGlkIjozNX0seyJyIjoiRyIsInBpZCI6NjJ9LHsiciI6IkciLCJwaWQiOjM2fSx7InIiOiJHIiwicGlkIjozNH0seyJyIjoiQSIsInBpZCI6MTF9LHsiciI6IkciLCJwaWQiOjh9LHsiciI6IkciLCJwaWQiOjU5fSx7InIiOiJHIiwicGlkIjo5fSx7InIiOiJHIiwicGlkIjo2MH0seyJyIjoiRyIsInBpZCI6NDd9LHsiciI6IkciLCJwaWQiOjU4fSx7InIiOiJHIiwicGlkIjo0NX0seyJyIjoiRyIsInBpZCI6MzJ9LHsiciI6IkciLCJwaWQiOjcxfSx7InIiOiJHIiwicGlkIjo2OX0seyJyIjoiRyIsInBpZCI6NTZ9LHsiciI6IkciLCJwaWQiOjU3fSx7InIiOiJHIiwicGlkIjozMX0seyJyIjoiRyIsInBpZCI6NDJ9LHsiciI6IkciLCJwaWQiOjI5fSx7InIiOiJHIiwicGlkIjo2N30seyJyIjoiRyIsInBpZCI6MzB9LHsiciI6IkciLCJwaWQiOjY4fSx7InIiOiJHIiwicGlkIjo1NX1dLCJpc3MiOiJpYW0tc2VydmljZSIsInB0cCI6IlVTRVIiLCJhdWQiOiIqIiwidW5tIjoib3NoY2hlcmJpbmEiLCJwcm9qZWN0X2Fzc2lnbm1lbnRzIjpbeyJwcm9qZWN0SWQiOjUzLCJyb2xlIjoiR1VFU1QifSx7InByb2plY3RJZCI6NDAsInJvbGUiOiJHVUVTVCJ9LHsicHJvamVjdElkIjoyOCwicm9sZSI6IkdVRVNUIn0seyJwcm9qZWN0SWQiOjEzLCJyb2xlIjoiR1VFU1QifSx7InByb2plY3RJZCI6NjQsInJvbGUiOiJHVUVTVCJ9LHsicHJvamVjdElkIjo1MSwicm9sZSI6IkdVRVNUIn0seyJwcm9qZWN0SWQiOjEsInJvbGUiOiJHVUVTVCJ9LHsicHJvamVjdElkIjoxMiwicm9sZSI6IkdVRVNUIn0seyJwcm9qZWN0SWQiOjYzLCJyb2xlIjoiR1VFU1QifSx7InByb2plY3RJZCI6NjEsInJvbGUiOiJHVUVTVCJ9LHsicHJvamVjdElkIjozNSwicm9sZSI6IkdVRVNUIn0seyJwcm9qZWN0SWQiOjYyLCJyb2xlIjoiR1VFU1QifSx7InByb2plY3RJZCI6MzYsInJvbGUiOiJHVUVTVCJ9LHsicHJvamVjdElkIjozNCwicm9sZSI6IkdVRVNUIn0seyJwcm9qZWN0SWQiOjExLCJyb2xlIjoiQURNSU5JU1RSQVRPUiJ9LHsicHJvamVjdElkIjo4LCJyb2xlIjoiR1VFU1QifSx7InByb2plY3RJZCI6NTksInJvbGUiOiJHVUVTVCJ9LHsicHJvamVjdElkIjo5LCJyb2xlIjoiR1VFU1QifSx7InByb2plY3RJZCI6NjAsInJvbGUiOiJHVUVTVCJ9LHsicHJvamVjdElkIjo0Nywicm9sZSI6IkdVRVNUIn0seyJwcm9qZWN0SWQiOjU4LCJyb2xlIjoiR1VFU1QifSx7InByb2plY3RJZCI6NDUsInJvbGUiOiJHVUVTVCJ9LHsicHJvamVjdElkIjozMiwicm9sZSI6IkdVRVNUIn0seyJwcm9qZWN0SWQiOjcxLCJyb2xlIjoiR1VFU1QifSx7InByb2plY3RJZCI6NjksInJvbGUiOiJHVUVTVCJ9LHsicHJvamVjdElkIjo1Niwicm9sZSI6IkdVRVNUIn0seyJwcm9qZWN0SWQiOjU3LCJyb2xlIjoiR1VFU1QifSx7InByb2plY3RJZCI6MzEsInJvbGUiOiJHVUVTVCJ9LHsicHJvamVjdElkIjo0Miwicm9sZSI6IkdVRVNUIn0seyJwcm9qZWN0SWQiOjI5LCJyb2xlIjoiR1VFU1QifSx7InByb2plY3RJZCI6NjcsInJvbGUiOiJHVUVTVCJ9LHsicHJvamVjdElkIjozMCwicm9sZSI6IkdVRVNUIn0seyJwcm9qZWN0SWQiOjY4LCJyb2xlIjoiR1VFU1QifSx7InByb2plY3RJZCI6NTUsInJvbGUiOiJHVUVTVCJ9XSwicGVybWlzc2lvbnMiOlsicmVwb3J0aW5nOnN0YWNrdHJhY2UtbGFiZWxzOmFzc2lnbiJdLCJwbXMiOlsicmVwb3J0aW5nOnN0YWNrdHJhY2UtbGFiZWxzOmFzc2lnbiJdLCJ0bnQiOiJzb2x2ZGludGVybmFsIiwiZXhwIjoxNzE4NDA0ODUwLCJpYXQiOjE3MTgzNjE2NTAsInJvIjpmYWxzZSwidGVuYW50Ijoic29sdmRpbnRlcm5hbCIsInVzZXJuYW1lIjoib3NoY2hlcmJpbmEifQ.w1ojnkUEVdyt6MIuqCmZ1HSThtEVohM53D68_JlcVolCCOX8mY3rjk9ojV2yB0cga3-iQj6-5cpoFNa3egcmYw");

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
