using BirthClinicPlanningMongo.Models.Employee;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace BirthClinicPlanningMongo.Models
{
    public class Works
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string WorksId { get; set; }
        public ObjectId BirthId { get; set; }
        public ObjectId EmployeeId { get; set; }
    }
}
