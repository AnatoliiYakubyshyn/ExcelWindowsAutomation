using NUnit.Framework;
using TechTalk.SpecFlow;
//using TechTalk.SpecFlow.NUnit;
//using ZebrunnerAgent.Attributes;

namespace WordPadWindowsAutomation.Test
{
    [TestFixture]
   // [ZebrunnerClass]
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
        // [Test]
        // [Scenario]
        // public void TypeTextAndVerify()
        // {
        //     // This will execute the SpecFlow scenario defined in the type_text.feature file
        //     var featureFile = Path.Combine(TestContext.CurrentContext.TestDirectory, "Features", "type_text.feature");
        //     var feature = TestRunnerManager.GetTestRunner().FeatureContext.FeatureInfo;
        //     var scenario = TestRunnerManager.GetTestRunner().ScenarioContext.ScenarioInfo;

        //     TestRunnerManager.GetTestRunner().Given("I am on document page with clean document");
        //     TestRunnerManager.GetTestRunner().When("I type \"hello world!\"");
        //     TestRunnerManager.GetTestRunner().Then("\"hello world!\" is displayed");

        //     TestRunnerManager.GetTestRunner().Given("I am on document page with clean document");
        //     TestRunnerManager.GetTestRunner().When("I type \"good bye\"");
        //     TestRunnerManager.GetTestRunner().Then("\"good bye\" is displayed");

        //     TestRunnerManager.GetTestRunner().Given("I am on document page with clean document");
        //     TestRunnerManager.GetTestRunner().When("I type \"Hello\"");
        //     TestRunnerManager.GetTestRunner().Then("\"Hello\" is displayed");
       // }
    }
}
