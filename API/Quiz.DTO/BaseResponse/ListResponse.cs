using Microsoft.AspNetCore.Http;
using Quiz.Infrastructure.Http;

namespace Quiz.DTO.BaseResponse
{
	public class ListResponse<T> : IResponse where T : class
	{
        public int Page { get; set; }
        public int PageSize { get; set; }
        public string NextUrl { get; set; }
        public string PrevUrl { get; set; }
        public int StatusCode { get; set; } = StatusCodes.Status200OK;
		public int Count { get; set; }
		public List<T> Results { get; set; }
    }
}
