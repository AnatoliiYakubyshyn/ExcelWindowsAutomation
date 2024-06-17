using NUnit.Framework;
using TechTalk.SpecFlow;

namespace WordPadWindowsAutomation.Test
{
    [TestFixture]

    public class WordPadFeatureTest
    {
        private ITestRunner _testRunner;

        [SetUp]
        public void SetUp()
        {
            _testRunner = TestRunnerManager.GetTestRunner();
        }

        [Test]
        public void TypeTextAndVerify()
        {
            _testRunner.OnTestRunStart();
            _testRunner.Given("I am on document page with clean document");
            _testRunner.When("I type \"hello world!\"");
            _testRunner.Then("\"hello world!\" is displayed");

            _testRunner.Given("I am on document page with clean document");
            _testRunner.When("I type \"good bye\"");
            _testRunner.Then("\"good bye\" is displayed");

            _testRunner.Given("I am on document page with clean document");
            _testRunner.When("I type \"Hello\"");
            _testRunner.Then("\"Hello\" is displayed");
        }

        [TearDown]
        public void TearDown()
        {
            _testRunner.OnTestRunEnd();
        }
    }
}
