using System;
using System.IO;

namespace WordPadWindowsAutomation.Utilities
{
    public static class TokenHelper
    {
        public static string GetAuthTokenFromFile(string filePath)
        {
            if (File.Exists(filePath))
            {
                return File.ReadAllText(filePath).Trim();
            }
            else
            {
                throw new FileNotFoundException("The auth token file was not found.", filePath);
            }
        }
    }
}
