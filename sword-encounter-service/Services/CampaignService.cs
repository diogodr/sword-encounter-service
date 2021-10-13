using MongoDB.Driver;
using sword_encounter_service.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace sword_encounter_service.Services
{
    public class CampaignService
    {
        private readonly IMongoCollection<Campaign> _campaigns;

        public CampaignService(ISwordEncounterDatabaseSettings settings)
        {
            var client = new MongoClient(settings.ConnectionString);
            var database = client.GetDatabase(settings.DatabaseName);

            _campaigns = database.GetCollection<Campaign>("Campaign");
        }

        public List<Campaign> Get() =>
            _campaigns.Find(campaign => true).ToList();

        public Campaign Get(string id) =>
            _campaigns.Find<Campaign>(campaign => campaign.Id == id).FirstOrDefault();

        public Campaign Create(Campaign campaign)
        {
            _campaigns.InsertOne(campaign);
            return campaign;
        }

        public void Update(string id, Campaign campaignIn) =>
            _campaigns.ReplaceOne(campaign => campaign.Id == id, campaignIn);

        public void Remove(Campaign campaignIn) =>
            _campaigns.DeleteOne(campaign => campaign.Id == campaignIn.Id);

        public void Remove(string id) =>
            _campaigns.DeleteOne(campaign => campaign.Id == id);
    }
}