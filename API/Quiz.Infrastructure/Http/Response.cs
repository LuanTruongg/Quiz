using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;

namespace Quiz.Infrastructure.Http
{
	public class Response
	{
		[JsonIgnore]
		public int __Id { get; set; }
		public virtual int StatusCode { get; set; } = StatusCodes.Status200OK;
	}
}
