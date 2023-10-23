using Microsoft.AspNetCore.Mvc;
using ProofOfConcept.Entities;
using System.ComponentModel.DataAnnotations;
using System.Net;
using System.Net.Mime;

namespace ProofOfConcept.Controllers
{
    /// <summary>
    /// Test Rest Controller.
    /// </summary>
    [ApiController]
    [Route("rest/[controller]")]
    public class RestTestController : ControllerBase
    {
        private readonly ILogger<RestTestController> _logger;

        /// <summary>
        /// Create a new instanct of RestTestController.
        /// </summary>
        /// <param name="logger">Logger.</param>
        public RestTestController(ILogger<RestTestController> logger)
        {
            _logger = logger;
        }

        /// <summary>
        /// Test Get method.
        /// </summary>
        /// <returns>A Http Action Result</returns>
        [HttpGet]
        [ProducesResponseType((int)HttpStatusCode.OK, Type = typeof(TestResponse))]
        [Produces(MediaTypeNames.Application.Json)]
        public IActionResult Get()
        {
            return Ok(new TestResponse() { Message = "Hello World"});
        }

        /// <summary>
        /// Test Post request.
        /// </summary>
        /// <param name="thing">The thing.</param>
        /// <returns>A Http Action Result</returns>
        [HttpPost]
        [ProducesResponseType((int)HttpStatusCode.OK, Type = typeof(TestResponse))]
        [ProducesResponseType((int)HttpStatusCode.Forbidden, Type = typeof(ErrorResponse))]
        [ProducesResponseType((int)HttpStatusCode.BadRequest, Type = typeof(ValidationProblemDetails))]
        [Produces(MediaTypeNames.Application.Json)]
        public IActionResult Post([FromBody, Required] TestMessage thing)
        {
            if(thing?.Message.ToLower() == "nerd")
            {
                return Unauthorized(new ErrorResponse() { Error = "Nuh uh"});
            }
            else
            {
                return Ok(new TestResponse() { Message = "Hello " + thing?.Message });
            }
        }
    }
}