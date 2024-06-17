using NUnit.Framework;
using TechTalk.SpecFlow;

namespace WordPadWindowsAutomation.Test
{
    [TestFixture]
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
    }
}
