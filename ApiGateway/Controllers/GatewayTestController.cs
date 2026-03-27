using Microsoft.AspNetCore.Mvc;

namespace ApiGateway.Controllers
{
    [ApiController]
    [Route("api/gateway-test")]
    public class GatewayTestController : ControllerBase
    {
        [HttpGet]
        public IActionResult Get()
        {
            return Ok("API Gateway is running");
        }
    }
}