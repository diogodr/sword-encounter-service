using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using sword_encounter_service.Models;
using sword_encounter_service.Services;

namespace sword_encounter_service.Controllers
{

    [Route("api/[controller]")]
    [ApiController]
    public class GameController : ControllerBase
    {
        private readonly UserService _userService;
        private readonly CharacterService _characterService;
        private readonly CampaignService _campaignService;

        public GameController(UserService userService, CampaignService campaignService, CharacterService characterService)
        {
            _userService = userService;
            _characterService = characterService;
            _campaignService = campaignService;            
        }

        /// <summary>
        /// Traz informações para o game
        /// </summary>
        [HttpGet("{id:length(24)}", Name = "GetGame")]
        public ActionResult<GameResponse> Get(string id)
        {
            var campaign = _campaignService.Get(id);

            if (campaign == null)
            {
                return NotFound();
            }

            var characters = _characterService.GetByCampaign(id);

            GameResponse gameResponse = new GameResponse();

            gameResponse.Campaign = campaign;

            gameResponse.Characters = characters;

            return gameResponse;
        }
        
    }
}
