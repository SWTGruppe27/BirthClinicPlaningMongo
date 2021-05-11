using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BirthClinicPlanningMongo.Models;
using BirthClinicPlanningMongo.Models.Rooms;
using BirthClinicPlanningMongo.Services;
using MongoDB.Driver;

namespace BirthClinicPlanningMongo
{
    public class MongoDbSearch
    {
        private BirthClinicPlanningService _birthClinicPlanningService;

        public MongoDbSearch(BirthClinicPlanningService birthClinicPlanningService)
        {
            _birthClinicPlanningService = birthClinicPlanningService;
        }

        public void ShowPlannedBirths()
        {

            var birthList = _birthClinicPlanningService.Births.Find(Builders<Birth>.Filter.Empty).ToList();

            foreach (var birth in birthList)
            {
                if (birth.PlannedStartDate.Date == DateTime.Now.Date || birth.PlannedStartDate.Date == DateTime.Now.Date.AddDays(1) ||
                    birth.PlannedStartDate.Date == DateTime.Now.Date.AddDays(2) && birth.PlannedEndDate! > DateTime.Now)
                {
                    Console.WriteLine($"\nFødsels id: {birth.BirthId}");
                    Console.WriteLine($"Planlagt start tid: { birth.PlannedStartDate.ToLocalTime()}");
                    Console.WriteLine($"Planlagt slut tid: {birth.PlannedEndDate.ToLocalTime()}");
                }
            }

            Console.WriteLine();
        }

        //public void ShowAvaliableClinciansAndRoomsForNextFiveDays()
        //{
            //var builder = Builders<Room>.Filter;

            //var filterRooms = Builders<Room>.Filter.Where(r => r.ReservationList.Count == 0 || r.ReservationList.TrueForAll(r => r.ReservationStartDate >= DateTime.Now.AddDays(4)));

            //var test 3 =
            //    builder.Gte(r => r.ReservationList.TrueForAll(r => r.ReservationStartDate >= DateTime.Now.AddDays(4)));   //FILTERET VIRKER IKKE MED STØRRE ELLER LIG MED TEGN


            //var test2 = builder.Gte(r => r.ReservationStartDate, DateTime.Now.AddDays(4));

            //var test = builder.Or(r => r.ReservationList.Count == 0, r.ReservationList.TrueForAll(builder.Gte(r => r.ReservationStartDate, DateTime.Now.AddDays(4)))));

            //var filterRooms = Builders<Room>.Filter.Where(builder.Or(r => r.ReservationList.Count == 0,
            //    r.ReservationList.TrueForAll(builder.Gte(r => r.ReservationStartDate, DateTime.Now.AddDays(4)))));

            //List<Room> listOfRooms = _birthClinicPlanningService.Rooms.Find(filterRooms).ToList();

        //    //var test = Builders<Room>.Filter.Gte(x => x.ReservationList.ToA, 14);


        //    List<Room> listOfRooms = _birthClinicPlanningService.Rooms.Find(test).ToList();


        //    foreach (var VARIABLE in listOfRooms)
        //    {
        //        Console.WriteLine(VARIABLE.RoomNumber);
        //    }
        //}

        ////    builder.
            //    foreach (var room in listOfRooms)
            //    {
            //        Console.WriteLine($"RumNummer: {room.RoomNumber} " +
            //                          $"\nType rum: {room.RoomType} ");
            //    }

            //    Console.WriteLine();

            //var cliniciansList = _birthClinicPlanningService.Clinicians
            //    .Include(c => c.WorksList)
            //    .ThenInclude(w => w.Birth)
            //    .Where(c => c.BirthRoomId == 0);

            //foreach (var clinician in cliniciansList)
            //{
            //    Console.WriteLine($"Medarbejder id: {clinician.EmployeeId} \nTitel: {clinician.Position}");
            //}

        }
    }
