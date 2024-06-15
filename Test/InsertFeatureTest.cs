using NUnit.Framework;
using TechTalk.SpecFlow;
//using TechTalk.SpecFlow.NUnit;
//using ZebrunnerAgent.Attributes;

namespace WordPadWindowsAutomation.Test
{
    [TestFixture]
   // [ZebrunnerClass]
    public class InsertFeatureTest
    {
        private ITestRunner _testRunner;

        [SetUp]
        public void SetUp()
        {
            _testRunner = TestRunnerManager.GetTestRunner();
        }

        [Test]
        public void InsertAndVerifyDateTime()
        {
            _testRunner.OnTestRunStart();
            _testRunner.Given("I am on Document page with clean document");
            _testRunner.When("I insert the current date and time");
            _testRunner.Then("the current date and time should be displayed in the document");
        }

        [TearDown]
        public void TearDown()
        {
            _testRunner.OnTestRunEnd();
        }
        // [Test]
        // [Scenario]
        // public void InsertAndVerifyDateTime()
        // {
        //     // This will execute the SpecFlow scenario defined in the insert.feature file
        //     var featureFile = Path.Combine(TestContext.CurrentContext.TestDirectory, "Features", "insert.feature");
        //     var feature = TestRunnerManager.GetTestRunner().FeatureContext.FeatureInfo;
        //     var scenario = TestRunnerManager.GetTestRunner().ScenarioContext.ScenarioInfo;

        //     TestRunnerManager.GetTestRunner().Given("I am on Document page with clean document");
        //     TestRunnerManager.GetTestRunner().When("I insert the current date and time");
        //     TestRunnerManager.GetTestRunner().Then("the current date and time should be displayed in the document");
        // }
    }
}
