using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BirthClinicPlanningMongo.Models;
using BirthClinicPlanningMongo.Models.Rooms;
using BirthClinicPlanningMongo.Services;
using MongoDB.Bson;
using MongoDB.Driver;

namespace BirthClinicPlanningMongo
{ 
    public class MongoDbAlterData
    {
        private BirthClinicPlanningService _birthClinicPlanningService;

        public MongoDbAlterData(BirthClinicPlanningService mongo)
        {
            _birthClinicPlanningService = mongo;
        }

        public void EndBirth(string id)
        {
            var filterBirth = Builders<Birth>.Filter.Where(b => b.BirthId == id);

            var update = Builders<Birth>.Update.Set(b => b.PlannedEndDate,DateTime.Now.ToLocalTime());

            _birthClinicPlanningService.Births.UpdateOne(filterBirth, update);
        }

        public void CancelReservation(string id)
        {
            Birth birth = _birthClinicPlanningService.Births
                .Find(Builders<Birth>.Filter.Where(b => b.BirthId == id)).Single();

            foreach (var objectId in birth.ReservedRooms)
            {
                Console.WriteLine($"Rum id: {objectId}");
            }

            Console.WriteLine("Indtast id på rum");

            string roomId = Console.ReadLine();

            var roomFilter = Builders<Room>.Filter.Where(r => r.RoomId == roomId);
            Room room = _birthClinicPlanningService.Rooms.Find(roomFilter).Single();

            List<Reservation> test = new List<Reservation>();

            int counter = 0;
            foreach (var reservation in room.ReservationList)
            {
                test.Add(reservation);
                Console.WriteLine($"Reservation:{counter}");
                Console.WriteLine($"Startdato: {reservation.ReservationStartDate}");
                Console.WriteLine($"Slutdato: {reservation.ReservationEndDate}");
                counter++;
            }


            Console.WriteLine("Vælg hvilken reservation der skal slettes:");
            int choice = int.Parse(Console.ReadLine());

            var update = Builders<Room>.Update.PullFilter(r => r.ReservationList, Builders<Reservation>.Filter.Where(r => r.ReservationEndDate == test.ElementAt(choice).ReservationEndDate));

            _birthClinicPlanningService.Rooms.UpdateOne(roomFilter, update);

        }

    }
}
