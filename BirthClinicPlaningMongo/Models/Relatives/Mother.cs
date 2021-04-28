namespace BirthClinicPlanningMongo.Models.Relatives
{
    public class Mother : Relatives
    {
        public Mother()
        {
        }
        public Mother(string name) : base(name)
        {
            Relation = "Mother";
        }
        public string CPRNumber { get; set; }
    }
}
