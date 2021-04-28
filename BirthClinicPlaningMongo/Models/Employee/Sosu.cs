namespace BirthClinicPlanningMongo.Models.Employee
{
    public class Sosu : Clinicians 
    {
        public Sosu()
        {

        }

        public Sosu(string name) : base(name)
        {
            Position = "Sosu";
        }
    }
}
