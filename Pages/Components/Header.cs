using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using OpenQA.Selenium.Appium.Windows;

namespace ExcelTesting.Pages.Components
{
    public class Header
    {
        private WindowsElement searchContext; 

        public Header(WindowsElement searchContext) {
            this.searchContext = searchContext;
        }

        private WindowsElement GetFileButton() {
            return (WindowsElement)searchContext.FindElementByName("File tab");
        }

        private WindowsElement GetSaveBtn() {
            return (WindowsElement)searchContext.FindElementByName("Save");
        }

        private WindowsElement GetFileElement(string fileName) {
            return (WindowsElement)searchContext.FindElementByName(fileName+".rtf");
        }

        public void Save() {
            GetSaveBtn().Click();
        }

        private WindowsElement GetOpenIcon() {
            throw new NotImplementedException();
        }

        private WindowsElement GetOkBtn() {
            throw new NotImplementedException();
        }

        public void OpenFile(string name) {
            GetFileButton().Click();
            GetOpenIcon().Click();
            GetFileElement(name).Click();
            GetOkBtn().Click();
        } 
    
    }
}