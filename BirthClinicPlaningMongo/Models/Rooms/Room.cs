using System.Collections.Generic;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace BirthClinicPlanningMongo.Models.Rooms
{
    public abstract class Room
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string RoomId { get; set; }
        public int RoomNumber { get; set; }
        public string RoomType { get; set; }

        public List<ObjectId> ReservationList { get; set; }
        protected Room()
        {

        }

        protected Room(string roomType)
        {
            RoomType = roomType;
        }
    }
}
