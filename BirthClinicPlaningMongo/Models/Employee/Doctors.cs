namespace BirthClinicPlanningMongo.Models.Employee
{
    public class Doctors : Clinicians
    {
        public Doctors()
        {
        }

        public Doctors(string name) : base(name)
        {
            Position = "Doctor";
        }
    }
}
