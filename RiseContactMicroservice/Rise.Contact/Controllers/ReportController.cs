using DotNetCore.CAP;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Rise.Contact.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class ReportController : ControllerBase
    {
        private readonly ICapPublisher _capBus;

        public ReportController(ICapPublisher capBus)
        {
            _capBus = capBus;
        }

        [HttpGet("test")]
        public async Task<IActionResult> Publish()
        {
            await _capBus.PublishAsync("test", DateTime.Now);

            return NoContent();
        }
    }
}
