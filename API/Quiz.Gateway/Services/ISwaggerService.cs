using Quiz.Gateway.SwaggerExtensions;

namespace Quiz.Gateway.Services
{
    public interface ISwaggerService
    {
        SwaggerDocument MergeSwagger();
    }
}
