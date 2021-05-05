using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BirthClinicPlanningMongo.Models;
using BirthClinicPlanningMongo.Models.Employee;
using BirthClinicPlanningMongo.Models.Rooms;
using BirthClinicPlanningMongo.Services;
using MongoDB.Bson;
using MongoDB.Driver;

namespace BirthClinicPlanningMongo
{
    public class MongoDbInsertData
    {
        private BirthClinicPlanningService _mongoDb;

        public MongoDbInsertData(BirthClinicPlanningService mongoDb)
        {
            _mongoDb = mongoDb;
        }

        public void NewBirth()
        {
            long numberOfMaternityRooms = 0;
            long numberOfRestRoom4HoursRooms = 0;
            long numberOfBirthRoomsRooms = 0;

            var filterBirthRoom = Builders<Room>.Filter.Where(r => r.RoomType == "BirthRoom");
            var filterMaternityRoom = Builders<Room>.Filter.Where(r => r.RoomType == "MaternityRoom");
            var filterRestRoom4HoursRoom = Builders<Room>.Filter.Where(r => r.RoomType == "RestRoom4Hours");

            numberOfBirthRoomsRooms = _mongoDb.Rooms.CountDocuments(filterBirthRoom);
            numberOfMaternityRooms = _mongoDb.Rooms.CountDocuments(filterMaternityRoom);
            numberOfRestRoom4HoursRooms = _mongoDb.Rooms.CountDocuments(filterRestRoom4HoursRoom);

            Console.WriteLine($"Vælg en start dato for din fødsel.");
            Birth newBirth = new Birth();
            
            newBirth.PlannedStartDate = ReservationDate();

            Console.WriteLine($"Vælg en slut dato for din fødsel.");
            newBirth.PlannedEndDate = ReservationDate();

            Console.WriteLine($"Vælg et fødselserum mellem {numberOfMaternityRooms + numberOfRestRoom4HoursRooms + 1} og {numberOfBirthRoomsRooms + numberOfMaternityRooms + numberOfRestRoom4HoursRooms}: ");
            int choice3 = int.Parse(Console.ReadLine());

            bool notDone = true;
            
            while (notDone)
            {
                Console.WriteLine("Tryk enter for at gå videre og 'e' for at afslutte");
                if (Console.ReadKey().Key != ConsoleKey.E)
                {
                    Console.WriteLine($"Vælg et ledig personale: ");
                    //dbSearch.ShowAvaliableClinciansAndRoomsForNextFiveDays();
                    //Employee eml = new Doctors();
                    //eml =  _mongoDb.Employees.Find(Builders<Employee>.Filter.Empty).Single();

                    //Console.WriteLine(eml.FullName);

                    Console.WriteLine("Indtast et Id på et ledig personale");
                    int choice4 = int.Parse(Console.ReadLine());


                    var filterEmployee = Builders<Employee>.Filter.Where(e => e.EmployeeNumber == choice4 );

                    Employee em = _mongoDb.Employees.Find(filterEmployee).Single();

                    newBirth.EmployeeList.Add(ObjectId.Parse(em.EmployeeId));
                }
                else
                {
                    notDone = false;
                }
            }

            _mongoDb.Births.InsertOne(newBirth);
        }

        private static DateTime ReservationDate()
        {
            Console.Write("Vælg en måned: ");
            int month = int.Parse(Console.ReadLine());
            Console.Write("Vælg en dag: ");
            int day = int.Parse(Console.ReadLine());
            Console.Write("Vælg et år: ");
            int year = int.Parse(Console.ReadLine());

            Console.Write("Vælg et tidspunkt (time): ");
            int time = int.Parse(Console.ReadLine());
            Console.Write("Vælg et tidspunkt (minutter): ");
            int minuts = int.Parse(Console.ReadLine());
            DateTime inputtedDate = new DateTime(year, month, day, time, minuts, 0);

            return inputtedDate;
        }

    }
}
