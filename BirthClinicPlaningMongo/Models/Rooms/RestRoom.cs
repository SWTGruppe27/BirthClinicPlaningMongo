namespace BirthClinicPlanningMongo.Models.Rooms
{
    public abstract class RestRoom : Room
    {
        protected RestRoom()
        {
            RoomType = "Restroom";
        }
    }
}
