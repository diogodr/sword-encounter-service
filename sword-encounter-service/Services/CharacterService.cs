using sword_encounter_service.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using MongoDB.Driver;

namespace sword_encounter_service.Services
{
    public class CharacterService
    {
        private readonly IMongoCollection<Character> _characters;

        public CharacterService(ISwordEncounterDatabaseSettings settings)
        {
            var client = new MongoClient(settings.ConnectionString);
            var database = client.GetDatabase(settings.DatabaseName);

            _characters = database.GetCollection<Character>("Character");
        }

        public List<Character> Get() =>
            _characters.Find(character => true).ToList();

        public Character Get(string id) =>
            _characters.Find<Character>(character => character.Id == id).FirstOrDefault();

        public List<Character> GetByCampaign(string idCampanha)
        {
            var filter = Builders<Character>.Filter.Eq(x => x.CampaignId, idCampanha);

            var characters = _characters.Find(filter).ToList();

            return characters;
        }
    

        public Character Create(Character character)
        {
            _characters.InsertOne(character);
            return character;
        }

        public void Update(string id, Character characterIn) =>
            _characters.ReplaceOne(character => character.Id == id, characterIn);

        public void AddDice(string id, DiceRoll dice)
        {
            var newCharacter = _characters.Find<Character>(character => character.Id == id).FirstOrDefault();
            newCharacter.DiceRolls.Add(dice);
            _characters.ReplaceOne(character => character.Id == id, newCharacter);
        }

        public void AddPosition(string id, Position position)
        {
            var newCharacter = _characters.Find<Character>(character => character.Id == id).FirstOrDefault();
            newCharacter.Positions.Add(position);
            _characters.ReplaceOne(character => character.Id == id, newCharacter);
        }


        public void Remove(Character characterIn) =>
            _characters.DeleteOne(character => character.Id == characterIn.Id);

        public void Remove(string id) =>
            _characters.DeleteOne(character => character.Id == id);
    }
}
