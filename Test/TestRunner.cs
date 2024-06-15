using System;
using System.Threading.Tasks;
using NUnit.Framework;
using WordPadWindowsAutomation.Utilities;
using NLog;
using ZebrunnerAgent.Attributes;

namespace WordPadWindowsAutomation.Test
{
    [SetUpFixture]
    public class TestRunner
    {
        private static readonly Logger Logger = LogManager.GetCurrentClassLogger();
        private static string _testRunId;

        public static string TestRunId => _testRunId;


        [OneTimeSetUp]
        public async Task OneTimeSetUp() //////////
        {
            try
            {
                _testRunId = await ZebrunnerApiHelper.StartTestRunAsync(
                    Environment.GetEnvironmentVariable("REPORTING_PROJECT_KEY"),
                    "WordPad Automation Test Run",
                    Environment.GetEnvironmentVariable("REPORTING_RUN_ENVIRONMENT"),
                    Environment.GetEnvironmentVariable("REPORTING_RUN_BUILD"),
                    DateTime.Now
                );
                Logger.Info($"Test run started with ID: {_testRunId}");
            }
            catch (Exception ex)
            {
                Logger.Error($"Failed to start test run: {ex.Message}");
            }
            // try
            // {
            //     _testRunId = await ZebrunnerApiHelper.StartTestRunAsync(
            //         Environment.GetEnvironmentVariable("REPORTING_PROJECT_KEY"),
            //         Environment.GetEnvironmentVariable("REPORTING_RUN_ENVIRONMENT"),
            //         Environment.GetEnvironmentVariable("REPORTING_RUN_BUILD")
            //     );
            //     Logger.Info($"Test run started with ID: {_testRunId}");
            // }
            // catch (Exception ex)
            // {
            //     Logger.Error($"Failed to start test run: {ex.Message}");
            // }
        }

        [OneTimeTearDown]
        public async Task OneTimeTearDown()
        {

            try
            {
                await ZebrunnerApiHelper.FinishTestRunAsync(_testRunId, DateTime.Now);
                Logger.Info($"Test run finished with ID: {_testRunId}");
            }
            catch (Exception ex)
            {
                Logger.Error($"Failed to finish test run: {ex.Message}");
                throw; 
            }
            // try
            // {
            //     bool passed = TestContext.CurrentContext.Result.Outcome.Status == NUnit.Framework.Interfaces.TestStatus.Passed;
            //     await ZebrunnerApiHelper.FinishTestRunAsync(_testRunId, passed);
            //     Logger.Info($"Test run finished with ID: {_testRunId}, Passed: {passed}");
            // }
            // catch (Exception ex)
            // {
            //     Logger.Error($"Failed to finish test run: {ex.Message}");
            // }


            // bool passed = TestContext.CurrentContext.Result.Outcome.Status == NUnit.Framework.Interfaces.TestStatus.Passed;
            // await ZebrunnerApiHelper.FinishTestRunAsync(_testRunId, passed);
        }

        // public static string TestRunId => _testRunId;
    }
}