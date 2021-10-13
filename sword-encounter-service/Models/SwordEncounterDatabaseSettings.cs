using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace sword_encounter_service.Models
{
    public class SwordEncounterDatabaseSettings : ISwordEncounterDatabaseSettings
    {
        public string ConnectionString { get; set; }
        public string DatabaseName { get; set; }
    }

    public interface ISwordEncounterDatabaseSettings
    {
        string ConnectionString { get; set; }
        string DatabaseName { get; set; }
    }
}
