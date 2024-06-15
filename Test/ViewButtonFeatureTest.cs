using NUnit.Framework;
using TechTalk.SpecFlow;
//using TechTalk.SpecFlow.NUnit;
//using ZebrunnerAgent.Attributes;

namespace WordPadWindowsAutomation.Test
{
    [TestFixture]
   // [ZebrunnerClass]
    public class ViewButtonFeatureTest
    {
        private ITestRunner _testRunner;

        [SetUp]
        public void SetUp()
        {
            _testRunner = TestRunnerManager.GetTestRunner();
        }

        [Test]
        public void CheckAndUncheckStatusBarAndRuler()
        {
            _testRunner.OnTestRunStart();
            _testRunner.Given("I have opened WordPad");
            _testRunner.When("I click on the View button");
            _testRunner.When("I check if \"Status Bar\" and \"Ruler\" are present");
            _testRunner.Then("the \"Status Bar\" and \"Ruler\" should be present");
            _testRunner.When("I click on the \"Status Bar\" and \"Ruler\" checkboxes");
            _testRunner.Then("the \"Status Bar\" and \"Ruler\" should not be present");
        }

        [TearDown]
        public void TearDown()
        {
            _testRunner.OnTestRunEnd();
        }
        // [Test]
        // [Scenario]
        // public void CheckAndUncheckStatusBarAndRuler()
        // {
        //     // This will execute the SpecFlow scenario defined in the view_button.feature file
        //     var featureFile = Path.Combine(TestContext.CurrentContext.TestDirectory, "Features", "view_button.feature");
        //     var feature = TestRunnerManager.GetTestRunner().FeatureContext.FeatureInfo;
        //     var scenario = TestRunnerManager.GetTestRunner().ScenarioContext.ScenarioInfo;

        //     TestRunnerManager.GetTestRunner().Given("I have opened WordPad");
        //     TestRunnerManager.GetTestRunner().When("I click on the View button");
        //     TestRunnerManager.GetTestRunner().When("I check if \"Status Bar\" and \"Ruler\" are present");
        //     TestRunnerManager.GetTestRunner().Then("the \"Status Bar\" and \"Ruler\" should be present");
        //     TestRunnerManager.GetTestRunner().When("I click on the \"Status Bar\" and \"Ruler\" checkboxes");
        //     TestRunnerManager.GetTestRunner().Then("the \"Status Bar\" and \"Ruler\" should not be present");
        // }
    }
}
