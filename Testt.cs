using NUnit.Framework;
using TechTalk.SpecFlow;
using System.Diagnostics;
using ZebrunnerAgent.Attributes;

[assembly: ZebrunnerAssembly]

namespace WordPadTesting
{
    [TestFixture, ZebrunnerClass, ZebrunnerTest]
    public class Testt
    {
        [TestCase]
        public void TestInsertFeature() 
        {
            ExecuteSpecFlowTest("insert.feature");
        }

        [TestCase]
        public void TestTypeTextFeature() 
        {
            ExecuteSpecFlowTest("type_text.feature");
        }

        [TestCase]
        public void TestViewButtonFeature() 
        {
            ExecuteSpecFlowTest("view_button.feature");
        }

        private void ExecuteSpecFlowTest(string featureFile)
        {
            // Побудова команди для запуску dotnet test з конкретним файлом функціональності
            string command = "dotnet";
            string arguments = $"test --filter FullyQualifiedName~\"{featureFile.Replace(".feature", "")}\"";

            // Створення об'єкта ProcessStartInfo для налаштування процесу
            ProcessStartInfo startInfo = new ProcessStartInfo
            {
                FileName = command,
                Arguments = arguments,
                UseShellExecute = false,
                RedirectStandardOutput = true,
                RedirectStandardError = true,
                CreateNoWindow = true
            };

            // Створення і запуск процесу
            Process process = new Process
            {
                StartInfo = startInfo
            };

            process.Start();

            // Зчитування виводу
            string output = process.StandardOutput.ReadToEnd();
            string error = process.StandardError.ReadToEnd();

            // Очікування завершення процесу
            process.WaitForExit();

            // Виведення результатів
            TestContext.WriteLine("Output:");
            TestContext.WriteLine(output);

            TestContext.WriteLine("Error:");
            TestContext.WriteLine(error);
        }
    }
}