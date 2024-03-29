﻿using DotNetCore.CAP;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Rise.Report.DataAccess.Abstract;
using Rise.Shared;
using Rise.Shared.Dtos;
using System.Net;
using AutoMapper;
using Rise.Report.Rest;

namespace Rise.Report.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class ReportController : ControllerBase
    {
        private readonly ICapPublisher _capBus;
        private readonly IReportService _reportService;
        private readonly IMapper _mapper;
        private readonly IRefitPersonService _refitPersonService;

        public ReportController(ICapPublisher capBus, IReportService reportService, IMapper mapper, IRefitPersonService refitPersonService)
        {
            _capBus = capBus;
            _reportService = reportService;
            _mapper = mapper;
            _refitPersonService = refitPersonService;
        }

        [ApiExplorerSettings(IgnoreApi = true)]
        [CapSubscribe(RiseContactConst.ReportRequestPublisherName)]
        public async Task ConsumerReport(CreateReportRequestDto reportRequest)
        {
            await _reportService.CreateReport(reportRequest.ReportRequestId);
        }

        [ApiExplorerSettings(IgnoreApi = true)]
        [CapSubscribe(RiseContactConst.ReportRequestPublisherWithData)]
        public async Task ConsumerReportWithData(CreateReportRequestDto reportRequest)
        {
            await _reportService.CreateReportWithData(reportRequest.ReportRequestId, reportRequest.Persons);
        }
        [HttpPost("GetReportStatus")]
        [ProducesResponseType(typeof(CustomReportResultDto), (int)HttpStatusCode.BadRequest)]
        [ProducesResponseType(typeof(CustomReportResultDto), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> GetReportStatus(string reportId)
        {
            var report = await _reportService.GetReportStatus(reportId);
            if (report == null)
            {
                return BadRequest();
            }
            var otherResult = _mapper.Map<CustomReportResultDto>(report);
            return Ok(otherResult);
        }

        [HttpPost("GetReportList")]
        [ProducesResponseType(typeof(List<ReportListDto>), (int)HttpStatusCode.BadRequest)]
        [ProducesResponseType(typeof(List<ReportListDto>), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> GetReportList()
        {
            var reports = await _reportService.GetReportList();
            if (reports == null)
            {
                return BadRequest();
            }
            return Ok(reports);
        }
        [HttpPost("GetReportDetail")]
        [ProducesResponseType(typeof(List<ReportDetailDto>), (int)HttpStatusCode.BadRequest)]
        [ProducesResponseType(typeof(List<ReportDetailDto>), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> GetReportDetail(string reportId)
        {
            var reports = await _reportService.GetReportDetails(reportId);
            if (reports == null)
            {
                return BadRequest();
            }
            return Ok(reports);
        }
        

        [HttpGet("CreateReportWithRest")]
        public async Task<IActionResult> CreateReportWithRest()
        {
            var reportId = Guid.NewGuid().ToString();
            var persons = await _refitPersonService.GetAllPersonsWithDetail();

            await _capBus.PublishAsync(RiseContactConst.ReportRequestPublisherWithData, new CreateReportRequestDto()
            {
                ReportRequestId = reportId,
                Persons = persons
            });
            return Ok(reportId);

        }

    }
}
