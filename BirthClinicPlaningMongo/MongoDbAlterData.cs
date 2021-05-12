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

        public void EndBirth()
        {
            List<Birth> list = PrintAllBirths();

            Console.WriteLine();
            Console.WriteLine("Indtast fødsels nummer:");
            int number = int.Parse(Console.ReadLine());

            var filterBirth = Builders<Birth>.Filter.Where(b => b.BirthId == list.ElementAt(number).BirthId);

            var update = Builders<Birth>.Update.Set(b => b.PlannedEndDate,DateTime.Now.ToLocalTime());

            _birthClinicPlanningService.Births.UpdateOne(filterBirth, update);
        }

        public void CancelReservation()
        {

            List<Birth> list = PrintAllBirths();

            Console.WriteLine();
            Console.WriteLine("Indtast fødsels nummer:");
            int number = int.Parse(Console.ReadLine());

            Birth birth = _birthClinicPlanningService.Births
                .Find(Builders<Birth>.Filter.Where(b => b.BirthId == list.ElementAt(number).BirthId)).Single();

            Console.WriteLine();

            int counterRoom = 0;
            foreach (var objectId in birth.ReservedRooms)
            {
                Console.WriteLine($"Nummer {counterRoom} Rum id: {objectId}");
                counterRoom++;
            }

            Console.WriteLine("Indtast nummer på rum");

            int roomNumber = int.Parse(Console.ReadLine());

            var roomFilter = Builders<Room>.Filter.Where(r => r.RoomId == birth.ReservedRooms.ElementAt(roomNumber).ToString());
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

            if (room.ReservationList.Count != 0)
            {
                Console.WriteLine("Vælg hvilken reservation der skal slettes:");
                int choice = int.Parse(Console.ReadLine());

                var update = Builders<Room>.Update.PullFilter(r => r.ReservationList, Builders<Reservation>.Filter.Where(r => r.ReservationEndDate == test.ElementAt(choice).ReservationEndDate));

                _birthClinicPlanningService.Rooms.UpdateOne(roomFilter, update);
            }
            else
            {
                Console.WriteLine("\nDer er ingen reservationer for dette rum");
            }
        }
        private List<Birth> PrintAllBirths()
        {
            List<Birth> birthList = _birthClinicPlanningService.Births.Find(Builders<Birth>.Filter.Empty).ToList();

            int counter = 0;

            foreach (var birth in birthList)
            {
                Console.WriteLine($"Fødsels nummer {counter}");
                Console.WriteLine($"Id: {birth.BirthId}");
                counter++;
            }

            return birthList;
        }
    }
}
