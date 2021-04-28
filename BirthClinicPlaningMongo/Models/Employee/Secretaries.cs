namespace BirthClinicPlanningMongo.Models.Employee
{
    public class Secretaries : Employee
    {
        public Secretaries()
        {

        }

        public Secretaries(string name) : base(name)
        {
            Title = "Secretary";
        }
    }
}
