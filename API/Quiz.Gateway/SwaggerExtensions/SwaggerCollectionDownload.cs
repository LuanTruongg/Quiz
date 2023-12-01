namespace Quiz.Gateway.SwaggerExtensions
{
    public class SwaggerCollectionDownload
    {
        public bool IsDownload { get; set; }
        public async Task Download(string env)
        {
            var httpClient = new HttpClient();
            string filePathSwagger = $"swagger.{env}.json";
            string jsonData = File.ReadAllText(filePathSwagger);

            SwaggerEndpointsCollection SwaggerList = System.Text.Json.JsonSerializer.Deserialize<SwaggerEndpointsCollection>(jsonData);
            foreach (var swagger in SwaggerList.SwaggerEndpoints)
            {
                foreach (var config in swagger.Config)
                {
                    string jsonFileUrl = config.Url;
                    try
                    {
                        HttpResponseMessage response = await httpClient.GetAsync(jsonFileUrl);
                        if (response.IsSuccessStatusCode)
                        {
                            string jsonContent = await response.Content.ReadAsStringAsync();
                            string AppPath = Path.GetDirectoryName(AppDomain.CurrentDomain.BaseDirectory);
                            string filePathSave = $"{AppPath}/{swagger.Key}.json";
                            await File.WriteAllTextAsync(filePathSave, jsonContent);
                            Console.WriteLine($">>> JSON file downloaded successfully and saved to {jsonFileUrl}.");
                        }
                        else
                            Console.WriteLine($">>> Failed to download the JSON file. Status code: {response.StatusCode}");
                    }
                    catch (HttpRequestException e)
                    {
                        Console.WriteLine($">>> Error occurred: {e.Message}");
                    }
                }
            }
            IsDownload = true;
        }
    }
}
