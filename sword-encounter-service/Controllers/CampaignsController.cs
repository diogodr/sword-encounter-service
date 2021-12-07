using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using sword_encounter_service.Models;
using sword_encounter_service.Services;

namespace sword_encounter_service.Controllers
{

    [Route("api/[controller]")]
    [ApiController]
    public class CampaignsController : ControllerBase
    {
        private readonly CampaignService _campaignService;

        public CampaignsController(CampaignService campaignService)
        {
            _campaignService = campaignService;
        }

        [HttpGet]
        public ActionResult<List<Campaign>> Get() =>
            _campaignService.Get();

        [HttpGet("user/{userId}")]
        public ActionResult<List<Campaign>> ListByUser(string userId) =>
            _campaignService.GetByUser(userId);

        [HttpGet("{id:length(24)}", Name = "GetCampaign")]
        public ActionResult<Campaign> Get(string id)
        {
            var campaign = _campaignService.Get(id);

            if (campaign == null)
            {
                return NotFound();
            }

            return campaign;
        }

        [HttpPost]
        public ActionResult<Campaign> Create(Campaign campaign)
        {
            _campaignService.Create(campaign);

            return CreatedAtRoute("GetCampaign", new { id = campaign.Id.ToString() }, campaign);
        }

        [HttpPut("{id:length(24)}")]
        public IActionResult Update(string id, Campaign campaignIn)
        {
            var campaign = _campaignService.Get(id);

            if (campaign == null)
            {
                return NotFound();
            }

            _campaignService.Update(id, campaignIn);

            return NoContent();
        }

        [HttpDelete("{id:length(24)}")]
        public IActionResult Delete(string id)
        {
            var campaign = _campaignService.Get(id);

            if (campaign == null)
            {
                return NotFound();
            }

            _campaignService.Remove(campaign.Id);

            return NoContent();
        }
    }
}
