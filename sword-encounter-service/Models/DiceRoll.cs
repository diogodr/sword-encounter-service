using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace sword_encounter_service.Models
{
    public class DiceRoll
    {
        public string Type { get; set; }
        public string Value { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.Now;
    }
}
