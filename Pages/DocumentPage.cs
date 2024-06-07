using System;
using ExcelTesting.Pages.Components;
using OpenQA.Selenium.Appium.Windows;

namespace ExcelTesting.Pages
{
    public class DocumentPage : AbstractPage
    {
        public DocumentPage(WindowsDriver<WindowsElement> driver) : base(driver) { }

        private WindowsElement GetWordPadPage()
        {
            return Driver.FindElementByName("Rich Text Window");
        }

        private WindowsElement HomeTab()
        {
            return Driver.FindElementByName("Home");
        }

        private WindowsElement DateAndTimeButton()
        {
            return Driver.FindElementByName("Date and time");
        }

        private WindowsElement OkButton()
        {
            return Driver.FindElementByName("OK");
        }

        public void ClearText()
        {
            GetWordPadPage().Clear();
        }

        public string GetText()
        {
            return GetWordPadPage().Text;
        }

        public void TypeText(string text)
        {
            GetWordPadPage().SendKeys(text);
        }

        public void InsertDateTime()
        {
            HomeTab().Click();
            DateAndTimeButton().Click();
            OkButton().Click();
        }

        public Header GetHeader() {
            throw new NotImplementedException();
            return new Header(Driver.FindElement());
        }
    }
}