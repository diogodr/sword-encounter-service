using MongoDB.Driver;
using sword_encounter_service.Models;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace sword_encounter_service.Services
{
    public class CampaignService
    {
        private readonly IMongoCollection<Campaign> _campaigns;
        private readonly IMongoCollection<Character> _character;

        public CampaignService(ISwordEncounterDatabaseSettings settings)
        {
            var client = new MongoClient(settings.ConnectionString);
            var database = client.GetDatabase(settings.DatabaseName);

            _campaigns = database.GetCollection<Campaign>("Campaign");
            _character = database.GetCollection<Character>("Character");
        }

        public List<Campaign> Get() =>
            _campaigns.Find(campaign => true).ToList();

        public List<Campaign> GetByUser(string userId)
        {            
            var characters = _character
                .Find(character => character.PlayerId == userId).ToList();

            var filter = Builders<Campaign>.Filter.In(x => x.Id, characters.Select(c => c.CampaignId).ToList());
            var filter2 = Builders<Campaign>.Filter.Eq(x => x.MasterId, userId);

            var campaigns = _campaigns.Find(filter | filter2).ToList();

            return campaigns;
        }
         

        public Campaign Get(string id) =>
            _campaigns.Find<Campaign>(campaign => campaign.Id == id).FirstOrDefault();

        public Campaign Create(Campaign campaign)
        {
            _campaigns.InsertOne(campaign);
            return campaign;
        }

        public void Update(string id, Campaign campaignIn) =>
            _campaigns.ReplaceOne(campaign => campaign.Id == id, campaignIn);

        public void AddMap(string id, string map)
        {
            var newCampaign = _campaigns.Find<Campaign>(campaign => campaign.Id == id).FirstOrDefault();
            newCampaign.Maps.Add(map);
            _campaigns.ReplaceOne(character => character.Id == id, newCampaign);
        }

        public void Remove(Campaign campaignIn) =>
            _campaigns.DeleteOne(campaign => campaign.Id == campaignIn.Id);

        public void Remove(string id) =>
            _campaigns.DeleteOne(campaign => campaign.Id == id);
    }
}