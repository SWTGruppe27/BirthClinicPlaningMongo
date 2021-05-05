using System.Collections.Generic;
using MongoDB.Bson;

namespace BirthClinicPlanningMongo.Models.Employee
{
    public abstract class Clinicians : Employee
    {
        protected Clinicians()
        {
        }

        protected Clinicians(string name) : base(name)
        {
            Title = "Clinician";
        }

    }
}
