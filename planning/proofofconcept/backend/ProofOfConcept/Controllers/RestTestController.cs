using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;
using System.Net;
using System.Net.Mime;

namespace ProofOfConcept.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class RestTestController : ControllerBase
    {
        private readonly ILogger<RestTestController> _logger;

        public RestTestController(ILogger<RestTestController> logger)
        {
            _logger = logger;
        }

        [HttpGet]
        [ProducesResponseType((int)HttpStatusCode.OK, Type = typeof(TestResponse))]
        [Produces(MediaTypeNames.Application.Json)]
        public IActionResult Get()
        {
            return Ok(new TestResponse() { Message = "Hello World"});
        }

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
                return Ok(new TestResponse() { Message = "Hello World" });
            }
        }

        public class TestMessage
        {
            public required string Message { get; set; }
        }

        public class TestResponse
        {
            public required string Message { get; set; }
        }

        public class ErrorResponse
        {
            public required string Error { get; set; }
        }
    }
}