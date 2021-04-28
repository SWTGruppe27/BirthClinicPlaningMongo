using System.Collections.Generic;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace BirthClinicPlanningMongo.Models.Relatives
{
    public abstract class Relatives
    {
        protected Relatives()
        {
        }

        protected Relatives(string name)
        {
            FullName = name;
        }

        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string RelativesId { get; set; }
        public string FullName { get; set; }
        public string Relation { get; set; }
        public List<ObjectId> ReservationList { get; set; }
    }
}
