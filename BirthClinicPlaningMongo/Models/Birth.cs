using System;
using System.Collections.Generic;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace BirthClinicPlanningMongo.Models
{
    public class Birth
    {
        public Birth()
        {
            EmployeeList = new List<ObjectId>();
            ChildList = new List<Child>();
            RelativesList = new List<Relatives.Relatives>();
        }

        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string BirthId { get; set; }
        public DateTime PlannedStartDate { get; set; }
        public DateTime PlannedEndDate { get; set; }
        public int RoomNumber { get; set; }
        public List<ObjectId> EmployeeList { get; set; }
        public List<Child> ChildList { get; set; }
        public List<Relatives.Relatives> RelativesList { get; set; }
    }
}
