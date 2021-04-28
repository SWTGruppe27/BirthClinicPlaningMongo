using System;
using BirthClinicPlanningMongo.Models.Rooms;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace BirthClinicPlanningMongo.Models
{
    public class Reservation
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string ReservationId { get; set; }
        public ObjectId RoomId { get; set; }
        public DateTime ReservationStartDate { get; set; }
        public DateTime ReservationEndDate { get; set; }
        public ObjectId RelativesId { get; set; }
        public Relatives.Relatives Relatives { get; set; }

    }
}
