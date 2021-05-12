using System;
using System.Collections.Generic;
using System.Linq;
using BirthClinicPlanningMongo.Models;
using BirthClinicPlanningMongo.Models.Employee;
using BirthClinicPlanningMongo.Models.Rooms;
using BirthClinicPlanningMongo.Services;
using MongoDB.Bson;
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
            var birthFilter1 = Builders<Birth>.Filter.Lte(b => b.PlannedStartDate, DateTime.Now.AddDays(2));

            var birthFilter2 = Builders<Birth>.Filter.Gt(b => b.PlannedEndDate, DateTime.Now);

            var birthList = _birthClinicPlanningService.Births.Find(birthFilter1 & birthFilter2).ToList();

            foreach (var birth in birthList)
            {
                Console.WriteLine($"\nFødsels id: {birth.BirthId}");
                Console.WriteLine($"Planlagt start tid: { birth.PlannedStartDate.ToLocalTime()}");
                Console.WriteLine($"Planlagt slut tid: {birth.PlannedEndDate.ToLocalTime()}");
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
                    Employee employee = FindEmployeeById(objectId);
                    Console.WriteLine($"Medarbejder id: {employee.EmployeeNumber} \nTitel: {employee.Position}");
                }

            }
        }

        public void ShowInfoAboutOngoingBirths()
        {

            var birthFilter = Builders<Birth>.Filter.Gt(b => b.PlannedEndDate, DateTime.Now.Date);

            var birthList = _birthClinicPlanningService.Births.Find(birthFilter).ToList();

            foreach (var birth in birthList)
            {
                Console.WriteLine($"Fødses id: {birth.BirthId}");
                Console.WriteLine($"Starttid: {birth.PlannedStartDate}");
                Console.WriteLine($"Sluttid: {birth.PlannedEndDate}\n");

                Console.WriteLine("Klinikere tilknyttet:");

                foreach (var objectId in birth.EmployeeList)
                {
                    Employee employee = FindEmployeeById(objectId);

                    Console.WriteLine($"Medarbejder id: {employee.EmployeeNumber} " +
                                      $"\nNavn: {employee.FullName} " +
                                      $"\nTitle: {employee.Title}");
                }

                foreach (var relatives in birth.RelativesList)
                {
                    switch (relatives.Relation)
                    {
                        case "Mother":
                            Console.WriteLine($"Mor: {relatives.FullName}");
                            break;
                        case "Father":
                            Console.WriteLine($"Far: {relatives.FullName}");
                            break;
                    }
                }

                Console.WriteLine($"Rumnummer: {birth.RoomNumber}");
            }
        }

        public void ShowInfoAboutRestRoomsInUse()
        {
            var roomFilter = Builders<Room>.Filter.Where(r => r.ReservationList.Count != 0);

            var roomList = _birthClinicPlanningService.Rooms.Find(roomFilter).ToList();

            foreach (var room in roomList)
            {
                foreach (var reservation in room.ReservationList)
                {
                    if (reservation.ReservationEndDate > DateTime.Now && reservation.ReservationStartDate < DateTime.Now)
                    {
                        if (room.RoomType != "BirthRoom")
                        {
                            Console.WriteLine($"Rum Id: {room.RoomId} " +
                                              $"\nRum type: {room.RoomType} " +
                                              $"\nStart dato: {reservation.ReservationStartDate}" +
                                              $"\nSlut dato: {reservation.ReservationEndDate}\n");

                            var birthList = _birthClinicPlanningService.Births.Find(Builders<Birth>.Filter.Empty).ToList();

                            foreach (var birth in birthList)
                            {
                                foreach (var objectId in birth.ReservedRooms)
                                {
                                    if (objectId.ToString() == room.RoomId)
                                    {
                                        foreach (var child in birth.ChildList)
                                        {
                                            Console.WriteLine($"Barn Id: {child.ChildId} " +
                                                              $"\nCpr nummer: { child.CprNumber}");
                                        }

                                        foreach (var relatives in birth.RelativesList)
                                        {
                                            Console.WriteLine($"Relation: {relatives.Relation}" +
                                                              $"\nNavn: { relatives.FullName}");
                                        }
                                    }
                                }
                            }
                        }
                    }
                }
                Console.WriteLine();
            }
        }

        public void ShowReservedRooms()
        {
            List<Birth> list = PrintAllBirths();

            Console.WriteLine("Indtast fødsels nummer:");
            int id = int.Parse(Console.ReadLine());

            Birth birth = _birthClinicPlanningService.Births.Find(Builders<Birth>.Filter.Where(b => b.BirthId == list.ElementAt(id).BirthId)).Single();

            foreach (var objectId in birth.ReservedRooms)
            {
                Room room = FindRoomById(objectId);
                Console.WriteLine($"Rumnummer: {room.RoomNumber} \nRum type: {room.RoomType}");
            }
        }

        public void ShowCliniciansAssignedBirths()
        {

            List<Birth> list = PrintAllBirths();

            Console.WriteLine();
            Console.WriteLine("Indtast fødsels nummer:");
            int number = int.Parse(Console.ReadLine());

            Birth birth = _birthClinicPlanningService.Births.Find(Builders<Birth>.Filter.Where(b => b.BirthId == list.ElementAt(number).BirthId)).Single();

            foreach (var objectId in birth.EmployeeList)
            {
                Employee employee = FindEmployeeById(objectId);

                Console.WriteLine($"Medarbejdernummer: {employee.EmployeeNumber} " +
                                  $"\nNavn: {employee.FullName} " +
                                  $"\nPosition: {employee.Position}");
            }
        }

        private Employee FindEmployeeById(ObjectId employeeId)
        {
            return _birthClinicPlanningService.Employees.Find(Builders<Employee>.Filter.Where(e => e.EmployeeId == employeeId.ToString())).Single();
        }

        private Room FindRoomById(ObjectId roomId)
        {
            return _birthClinicPlanningService.Rooms.Find(Builders<Room>.Filter.Where(r => r.RoomId == roomId.ToString())).Single();
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
