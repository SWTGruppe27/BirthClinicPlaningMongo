using System;
using System.Collections.Generic;
using BirthClinicPlanningMongo.Models;
using BirthClinicPlanningMongo.Models.Employee;
using BirthClinicPlanningMongo.Models.Rooms;
using MongoDB.Bson;
using MongoDB.Driver;

namespace BirthClinicPlanningMongo.Services
{
    public class BirthClinicPlanningService
    {
        private readonly string ConnectionString = "mongodb://localhost:27017/?readPreference=primary&appname=MongoDB%20Compass&ssl=false";
        private readonly string BirthClinicPlanningDb = "BirthClinicPlanningDB";
        private const string BirthCollection = "Birth";
        private const string WorksCollection = "Works";
        private const string ReservationCollection = "Reservations";
        private const string RoomCollection = "Rooms";
        private const string EmployeeCollection = "Employees";
        public IMongoCollection<Birth> Births { get; private set; }
        public IMongoCollection<Reservation> Reservations { get; private set; }
        public IMongoCollection<Room> Rooms { get; private set; }
        public IMongoCollection<Employee> Employees { get; private set; }

        public BirthClinicPlanningService()
        {
            var client = new MongoClient(ConnectionString);
            var database = client.GetDatabase(BirthClinicPlanningDb);
            Births = database.GetCollection<Birth>(BirthCollection);
            Reservations = database.GetCollection<Reservation>(ReservationCollection);
            Rooms = database.GetCollection<Room>(RoomCollection);
            Employees = database.GetCollection<Employee>(EmployeeCollection);

            var filterRoom = Builders<Room>.Filter.Empty;
            long numberOfRooms = Rooms.CountDocuments(filterRoom);

            if(numberOfRooms == 0)
            {
                SeedRooms(Rooms);
            }

            var filterEmployees = Builders<Employee>.Filter.Empty;
            long numberOfEmployees = Employees.CountDocuments(filterEmployees);

            if (numberOfEmployees == 0)
            {
                SeedEmployees(Employees);
            }
        }

        private void SeedEmployees(IMongoCollection<Employee> collection)
        {
            int numberOfSous = 20;

            string[] namesSosus = new string[] { "Sofie Jensen", "Søren Larsen", "Sarah Hansen", "Susanne Himmelblå", "Simon Bjermand Kjær", "Simon Schou Jensen", "Selma Jakobsen", "Susan Kristensen", "Stine Olsen", "Sandra Toft", "Silke Rasmusen", "Siff Andersen", "Sophie Loft", "Sol-Solvej Solskin", "Simone Kjær", "Sabrina Møller Andersen", "Sara Christensen", "Sascha Madsen", "Sidsel Lund Sørensen", "Sten Steensen" };

            List<Sosu> sosus = new List<Sosu>();

            for (int i = 1; i < numberOfSous + 1; i++)
            {
                Sosu s = new Sosu(namesSosus[i - 1]);
                s.EmployeeNumber = i;
                sosus.Add(s);
            }

            collection.InsertMany(sosus);

            int numberOfDoctors = 5;

            string[] namesDoctor = new string[5] { "Danny Boy", "Dirk Passer", "David Davidson", "Diana Jensen", "Daniel Danielsen" };

            List<Doctors> doctors = new List<Doctors>();

            for (int i = numberOfSous + 1; i < numberOfSous + numberOfDoctors + 1; i++)
            {
                Doctors d = new Doctors(namesDoctor[i - numberOfSous - 1]);
                d.EmployeeNumber = i;
                doctors.Add(d);
            }

            collection.InsertMany(doctors);

            int numberOfNurses = 20;

            string[] namesNurses = new string[20] { "Niels Nielsen", "Nikolaj Nikolajsen", "Niklas Landin", "Natasha Romanoff", "Natalia Alianovna Romanova", "Nicki Sørensen", "Niller Nielsen", "Noah Overbyen", "Nik Petersen", "Nora Andersen", "Nadai Jensen", "Nikita Mortensen Bækgaard", "Nanna Louise Johansen", "Nelly Winston", "Naja Madsen", "Neville Longbottom", "Norbit Albertrise", "No Name", "Nairobi Kenya", "Norge Nordmand" };

            List<Nurses> nurses = new List<Nurses>();

            for (int i = numberOfSous + numberOfDoctors + 1; i < numberOfSous + numberOfDoctors + numberOfNurses + 1; i++)
            {
                Nurses n = new Nurses(namesNurses[i - numberOfSous - numberOfDoctors - 1]);
                n.EmployeeNumber = i;
                nurses.Add(n);
            }

            collection.InsertMany(nurses);


            int numberOfMidwifes = 10;

            string[] namesMidwifes = new string[10] { "Malfoy Draco", "Mille Madsen", "Mads Madsen", "Marie Toft", "Malene Sørensen", "Morten Mortensen", "Martin Frederiksen", "Marcus Nielsen", "Maja Mikkelsen", "Maria Loft Nielsen" };

            List<Midwifes> midwifes = new List<Midwifes>();

            for (int i = numberOfSous + numberOfDoctors + numberOfNurses + 1; i < numberOfSous + numberOfDoctors + numberOfNurses + numberOfMidwifes + 1; i++)
            {
                Midwifes m = new Midwifes(namesMidwifes[i - numberOfSous - numberOfDoctors - numberOfNurses - 1]);
                m.EmployeeNumber = i;
                midwifes.Add(m);
            }

            collection.InsertMany(midwifes);


            int numberOfSecretaries = 4;

            string[] namesSecretaries = new string[4] { "Simba Kongesøn", "Signe Mikkelsen", "Sune Orlater", "Søren Krag" };

            List<Secretaries> secretaries = new List<Secretaries>();

            for (int i = numberOfSous + numberOfDoctors + numberOfNurses + numberOfMidwifes + 1; i < numberOfSous + numberOfDoctors + numberOfNurses + numberOfMidwifes + numberOfSecretaries + 1; i++)
            {
                Secretaries ss = new Secretaries(namesSecretaries[i - numberOfSous - numberOfDoctors - numberOfNurses - numberOfMidwifes - 1]);
                ss.EmployeeNumber = i;
                secretaries.Add(ss);
            }

            collection.InsertMany(secretaries);
        }

        private void SeedRooms(IMongoCollection<Room> collection)
        {
            List<MaternityRoom> maternityRooms = new List<MaternityRoom>();

            for (int i = 1; i < 23; i++)
            {
                MaternityRoom maternityRoom = new MaternityRoom();
                maternityRoom.RoomNumber = i;
                maternityRooms.Add(maternityRoom);
            }
            
            collection.InsertMany(maternityRooms);

            List<RestRoom4Hours> restRoom4Hours = new List<RestRoom4Hours>();

            for (int i = 23; i < 28; i++)
            {
                RestRoom4Hours restRoom4Hour = new RestRoom4Hours();
                restRoom4Hour.RoomNumber = i;
                restRoom4Hours.Add(restRoom4Hour);
            }
            collection.InsertMany(restRoom4Hours);

            List<BirthRoom> birthRooms = new List<BirthRoom>();

            for (int i = 28; i < 43; i++)
            {
                BirthRoom birthRoom = new BirthRoom();
                birthRoom.RoomNumber = i;
                birthRooms.Add(birthRoom);
            }

            collection.InsertMany(birthRooms);
        }
    }
}