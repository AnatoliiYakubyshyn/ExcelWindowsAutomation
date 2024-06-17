using NUnit.Framework;
using TechTalk.SpecFlow;

namespace WordPadWindowsAutomation.Test
{
    [TestFixture]
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
    }
}
