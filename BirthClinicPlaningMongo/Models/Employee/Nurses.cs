namespace BirthClinicPlanningMongo.Models.Employee
{
    public class Nurses : Clinicians
    {
        public Nurses()
        {
        }

        public Nurses(string name) : base(name)
        {
            Position = "Nurse";
        }
    }
}
