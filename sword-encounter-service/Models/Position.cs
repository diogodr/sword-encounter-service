using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace sword_encounter_service.Models
{
    public class Position
    {
        public string Xcart { get; set; }
        public string Ycart { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.Now;
    }
}
