using OpenQA.Selenium.Appium.Windows;

namespace ExcelTesting.Pages
{
    public class AbstractPage
    {
        public WindowsDriver<WindowsElement> Driver {init;get;}

        public AbstractPage(WindowsDriver<WindowsElement> driver) {
            Driver = driver;
        }
    }
}