using System.Collections.Generic;
using MongoDB.Bson;

namespace BirthClinicPlanningMongo.Models.Employee
{
    public abstract class Clinicians : Employee
    {
        public string Position { get; set; }
        public ObjectId BirthRoomId { get; set; }
        public List<ObjectId> BirthList  { get; set; }

        protected Clinicians()
        {
        }

        protected Clinicians(string name) : base(name)
        {
            Title = "Clinician";
        }

    }
}
