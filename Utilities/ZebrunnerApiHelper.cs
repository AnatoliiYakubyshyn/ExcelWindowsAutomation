using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using NUnit.Framework;
using NLog;

namespace WordPadWindowsAutomation.Utilities
{
    public static class ZebrunnerApiHelper
    {
        private static readonly Logger Logger = LogManager.GetCurrentClassLogger();
        private static string AuthToken;

        private static readonly string BaseUrl = Environment.GetEnvironmentVariable("REPORTING_SERVER_HOSTNAME");
        private static readonly string RefreshToken = Environment.GetEnvironmentVariable("REPORTING_SERVER_ACCESS_TOKEN");

        public static void SetAuthToken(string authToken)
        {
            AuthToken = authToken;
        }

        private static async Task<string> GetAuthTokenAsync()
        {
            /////////
            if (!string.IsNullOrEmpty(AuthToken))
            {
                return AuthToken;
            }
            /////
            Logger.Info($"BaseUrl: {BaseUrl}");
            Logger.Info($"RefreshToken: {RefreshToken}");

            using (var client = new HttpClient())
            {
                var content = new StringContent(JsonConvert.SerializeObject(new { refreshToken = RefreshToken }), Encoding.UTF8, "application/json");
                try
                {
                    //////////////////
                    Logger.Info("Sending request to refresh auth token...");
                    Logger.Info($"Request URL: {BaseUrl}/api/iam/v1/auth/refresh");
                    Logger.Info($"Request Body: {JsonConvert.SerializeObject(new { refreshToken = RefreshToken })}");
                    /////////////////////////
                    var response = await client.PostAsync($"{BaseUrl}/api/iam/v1/auth/refresh", content);
                    ////////////////
                    Logger.Info($"Response Status: {response.StatusCode}");

                    var responseData = await response.Content.ReadAsStringAsync();
                    Logger.Info($"Response Data: {responseData}");
                    //////////////////
                    response.EnsureSuccessStatusCode();

                   // var responseData = await response.Content.ReadAsStringAsync();
                    dynamic result = JsonConvert.DeserializeObject(responseData);
                    Logger.Info($"Auth Token: {result.authToken}");
                    AuthToken = result.authToken;
                    return AuthToken;
                   // return result.authToken;
                }
                catch (Exception ex)
                {
                    Logger.Error($"Failed to retrieve auth token: {ex.Message}");
                    throw;
                }

                // var response = await client.PostAsync($"{BaseUrl}/api/iam/v1/auth/refresh", content);
                // response.EnsureSuccessStatusCode();

                // var responseData = await response.Content.ReadAsStringAsync();
                // dynamic result = JsonConvert.DeserializeObject(responseData);
                // return result.authToken;
            }
        }

        public static async Task<string> StartTestRunAsync(string projectKey, string testName, string environment, string build, DateTime startedAt)
        {
            Logger.Info("Starting test run...");

            var authToken = await GetAuthTokenAsync();
            using (var client = new HttpClient())
            {
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", authToken);

                var runInfo = new
                {
                    uuid = Guid.NewGuid().ToString(),
                    name = testName,
                    startedAt = startedAt.ToString("yyyy-MM-ddTHH:mm:ss.fffzzz"),
                    status = "IN_PROGRESS",
                    framework = "nunit",
                    config = new { environment, build }
                };

                var content = new StringContent(JsonConvert.SerializeObject(runInfo), Encoding.UTF8, "application/json");

                try
                {
                    Logger.Info("Sending request to start test run...");
                    Logger.Info($"Request URL: {BaseUrl}/api/reporting/v1/test-runs?projectKey={projectKey}");
                    Logger.Info($"Request Body: {JsonConvert.SerializeObject(runInfo)}");

                    var response = await client.PostAsync($"{BaseUrl}/api/reporting/v1/test-runs?projectKey={projectKey}", content);
                    Logger.Info($"Response Status: {response.StatusCode}");

                    var responseData = await response.Content.ReadAsStringAsync();
                    Logger.Info($"Response Data: {responseData}");

                    response.EnsureSuccessStatusCode();

                    dynamic result = JsonConvert.DeserializeObject(responseData);
                    Logger.Info($"Test run started successfully with ID: {result.id}");
                    return result.id;
                }
                catch (Exception ex)
                {
                    Logger.Error($"Failed to start test run: {ex.Message}");
                    throw;
                }
            }
        }

        // public static async Task<string> StartTestRunAsync(string projectKey, string environment, string build)
        // {
        //     Console.WriteLine("BlaaaaBBBBB");
        //     using (var client = new HttpClient())
        //     {
        //         TestContext.WriteLine($"STATUSSSS : {BaseUrl}");
        //         Console.WriteLine($"STATUSSSS : {BaseUrl}");
        //         client.BaseAddress = new Uri(BaseUrl);
        //         TestContext.WriteLine($"TOKENNN : {RefreshToken}");
        //         Console.WriteLine($"TOKENNN : {RefreshToken}");
        //         client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", RefreshToken);

        //         var runInfo = new
        //         {
        //             projectKey,
        //             environment,
        //             build
        //         };

        //         var content = new StringContent(JsonConvert.SerializeObject(runInfo), Encoding.UTF8, "application/json");

        //         try
        //         {
        //             var response = await client.PostAsync("/api/reporting/test-runs", content);
        //             response.EnsureSuccessStatusCode();

        //             var responseData = await response.Content.ReadAsStringAsync();
        //             dynamic result = JsonConvert.DeserializeObject(responseData);
        //             Logger.Info($"Test run started successfully with ID: {result.id}");
        //             return result.id;
        //         }
        //         catch (Exception ex)
        //         {
        //             Logger.Error($"Failed to start test run: {ex.Message}");
        //             throw;
        //         }        
        //         // var response = await client.PostAsync("/api/reporting/test-runs", content);
        //         // response.EnsureSuccessStatusCode();

        //         // var responseData = await response.Content.ReadAsStringAsync();
        //         // dynamic result = JsonConvert.DeserializeObject(responseData);
        //         // return result.id;
        //     }
        // }

        public static async Task<string> StartTestAsync(string testRunId, string testName, string className, string methodName, string maintainer, DateTime startedAt)
        {
            Logger.Info($"Starting test execution for test run ID: {testRunId}");

            var authToken = await GetAuthTokenAsync();
            using (var client = new HttpClient())
            {
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", authToken);

                var testInfo = new
                {
                    name = testName,
                    className,
                    methodName,
                    argumentsIndex = 0,
                    startedAt = startedAt.ToString("yyyy-MM-ddTHH:mm:ss.fffzzz"),
                    maintainer
                };

                var content = new StringContent(JsonConvert.SerializeObject(testInfo), Encoding.UTF8, "application/json");

                try
                {
                    var response = await client.PostAsync($"{BaseUrl}/api/reporting/v1/test-runs/{testRunId}/tests", content);
                    response.EnsureSuccessStatusCode();

                    var responseData = await response.Content.ReadAsStringAsync();
                    dynamic result = JsonConvert.DeserializeObject(responseData);
                    Logger.Info($"Test execution started successfully with ID: {result.id}");
                    return result.id;
                }
                catch (Exception ex)
                {
                    Logger.Error($"Failed to start test execution: {ex.Message}");
                    throw;
                }
            }
        }

        public static async Task FinishTestAsync(string testRunId, string testId, bool passed, string reason, DateTime endedAt)
        {
            Logger.Info($"Finishing test execution with ID: {testId} for test run ID: {testRunId}");

            var authToken = await GetAuthTokenAsync();
            using (var client = new HttpClient())
            {
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", authToken);

                var testInfo = new
                {
                    result = passed ? "PASSED" : "FAILED",
                    reason,
                    endedAt = endedAt.ToString("yyyy-MM-ddTHH:mm:ss.fffzzz")
                };

                var content = new StringContent(JsonConvert.SerializeObject(testInfo), Encoding.UTF8, "application/json");

                try
                {
                    var response = await client.PutAsync($"{BaseUrl}/api/reporting/v1/test-runs/{testRunId}/tests/{testId}", content);
                    response.EnsureSuccessStatusCode();
                    Logger.Info($"Test execution finished successfully with ID: {testId}");
                }
                catch (Exception ex)
                {
                    Logger.Error($"Failed to finish test execution: {ex.Message}");
                    throw;
                }
            }
        }

        public static async Task FinishTestRunAsync(string testRunId, DateTime endedAt)
        {
            Logger.Info($"Finishing test run with ID: {testRunId}");

            var authToken = await GetAuthTokenAsync();
            using (var client = new HttpClient())
            {
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", authToken);

                var runInfo = new
                {
                    endedAt = endedAt.ToString("yyyy-MM-ddTHH:mm:ss.fffzzz")
                };

                var content = new StringContent(JsonConvert.SerializeObject(runInfo), Encoding.UTF8, "application/json");

                try
                {
                    var response = await client.PutAsync($"{BaseUrl}/api/reporting/v1/test-runs/{testRunId}", content);
                    response.EnsureSuccessStatusCode();
                    Logger.Info($"Test run finished successfully with ID: {testRunId}");
                }
                catch (Exception ex)
                {
                    Logger.Error($"Failed to finish test run: {ex.Message}");
                    throw;
                }
            }
        }

        public static async Task UploadScreenshotAsync(string testRunId, string testId, byte[] screenshotData, long capturedAt)
        {
            Logger.Info($"Uploading screenshot for test run ID: {testRunId}, test ID: {testId}");

            var authToken = await GetAuthTokenAsync();
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(BaseUrl);
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", authToken);

                var content = new MultipartFormDataContent();
                var byteArrayContent = new ByteArrayContent(screenshotData);
                byteArrayContent.Headers.ContentType = MediaTypeHeaderValue.Parse("image/png");
                content.Add(byteArrayContent, "file", $"{testId}.png");
                content.Headers.Add("x-zbr-screenshot-captured-at", capturedAt.ToString());

                try
                {
                    var response = await client.PostAsync($"{BaseUrl}/api/reporting/v1/test-runs/{testRunId}/tests/{testId}/screenshots", content);
                    response.EnsureSuccessStatusCode();
                    Logger.Info($"Screenshot uploaded successfully for test run ID: {testRunId}, test ID: {testId}");
                }
                catch (Exception ex)
                {
                    Logger.Error($"Failed to upload screenshot: {ex.Message}");
                    throw;
                }
            }
        }

        public static async Task UploadLogAsync(string testRunId, string testId, List<LogEntry> logEntries)
        {
            Logger.Info($"Uploading logs for test run ID: {testRunId}, test ID: {testId}");

            var authToken = await GetAuthTokenAsync();
            using (var client = new HttpClient())
            {
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", authToken);

                var logs = logEntries.ConvertAll(log => new
                {
                    testId,
                    level = log.Level,
                    timestamp = log.Timestamp,
                    message = log.Message
                });

                var content = new StringContent(JsonConvert.SerializeObject(logs), Encoding.UTF8, "application/json");

                try
                {
                    var response = await client.PostAsync($"{BaseUrl}/api/reporting/v1/test-runs/{testRunId}/logs", content);
                    response.EnsureSuccessStatusCode();
                    Logger.Info($"Logs uploaded successfully for test run ID: {testRunId}, test ID: {testId}");
                }
                catch (Exception ex)
                {
                    Logger.Error($"Failed to upload logs: {ex.Message}");
                    throw;
                }
            }
        }

        public static async Task UploadTestRunArtifactAsync(string testRunId, string artifactName, byte[] artifactData)
        {
            Logger.Info($"Uploading artifact for test run ID: {testRunId}");

            var authToken = await GetAuthTokenAsync();
            using (var client = new HttpClient())
            {
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", authToken);

                var content = new MultipartFormDataContent();
                var byteArrayContent = new ByteArrayContent(artifactData);
                content.Add(byteArrayContent, "file", artifactName);

                try
                {
                    var response = await client.PostAsync($"{BaseUrl}/api/reporting/v1/test-runs/{testRunId}/artifacts", content);
                    response.EnsureSuccessStatusCode();
                    Logger.Info($"Artifact uploaded successfully for test run ID: {testRunId}");
                }
                catch (Exception ex)
                {
                    Logger.Error($"Failed to upload artifact: {ex.Message}");
                    throw;
                }
            }
        }

        public static async Task UploadTestExecutionArtifactAsync(string testRunId, string testId, string artifactName, byte[] artifactData)
        {
            Logger.Info($"Uploading artifact for test execution ID: {testId}");

            var authToken = await GetAuthTokenAsync();
            using (var client = new HttpClient())
            {
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", authToken);

                var content = new MultipartFormDataContent();
                var byteArrayContent = new ByteArrayContent(artifactData);
                content.Add(byteArrayContent, "file", artifactName);

                try
                {
                    var response = await client.PostAsync($"{BaseUrl}/api/reporting/v1/test-runs/{testRunId}/tests/{testId}/artifacts", content);
                    response.EnsureSuccessStatusCode();
                    Logger.Info($"Artifact uploaded successfully for test execution ID: {testId}");
                }
                catch (Exception ex)
                {
                    Logger.Error($"Failed to upload artifact: {ex.Message}");
                    throw;
                }
            }
        }
    }

    public class LogEntry
    {
        public string Level { get; set; }
        public long Timestamp { get; set; }
        public string Message { get; set; }
    }
}
