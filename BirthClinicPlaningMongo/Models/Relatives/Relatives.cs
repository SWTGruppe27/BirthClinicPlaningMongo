using System.Collections.Generic;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace BirthClinicPlanningMongo.Models.Relatives
{
    public class Relatives
    {
        protected Relatives()
        {
        }

        protected Relatives(string name)
        {
            FullName = name;
        }

        public string FullName { get; set; }
        public string Relation { get; set; }
        public string CPRNumber { get; set; }
    }
}
