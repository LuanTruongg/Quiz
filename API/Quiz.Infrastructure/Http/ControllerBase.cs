using System.Collections.Generic;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.DependencyInjection;

namespace Quiz.Infrastructure.Http
{
	public abstract class ControllerBase : Controller
	{
		protected IActionResult ThrowModelErrorsException()
		{
			throw new ErrorException(StatusCodes.Status400BadRequest, ModelState);
		}

		protected IActionResult GetResponse(int statusCode)
		{
			return _GetResponse(statusCode, null);
		}

		protected IActionResult GetResponse(IResponse res)
		{
			if (res == null)
			{
				return NotFound(GenericResponse.NotFound);
			}

			return _GetResponse(res.StatusCode, res);
		}

		protected IActionResult GetResponse(int code, object res)
		{
			return StatusCode(code, res);
		}

		private IActionResult _GetResponse(int statusCode, object res)
		{
			if (statusCode == StatusCodes.Status200OK)
			{
				return Ok(res);
			}
			else if (statusCode == StatusCodes.Status201Created)
			{
				return Created(string.Empty, res);
			}
			else if (statusCode == StatusCodes.Status204NoContent)
			{
				return NoContent();
			}
			else if (statusCode == StatusCodes.Status404NotFound)
			{
				return NotFound(res);
			}
			else if (statusCode == StatusCodes.Status400BadRequest)
			{
				return BadRequest(res);
			}
			else if (statusCode == StatusCodes.Status401Unauthorized)
			{
				return Unauthorized();
			}
			else if (statusCode == StatusCodes.Status409Conflict)
			{
				return Conflict(res);
			}

			return Forbid();
		}

		protected IActionResult GetResponse<T>(List<T> res) where T : IResponse
		{
			if (res == null)
			{
				return NotFound(GenericResponse.NotFound);
			}

			return Ok(res);
		}

		protected IActionResult GetResponse<T>(int code, List<T> res) where T : IResponse
		{
			return StatusCode(code, res);
		}
	}
}
