using Quiz.Gateway.SwaggerExtensions;
using System.Text.Json;

namespace Quiz.Gateway.Services
{
    public class SwaggerService : ISwaggerService
    {
        public SwaggerDocument MergeSwagger()
        {
            string filePathSwagger = Path.GetDirectoryName(AppDomain.CurrentDomain.BaseDirectory) + "/merged-swagger.json";
            Console.WriteLine(">>> Merge path: " + filePathSwagger);
            string jsonData = File.ReadAllText(filePathSwagger);
            SwaggerDocument ListOpperate = JsonSerializer.Deserialize<SwaggerDocument>(jsonData);
            return ListOpperate;
        }
    }
}
