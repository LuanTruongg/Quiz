using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Quiz.Infrastructure.Http;
using System.Net;
using System.Text.Json;

namespace Quiz.Infrastructure.Middlewares
{
	public class GlobalExceptionHandlingMiddleware
	{
		private readonly RequestDelegate _next;

		public GlobalExceptionHandlingMiddleware(RequestDelegate next)
		{
			_next = next;
		}

		public async Task Invoke(HttpContext context)
		{
			try
			{
				await _next(context);
			}
			catch (Exception error)
			{
				var response = context.Response;
				response.ContentType = "application/json";

				switch (error)
				{
					case TestException e:
						// custom application error
						response.StatusCode = (int)HttpStatusCode.BadRequest;
						break;
					case KeyNotFoundException e:
						// not found error
						response.StatusCode = (int)HttpStatusCode.NotFound;
						break;
					default:
						// unhandled error
						response.StatusCode = (int)HttpStatusCode.InternalServerError;
						break;
				}

				var result = JsonSerializer.Serialize(new { message = error?.Message });
				await response.WriteAsync(result);
			}
		}
	}
}
