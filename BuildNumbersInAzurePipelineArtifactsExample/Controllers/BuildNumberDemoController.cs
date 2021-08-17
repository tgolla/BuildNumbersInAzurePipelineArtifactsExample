using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;

namespace BuildNumbersInAzurePipelineArtifactsExample.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class BuildNumberDemoController : Controller
    {
        private readonly ILogger<BuildNumberDemoController> logger;

        /// <summary>
        /// Build Number Demo constructor.
        /// </summary>
        /// <param name="logger">A generic interface for logging where the category name is derived from the specified TCategoryName type name used to enable activation of a named ILogger from dependency injection.</param>
        /// <param name="configuration">Represents a set of key/value application configuration properties.</param>
        public BuildNumberDemoController(ILogger<BuildNumberDemoController> logger, IConfiguration configuration)
        {
            this.logger = logger;
        }

        /// <summary>
        /// Returns API information.
        /// </summary>
        /// <returns>API information.</returns>
        /// <response code="200">Returns API information.</response>
        /// <remarks>
        /// This API call should be included as the default in all APIs.
        /// </remarks>
        [HttpGet]
        public IActionResult ApiInformation()
        {
            return Ok(AssemblyInformation.InformationObject());
        }
    }
}
