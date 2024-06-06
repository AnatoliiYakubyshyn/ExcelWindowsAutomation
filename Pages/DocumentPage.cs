using System;
using OpenQA.Selenium.Appium.Windows;

namespace ExcelTesting.Pages
{
    public class DocumentPage : AbstractPage
    {
        public DocumentPage(WindowsDriver<WindowsElement> driver):base(driver) {}

        private WindowsElement GetWordPadPage() {
            return Driver.FindElementByName("Rich Text Window");
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
    }
}