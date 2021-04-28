namespace BirthClinicPlanningMongo.Models.Rooms
{
    public abstract class RestRoom : Room
    {
        public string Type { get; set; }
        protected RestRoom()
        {
            RoomType = "Restroom";
        }
    }
}
