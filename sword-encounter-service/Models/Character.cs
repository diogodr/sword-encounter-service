using System;
using MongoDB.Bson;
using System.Collections.Generic;
using MongoDB.Bson.Serialization.Attributes;
using System.ComponentModel.DataAnnotations;

namespace sword_encounter_service.Models
{
    public class Character
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }
        [Required]
        public string CampaignId { get; set; }
        public string PlayerId { get; set; }
        public List<CharacterAttribute> Attributes { get; set; }
        public List<DiceRoll> DiceRolls { get; set; } = new List<DiceRoll>();
        public List<Position> Positions { get; set; } = new List<Position>();
        public DateTime CreatedAt { get; set; } = DateTime.Now;
        public DateTime UpdatedAt { get; set; } = DateTime.Now;
    }
}
