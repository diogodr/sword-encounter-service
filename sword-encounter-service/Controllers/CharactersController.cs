using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using sword_encounter_service.Models;
using sword_encounter_service.Services;

namespace sword_encounter_service.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CharactersController : ControllerBase
    {
        private readonly CharacterService _characterService;

        public CharactersController(CharacterService characterService)
        {
            _characterService = characterService;
        }

        [HttpGet]
        public ActionResult<List<Character>> Get() =>
            _characterService.Get();

        [HttpGet("{id:length(24)}", Name = "GetCharacter")]
        public ActionResult<Character> Get(string id)
        {
            var character = _characterService.Get(id);

            if (character == null)
            {
                return NotFound();
            }

            return character;
        }

        [HttpPost]
        public ActionResult<Character> Create(Character character)
        {
            _characterService.Create(character);

            return CreatedAtRoute("GetCharacter", new { id = character.Id.ToString() }, character);
        }

        [HttpPut("{id:length(24)}")]
        public IActionResult Update(string id, Character characterIn)
        {
            var character = _characterService.Get(id);

            if (character == null)
            {
                return NotFound();
            }

            _characterService.Update(id, characterIn);

            return NoContent();
        }

        [HttpPut("{id:length(24)}/add-dice")]
        public IActionResult AddDice(string id, DiceRoll dice)
        {
            var character = _characterService.Get(id);

            if (character == null)
            {
                return NotFound();
            }

            _characterService.AddDice(id, dice);

            return NoContent();
        }

        [HttpPut("{id:length(24)}/add-position")]
        public IActionResult AddPosition(string id, Position position)
        {
            var character = _characterService.Get(id);

            if (character == null)
            {
                return NotFound();
            }

            _characterService.AddPosition(id, position);

            return NoContent();
        }

        [HttpDelete("{id:length(24)}")]
        public IActionResult Delete(string id)
        {
            var character = _characterService.Get(id);

            if (character == null)
            {
                return NotFound();
            }

            _characterService.Remove(character.Id);

            return NoContent();
        }
    }
}
