using OpenQA.Selenium.Appium.Windows;

namespace ExcelTesting.Pages
{
    public class AbstractPage
    {
        private string _url;
        public WindowsDriver<WindowsElement> Driver {init;get;}

        public AbstractPage(WindowsDriver<WindowsElement> driver) {
            Driver = driver;
        }

        public void SetUrl(string url)
        {
            _url = url;
        }

        public string GetUrl()
        {
            return _url;
        }
    }
}