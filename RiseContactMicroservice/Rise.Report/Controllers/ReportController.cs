﻿using DotNetCore.CAP;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Rise.Report.Controllers
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

        [HttpGet]
        [CapSubscribe("test")]
        public void Consumer(DateTime date)
        {
            Console.WriteLine(date);
        }



    }
}
