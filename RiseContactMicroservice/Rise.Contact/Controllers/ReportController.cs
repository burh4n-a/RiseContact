using DotNetCore.CAP;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Rise.Shared.Dtos;
using System.Net;
using Rise.Contact.DataAccess.Abstract;
using Rise.Shared;

namespace Rise.Contact.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class ReportController : ControllerBase
    {
        private readonly ICapPublisher _capBus;
        private readonly IPersonContactService _personContactService;

        public ReportController(ICapPublisher capBus, IPersonContactService personContactService)
        {
            _capBus = capBus;
            _personContactService = personContactService;
        }

        [HttpPost("CreateReportRequest")]
        [ProducesResponseType(typeof(string), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> CreateReportRequest()
        {
            var reportId = Guid.NewGuid().ToString();
            await _capBus.PublishAsync(RiseContactConst.ReportRequestPublisherName, new CreateReportRequestDto
            {
                ReportRequestId = reportId
            });
            return Ok(reportId);
        }


        [HttpPost("CreateReportRequestWithData")]
        [ProducesResponseType(typeof(string), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> CreateReportRequestWithData()
        {
            var reportId = Guid.NewGuid().ToString();
            var persons = await _personContactService.GetAllWithDetailPersons();
            await _capBus.PublishAsync(RiseContactConst.ReportRequestPublisherWithData, new CreateReportRequestDto()
            {
                ReportRequestId = reportId,
                Persons = persons
            });
            return Ok(reportId);
        }


    }

}
