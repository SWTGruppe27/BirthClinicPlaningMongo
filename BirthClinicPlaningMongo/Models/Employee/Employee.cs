using System.Collections.Generic;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace BirthClinicPlanningMongo.Models.Employee
{
    public class Employee
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string EmployeeId { get; set; }
        public int EmployeeNumber { get; set; }
        public string Title { get; set; } //Secretarie og clinician
        public string FullName { get; set; }
        public string Position { get; set; }
        public ObjectId BirthRoomId { get; set; }
        public List<ObjectId> BirthList { get; set; }

        protected Employee()
        {
        }

        protected Employee(string name)
        {
            FullName = name;
        }
    }
}
