using System;
using System.Linq;
using ExcelTesting.Pages;
using NUnit.Framework;
using OpenQA.Selenium.Appium.Windows;
using TechTalk.SpecFlow;
using ZebrunnerAgent.Attributes;
using System.Threading;

namespace WordPadWindowsAutomation.Steps
{
    [Binding]
    [ZebrunnerClass]
    public class ViewButtonStep
    {
        private static WindowsDriver<WindowsElement> _driver;
        private DocumentPage _documentPage;

        public ViewButtonStep(WindowsDriver<WindowsElement> driver)
        {
            _driver = driver;
            _documentPage = new DocumentPage(driver);
        }

        [Given("I have opened WordPad")]
        public void GivenIHaveOpenedWordPad()
        {
            Assert.NotNull(_driver);
        }

        [When("I click on the View button")]
        public void WhenIClickOnTheViewButton()
        {
            _documentPage.ClickViewTab();
        }

        [When("I check if \"(.*)\" and \"(.*)\" are present")]
        public void WhenICheckIfStatusBarAndRulerArePresent(string statusBar, string ruler)
        {
            _documentPage.ToggleRulerCheckbox();
            _documentPage.ToggleStatusBarCheckbox();
            Assert.IsTrue(_documentPage.IsRulerPresent());
            Assert.IsTrue(_documentPage.IsStatusBarPresent());
        }

        [Then("the \"(.*)\" and \"(.*)\" should be present")]
        public void ThenTheStatusBarAndRulerShouldBePresent(string statusBar, string ruler)
        {
            Assert.IsTrue(_documentPage.IsRulerChecked());
            Assert.IsTrue(_documentPage.IsStatusBarChecked());
        }

        [When("I click on the \"(.*)\" and \"(.*)\" checkboxes")]
        public void WhenIClickOnTheStatusBarAndRulerCheckboxes(string statusBar, string ruler)
        {
            _documentPage.ToggleRulerCheckbox();
            _documentPage.ToggleStatusBarCheckbox();
        }

        [Then("the \"(.*)\" and \"(.*)\" should not be present")]
        public void ThenTheStatusBarAndRulerShouldNotBePresent(string statusBar, string ruler)
        {
            Assert.IsFalse(_documentPage.IsRulerChecked());
            Assert.IsFalse(_documentPage.IsStatusBarChecked());
        }
    }
}