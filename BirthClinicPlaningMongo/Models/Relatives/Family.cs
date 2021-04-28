namespace BirthClinicPlanningMongo.Models.Relatives
{
    public class Family : Relatives
    {
        public Family()
        {
        }

        public Family(string name) : base(name)
        {
            Relation = "Family";
        }

    }
}
