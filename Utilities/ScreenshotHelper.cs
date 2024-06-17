using System;
using System.IO;
using System.Threading.Tasks;
using OpenQA.Selenium.Appium.Windows;

using NLog;


namespace WordPadWindowsAutomation.Utilities
{
    public static class ScreenshotHelper
    {
        private static readonly Logger Logger = LogManager.GetCurrentClassLogger();

        public static async Task CaptureScreenshotAsync(WindowsDriver<WindowsElement> driver, string screenshotName, string testRunId, string testId)
        {
            try
            {
                // Define the directory to store the screenshots
                string screenshotDirectory = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Screenshots");
                Directory.CreateDirectory(screenshotDirectory);

                // Define the file path for the screenshot
                string screenshotPath = Path.Combine(screenshotDirectory, $"{screenshotName}.png");

                // Capture and save the screenshot
                driver.GetScreenshot().SaveAsFile(screenshotPath, OpenQA.Selenium.ScreenshotImageFormat.Png);

                // Upload the screenshot to Zebrunner
                var screenshotData = File.ReadAllBytes(screenshotPath);
                long capturedAt = DateTimeOffset.UtcNow.ToUnixTimeMilliseconds();
                await ZebrunnerApiHelper.UploadScreenshotAsync(testRunId, testId, screenshotData, capturedAt);
            }

            catch (Exception ex)
            {
                Logger.Error($"Failed to capture or upload screenshot: {ex.Message}");
            }
        }
    }
}