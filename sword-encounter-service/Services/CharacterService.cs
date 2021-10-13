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

        public Character Create(Character character)
        {
            _characters.InsertOne(character);
            return character;
        }

        public void Update(string id, Character characterIn) =>
            _characters.ReplaceOne(character => character.Id == id, characterIn);

        public void Remove(Character characterIn) =>
            _characters.DeleteOne(character => character.Id == characterIn.Id);

        public void Remove(string id) =>
            _characters.DeleteOne(character => character.Id == id);
    }
}
