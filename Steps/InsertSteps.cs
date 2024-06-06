using System;
using System.Linq;
using ExcelTesting.Pages;
using NUnit.Framework;
using OpenQA.Selenium.Appium.Windows;
using TechTalk.SpecFlow;
using System.Text.RegularExpressions;

namespace WordPadTesting.Steps
{
    [Binding]
    public sealed class InsertSteps
    {
        private readonly WindowsDriver<WindowsElement> driver;
        private DocumentPage documentPage;

        public InsertSteps(WindowsDriver<WindowsElement> driver)
        {
            this.driver = driver;
            documentPage = new DocumentPage(driver);
        }

        [Given("I am on Document page with clean document")]
        public void GivenIAmOnDocumentPageWithCleanDocument()
        {
            documentPage.ClearText();
        }

       [When("I insert the current date and time")]
        public void WhenIInsertTheCurrentDateAndTime()
        {
            documentPage.InsertDateTime();
        }

        [Then("the current date and time should be displayed in the document")]
        public void ThenTheCurrentDateAndTimeShouldBeDisplayedInTheDocument()
        {
            string currentDate = DateTime.Now.ToShortDateString();
            string documentText = documentPage.GetText();

            string normalizedCurrentDate = RemoveInvisibleCharacters(currentDate);
            string normalizedDocumentText = RemoveInvisibleCharacters(documentText);

            Assert.IsTrue(normalizedDocumentText.Contains(normalizedCurrentDate), $"Expected '{normalizedCurrentDate}' to be part of '{normalizedDocumentText}'");
        }
        

         private string RemoveInvisibleCharacters(string input)
        {
            return Regex.Replace(input, @"\p{C}+", string.Empty);
        }
    }
}