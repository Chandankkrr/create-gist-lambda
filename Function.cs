using System;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

using Amazon.Lambda.Core;
using Newtonsoft.Json;

// Assembly attribute to enable the Lambda function's JSON input to be converted into a .NET class.
[assembly: LambdaSerializer(typeof(Amazon.Lambda.Serialization.SystemTextJson.LambdaJsonSerializer))]

namespace create_gist
{
    public class Function
    {
        private const string baseUrl = "https://api.github.com";

        /// <summary>
        /// A simple function that takes a string and does a ToUpper
        /// </summary>
        /// <param name="input"></param>
        /// <param name="context"></param>
        /// <returns></returns>
        public async Task<string> FunctionHandler(MessageInput input, ILambdaContext context)
        {
            var createGistRequest = new CreateGistRequest
            {
                Description = input.Description ?? $"{DateTime.Now.ToUniversalTime()} - Hello 👋 from c# dotnet core!",
                Public = false,
                Files = new Files
                {
                    HelloWorldJs = new File
                    {
                        Content = "Console.log(\"Hi there, how are you? 🙈\")"
                    }
                }
            };

            var content = JsonConvert.SerializeObject(createGistRequest);
            var requestUri = new Uri(string.Format($"{baseUrl}/gists"));

            using var client = new HttpClient
            {
                BaseAddress = new Uri(baseUrl)
            };

            string token = Environment.GetEnvironmentVariable("GIST_TOKEN");

            client.DefaultRequestHeaders.UserAgent.Add(new ProductInfoHeaderValue("AppName", "1.0"));
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Token", token);

            try
            {
                var response = await client.PostAsync(requestUri, new StringContent(content, Encoding.Default, "application/json"));

                if (!response.IsSuccessStatusCode)
                {
                    throw new Exception($"Create new gist failed with status code: {response.StatusCode}");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Create new gist failed with error message {ex.Message}");
            }

            return $"Successfully created new gist {input.Name} 🚀";
        }
    }
}
