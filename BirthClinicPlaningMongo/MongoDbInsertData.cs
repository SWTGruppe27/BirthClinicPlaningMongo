using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BirthClinicPlanningMongo.Models;
using BirthClinicPlanningMongo.Models.Employee;
using BirthClinicPlanningMongo.Models.Relatives;
using BirthClinicPlanningMongo.Models.Rooms;
using BirthClinicPlanningMongo.Services;
using MongoDB.Bson;
using MongoDB.Driver;

namespace BirthClinicPlanningMongo
{
    public class MongoDbInsertData
    {
        private BirthClinicPlanningService _birthClinicPlanningService;

        public MongoDbInsertData(BirthClinicPlanningService birthClinicPlanningService)
        {
            _birthClinicPlanningService = birthClinicPlanningService;
        }

        public void NewBirth()
        {
            List<long> getListOfRooms = new List<long>();

            getListOfRooms = GetNumberOfRooms(); //birth = 0, maternity = 1 og restRoom = 2

            Console.WriteLine($"Vælg en start dato for din fødsel.");
            Birth newBirth = new Birth();

            newBirth.PlannedStartDate = ReservationDate();

            Console.WriteLine($"Vælg en slut dato for din fødsel.");
            newBirth.PlannedEndDate = ReservationDate();

            Console.WriteLine(
                $"Vælg et fødselserum mellem {getListOfRooms.ElementAt(1) + getListOfRooms.ElementAt(2) + 1} og {getListOfRooms.ElementAt(0) + getListOfRooms.ElementAt(1) + getListOfRooms.ElementAt(2)}: ");

            int choice3 = int.Parse(Console.ReadLine());

            newBirth.RoomNumber = choice3;

            bool notDone = true;

            while (notDone)
            {
                Console.WriteLine("Tryk enter for at vælge personale eller 'e' for at afslutte");
                if (Console.ReadKey().Key != ConsoleKey.E)
                {
                    Console.WriteLine($"Vælg et ledig personale: ");
                    //dbSearch.ShowAvaliableClinciansAndRoomsForNextFiveDays();

                    List<Employee> eml = _birthClinicPlanningService.Employees.Find(Builders<Employee>.Filter.Empty).ToList();

                    foreach (var employee in eml)
                    {
                        Console.WriteLine($"id: {employee.EmployeeNumber} navn: {employee.FullName}");
                    }

                    Console.WriteLine("Indtast et Id på et ledig personale");
                    int choice4 = int.Parse(Console.ReadLine());


                    var filterEmployee = Builders<Employee>.Filter.Where(e => e.EmployeeNumber == choice4);

                    Employee em = _birthClinicPlanningService.Employees.Find(filterEmployee).Single();

                    newBirth.EmployeeList.Add(ObjectId.Parse(em.EmployeeId));
                }
                else
                {
                    notDone = false;
                }
            }

            List<Relatives> relativesList = new List<Relatives>();


            bool notdone1 = true;

            while (notdone1)
            {
                Console.WriteLine("\nTryk enter for at tilføje familiemedlemmer til fødslen eller 'e' for at afslutte");

                if (Console.ReadKey().Key != ConsoleKey.E)
                {
                    Relatives r1;

                    Console.WriteLine("\nIndtast m for mor, f for far eller a for familie");
                    string choice = Console.ReadLine();

                    Console.WriteLine("Indtast navn:");
                    string name = Console.ReadLine();

                    switch (choice)
                    {
                        case "m":
                            r1 = new Mother(name);
                            break;
                        case "f":
                            r1 = new Father(name);
                            break;
                        case "a":
                            r1 = new Family(name);
                            break;
                        default:

                            r1 = new Family(name);
                            break;
                    }

                    relativesList.Add(r1);
                }
                else
                {
                    notdone1 = false;
                }
            }

            foreach (var relatives in relativesList)
            {
                newBirth.RelativesList.Add(relatives);
            }

            _birthClinicPlanningService.Births.InsertOne(newBirth);
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

            return inputtedDate.ToLocalTime();
        }

        public void MakeReservation()
        {
            List<long> getListOfRooms = GetNumberOfRooms(); //birth = 0, maternity = 1 og restRoom = 2

            bool notTrue = true;

            while (notTrue)
            {
                string choice = Console.ReadLine();

                switch (choice)
                {
                    case "a":
                        Console.WriteLine($"Vælg et id mellem 1 og {getListOfRooms.ElementAt(1)}:");
                        notTrue = false;
                        break;

                    case "b":
                        Console.WriteLine(
                            $"Vælg et id mellem {getListOfRooms.ElementAt(1) + 1} og {getListOfRooms.ElementAt(2) + getListOfRooms.ElementAt(1)}:");
                        notTrue = false;
                        break;

                    case "c":
                        Console.WriteLine(
                            $"Vælg et id mellem {getListOfRooms.ElementAt(1) + getListOfRooms.ElementAt(2) + 1} og {getListOfRooms.ElementAt(0) + getListOfRooms.ElementAt(1) + getListOfRooms.ElementAt(2)}:");
                        notTrue = false;
                        break;

                    default:
                        Console.WriteLine($"Vælg et gyldigt bogstav");
                        break;
                }
            }

            int choice1 = int.Parse(Console.ReadLine());
            Reservation newReservation = new Reservation();

            Console.WriteLine($"Vælg en start dato for din reservation.");
            newReservation.ReservationStartDate = ReservationDate();

            Console.WriteLine($"Vælg en slut dato for din reservation.");
            newReservation.ReservationEndDate = ReservationDate();

            var filterRooms = Builders<Room>.Filter.Where(e => e.RoomNumber == choice1);

            Room room = _birthClinicPlanningService.Rooms.Find(filterRooms).Single();

            room.ReservationList.Add(newReservation);

            Console.WriteLine();

            List<Birth> list = PrintAllBirths();

            Console.WriteLine("Vælg et fødselsnummer som skal have en reservation");

            Console.WriteLine("\nIndtast fødselsnummer:");
            int number = int.Parse(Console.ReadLine());

            var filterBirth = Builders<Birth>.Filter.Where(b => b.BirthId == list.ElementAt(number).BirthId);

            var updateBirth = Builders<Birth>.Update.Push(r => r.ReservedRooms, ObjectId.Parse(room.RoomId));

            _birthClinicPlanningService.Births.UpdateOne(filterBirth, updateBirth);

            var update = Builders<Room>.Update.Push(r => r.ReservationList, newReservation);

            _birthClinicPlanningService.Rooms.UpdateOne(filterRooms, update);
        }

        public List<long> GetNumberOfRooms()
        {
            long numberOfMaternityRooms = 0;
            long numberOfRestRoom4HoursRooms = 0;
            long numberOfBirthRoomsRooms = 0;

            var filterBirthRoom = Builders<Room>.Filter.Where(r => r.RoomType == "BirthRoom");
            var filterMaternityRoom = Builders<Room>.Filter.Where(r => r.Type == "MaternityRoom");
            var filterRestRoom4HoursRoom = Builders<Room>.Filter.Where(r => r.Type == "RestRoom4Hours");

            numberOfBirthRoomsRooms = _birthClinicPlanningService.Rooms.CountDocuments(filterBirthRoom);
            numberOfMaternityRooms = _birthClinicPlanningService.Rooms.CountDocuments(filterMaternityRoom);
            numberOfRestRoom4HoursRooms = _birthClinicPlanningService.Rooms.CountDocuments(filterRestRoom4HoursRoom);

            List<long> listOfRooms = new List<long>();

            listOfRooms.Add(numberOfBirthRoomsRooms); //plads 0
            listOfRooms.Add(numberOfMaternityRooms); //plads 1
            listOfRooms.Add(numberOfRestRoom4HoursRooms); //plads 2


            return listOfRooms;
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
