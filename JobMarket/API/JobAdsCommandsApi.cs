using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using static JobMarket.Contracts.JobAds;
using Microsoft.AspNetCore.Mvc;

namespace JobMarket.API
{
    [Route("/ad")]
    public class JobAdsCommandsApi : Controller
    {
        private readonly JobAdsApplicationService
            _applicationService;

        public JobAdsCommandsApi(
            JobAdsApplicationService applicationService
        )
            => _applicationService = applicationService;

        [HttpPost]
        public async Task<IActionResult> Post(V1.Create request)
        {
            await _applicationService.Handle(request);
            return Ok();
        }

        [Route("name")]
        [HttpPut]
        public async Task<IActionResult> Put(V1.SetTitle request)
        {
            await _applicationService.Handle(request);
            return Ok();
        }

        [Route("text")]
        [HttpPut]
        public async Task<IActionResult> Put(V1.UpdateText request)
        {
            await _applicationService.Handle(request);
            return Ok();
        }

        [Route("salary")]
        [HttpPut]
        public async Task<IActionResult> Put(V1.UpdateSalary request)
        {
            await _applicationService.Handle(request);
            return Ok();
        }

        [Route("publish")]
        [HttpPut]
        public async Task<IActionResult> Put(V1.RequestToPublish request)
        {
            await _applicationService.Handle(request);
            return Ok();
        }
    }

}
