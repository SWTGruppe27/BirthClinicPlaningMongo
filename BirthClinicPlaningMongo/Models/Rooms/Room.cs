using System.Collections.Generic;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace BirthClinicPlanningMongo.Models.Rooms
{
    public class Room
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string RoomId { get; set; }
        public int RoomNumber { get; set; }
        public string RoomType { get; set; }
        public string Type { get; set; }

        public List<Reservation> ReservationList { get; set; }
        protected Room()
        {
            ReservationList = new List<Reservation>();
        }

        protected Room(string roomType)
        {
            RoomType = roomType;
            ReservationList = new List<Reservation>();
        }
    }
}
