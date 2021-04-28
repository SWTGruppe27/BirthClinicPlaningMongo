using System;
using BirthClinicPlanningMongo.Models;
using BirthClinicPlanningMongo.Models.Employee;
using BirthClinicPlanningMongo.Models.Rooms;
using MongoDB.Driver;

namespace BirthClinicPlanningMongo.Services
{
    public class BirthClinicPlanningService
    {
        //private IMongoCollection<Book> _books;
        private readonly string ConnectionString = "mongodb://localhost:27017/?readPreference=primary&appname=MongoDB%20Compass&ssl=false";
        private readonly string BirthClinicPlanningDb = "BirthClinicPlanningDB";
        private const string BirthCollection = "Birth";
        private const string WorksCollection = "Works";
        private const string ReservationCollection = "Reservations";
        private const string RoomCollection = "Rooms";
        private const string EmployeeCollection = "Employees";
        private IMongoCollection<Birth> _births;
        private IMongoCollection<Works> _works;
        private IMongoCollection<Reservation> _reservations;
        private IMongoCollection<Room> _rooms;
        private IMongoCollection<Employee> _employees;

        public BirthClinicPlanningService()
        {
            var client = new MongoClient(ConnectionString);
            var database = client.GetDatabase(BirthClinicPlanningDb);
            _births = database.GetCollection<Birth>(BirthCollection);
            _works = database.GetCollection<Works>(WorksCollection);
            _reservations = database.GetCollection<Reservation>(ReservationCollection);
            _rooms = database.GetCollection<Room>(RoomCollection);
            _employees = database.GetCollection<Employee>(EmployeeCollection);
            Console.WriteLine("test");
        }

        //  public List<Book> Get()
        //  {
        //    return _books.Find(book => true).ToList();
        //  }

        //  public Book Get(string id)
        //  {
        //    return _books.Find<Book>(book => book.Id == id).FirstOrDefault();
        //  }

        //  public IEnumerable<string> GetFormats(string id)
        //  {
        //    var projection = Builders<Book>.Projection.Include(b => b.Formats);

        //    var bson = _books.Find<Book>(book => book.Id == id).Project(projection).FirstOrDefault();
        //    var array = bson.GetElement("Formats").Value.AsBsonArray;


        //    return array.Select(str => str.AsString); // For complex objecst use BsonSerializer.Deserialize<ComplexObject>(str)
        //  }

        //  public Book Create(Book book)
        //  {
        //    _books.InsertOne(book);
        //    return book;
        //  }

        //  public void Update(string id, Book bookIn)
        //  {
        //    _books.ReplaceOne(book => book.Id == id, bookIn);
        //  }

        //  public void Remove(Book bookIn)
        //  {
        //    _books.DeleteOne(book => book.Id == bookIn.Id);
        //  }

        //  public void Remove(string id)
        //  {
        //    _books.DeleteOne(book => book.Id == id);
        //  }

    }
}