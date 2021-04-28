﻿using System;
using System.Collections.Generic;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace BirthClinicPlanningMongo.Models
{
    public class Birth
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string BirthId { get; set; }
        public DateTime PlannedStartDate { get; set; }
        public DateTime PlannedEndDate { get; set; }
        public List<ObjectId> ReservationList { get; set; }
        public List<ObjectId> WorksList { get; set; }
        public List<Child> ChildList { get; set; }
        public List<Relatives.Relatives> RelativesList { get; set; }
    }
}
