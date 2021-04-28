namespace BirthClinicPlanningMongo.Models.Relatives
{
    public class Father : Relatives
    {
        public Father()
        {
        }

        public Father(string name) : base(name)
        {
            Relation = "Father";
        }

        public string CPRNumber { get; set; }
    }
}
