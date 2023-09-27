using Newtonsoft.Json;

namespace Quiz.Infrastructure.Http
{
	public interface IResponse
	{
		[JsonIgnore]
		int StatusCode { get; set; }
	}
}
