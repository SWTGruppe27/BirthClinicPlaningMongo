namespace BirthClinicPlanningMongo.Models.Employee
{
    public class Midwifes : Clinicians
    {
        public Midwifes()
        {
        }

        public Midwifes(string name) : base(name)
        {
            Position = "Midwife";
        }
    }
}
