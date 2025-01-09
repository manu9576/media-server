using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Swashbuckle.AspNetCore.Annotations;

namespace api.Controllers
{
	/// <summary>
	/// Media controller.
	/// </summary>
	[Route("[controller]")]
	public class MediaController : Controller
	{
		private readonly ILogger<MediaController> _logger;
		private readonly IConfiguration _configuration;

		/// <summary>
		/// Constructor.
		/// </summary>
		/// <param name="logger"></param>
		/// <param name="configuration"></param>
		public MediaController(ILogger<MediaController> logger, IConfiguration configuration)
		{
			_logger = logger;
			_configuration = configuration;
		}

		/// <summary>
		/// Gets the list of movies path.
		/// </summary>
		/// <returns>List of path</returns>
		[HttpGet]
		[ProducesResponseType(typeof(string[]) , (int)HttpStatusCode.OK)]
		public ActionResult<string[]> GetMovies()
		{
			var rootPath = _configuration.GetValue<string>("MediaPath:RootPath");
			var moviesPath = _configuration.GetValue<string>("MediaPath:Films");

			string path = $"{rootPath}/{moviesPath}";

			string[] files = Directory.GetFiles(path, "*.*", SearchOption.AllDirectories);

			return Ok(files);		
		}
	}
}