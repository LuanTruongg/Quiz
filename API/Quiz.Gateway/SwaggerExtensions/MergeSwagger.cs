using Newtonsoft.Json;

namespace Quiz.Gateway.SwaggerExtensions
{
    public class MergeSwagger
    {
        public void Merge()
        {
            try
            {
                string openApiJsonQuiz = File.ReadAllText(Path.GetDirectoryName(AppDomain.CurrentDomain.BaseDirectory) + "/quizapi.json");
                SwaggerDocument openApiDocumentSrm = JsonConvert.DeserializeObject<SwaggerDocument>(openApiJsonQuiz);
                var mergedOpenApiDocument = new SwaggerDocument
                {
                    Paths = new Dictionary<string, SwaggerPath>()
                };

                foreach (var kvp in openApiDocumentSrm.Paths)
                {
                    var newPath = "/quiz" + kvp.Key;
                    mergedOpenApiDocument.Paths.Add(newPath, kvp.Value);
                }

                var settings = new JsonSerializerSettings
                {
                    NullValueHandling = NullValueHandling.Ignore,
                    Formatting = Formatting.Indented
                };
                string mergedSwaggerJson = JsonConvert.SerializeObject(mergedOpenApiDocument, settings);
                File.WriteAllText(Path.GetDirectoryName(AppDomain.CurrentDomain.BaseDirectory) + "/merged-swagger.json", mergedSwaggerJson);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message.ToString());
            }

        }
    }
    public class SwaggerDocument
    {
        public Dictionary<string, SwaggerPath> Paths { get; set; }
    }
    public class SwaggerPath
    {
        public SwaggerPathOperation Get { get; set; }
        public SwaggerPathOperation Post { get; set; }
        public SwaggerPathOperation Put { get; set; }
        public SwaggerPathOperation Delete { get; set; }
        public SwaggerPathOperation Patch { get; set; }
    }
    public class SwaggerPathOperation
    {
        public List<string> Tags { get; set; }
        public string Summary { get; set; }
        public List<Dictionary<string, List<string>>> Security { get; set; }
    }
}
