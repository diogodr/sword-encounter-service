using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace sword_encounter_service.Models
{
    public class GameResponse
    {
        public Campaign Campaign { get; set; }
        public List<Character> Characters { get; set; }
    }
}
