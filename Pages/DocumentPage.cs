using System;
using OpenQA.Selenium.Appium.Windows;
using WordPadWindowsAutomation.Configuration;

namespace ExcelTesting.Pages
{
    public class DocumentPage : AbstractPage
    {
        public DocumentPage(WindowsDriver<WindowsElement> driver):base(driver) {
            string baseUrl = ConfigurationHelper.GetBaseUrl();
            SetUrl(baseUrl);
        }
        private WindowsElement ViewTab() => Driver.FindElementByName("View");

        private WindowsElement StatusBarCheckbox() => Driver.FindElementByName("Status bar");

        private WindowsElement RulerCheckbox() => Driver.FindElementByName("Ruler");

        private WindowsElement GetWordPadPage() {
            return Driver.FindElementByName("Rich Text Window");
        }
        
        private WindowsElement HomeTab() {
            return Driver.FindElementByName("Home");
        }

        private WindowsElement DateAndTimeButton() {
            return Driver.FindElementByName("Date and time");
        }

        private WindowsElement OkButton() {
            return Driver.FindElementByName("OK");
        }

        public void ClearText() {
            GetWordPadPage().Clear();
        }

        public string GetText() {
            return GetWordPadPage().Text;
        }

        public void TypeText(string text) {
            GetWordPadPage().SendKeys(text);
        }

         public void InsertDateTime()
        {
            HomeTab().Click();
            DateAndTimeButton().Click();
            OkButton().Click();
        }

        public void ClickViewTab() => ViewTab().Click();

        public void ToggleStatusBarCheckbox() => StatusBarCheckbox().Click();

        public void ToggleRulerCheckbox() => RulerCheckbox().Click();

        public bool IsRulerChecked() => RulerCheckbox().Selected;

        public bool IsStatusBarChecked() => StatusBarCheckbox().Selected;
        
        public bool IsStatusBarPresent() => StatusBarCheckbox().Displayed;

        public bool IsRulerPresent() => RulerCheckbox().Displayed;

    }
}