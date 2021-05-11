using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BirthClinicPlanningMongo.Models;
using BirthClinicPlanningMongo.Models.Employee;
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

        public void ShowAvaliableClinciansAndRoomsForNextFiveDays()
        {
            List<Room> rooms = _birthClinicPlanningService.Rooms.Find(Builders<Room>.Filter.Empty).ToList();

            bool reserved = false;

            foreach (var room in rooms)
            {
                if (room.ReservationList.Count == 0)
                {
                    Console.WriteLine($"RumNummer: {room.RoomNumber} " +
                                      $"\nType rum: {room.RoomType} ");
                }

                foreach (var reservation in room.ReservationList)
                {
                    if (reservation.ReservationStartDate >= DateTime.Now.AddDays(4))
                    {
                        reserved = false;
                    }
                    else
                    {
                        reserved = true;
                    }
                }

                if (reserved == false)
                {
                    Console.WriteLine($"RumNummer: {room.RoomNumber} " +
                                      $"\nType rum: {room.RoomType} ");
                }
            }


            var birthFilter = Builders<Birth>.Filter.Gte(b => b.PlannedStartDate, DateTime.Now.AddDays(4));

            var birthList = _birthClinicPlanningService.Births.Find(birthFilter).ToList();

            foreach (var birth in birthList)
            {
                foreach (var objectId in birth.EmployeeList)
                {
                    var employee = _birthClinicPlanningService.Employees.Find(
                        Builders<Employee>.Filter.Where(e => e.EmployeeId == objectId.ToString())).Single();

                    Console.WriteLine($"Medarbejder id: {employee.EmployeeNumber} \nTitel: {employee.Position}");
                }
               
            }
        }

    }
    }
