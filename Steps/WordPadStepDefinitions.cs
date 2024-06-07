using ExcelTesting.Pages;
using NUnit.Framework;
using OpenQA.Selenium.Appium.Windows;
using TechTalk.SpecFlow;
using ZebrunnerAgent.Attributes;

namespace WordPadTesting.Steps
{
    [Binding]
    public sealed class WordPadStepDefinitions
    {
        private WindowsDriver<WindowsElement> driver;

        private DocumentPage documentPage;

        public WordPadStepDefinitions(WindowsDriver<WindowsElement> driver)
        {
            this.driver = driver;
        }

        [Given("I am on document page with clean document")]
        public void GivenIamOnDocumentPageWithCleanDocument()
        {
            documentPage = new DocumentPage(driver);
            documentPage.ClearText();
        }

        [When("I type (.*)")]
        public void WhenITypeText(string text)
        {
            documentPage.TypeText(text);
        }

        [Then("(.*) is displayed")]
        public void ThenTextIsDisplayed(string text)
        {
            Assert.AreEqual(text, documentPage.GetText());
        }

    }
}
