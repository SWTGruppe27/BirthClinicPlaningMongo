using System;
using System.Collections.Generic;
using BirthClinicPlanningMongo.Models.Relatives;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace BirthClinicPlanningMongo.Models
{
    public class Child
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string ChildId { get; set; }
        public int CprNumber { get; set; }
        public DateTime Birthdate { get; set; }
        public string Gender { get; set; }
        public ObjectId BirthId { get; set; }
        public List<ObjectId> RelativesChild { get; set; }
    }
}
