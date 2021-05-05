using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BirthClinicPlanningMongo.Models;
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
            //var birthList = IAsyncCursorSourceExtensions.ToList(_birthClinicPlanningService.Births.AsQueryable().Where(b => (b.PlannedStartDate.Date == DateTime.Now.Date ||
            //b.PlannedStartDate.Date == DateTime.Now.Date.AddDays(1) ||
            //b.PlannedStartDate.Date == DateTime.Now.Date.AddDays(2))
            //    && b.PlannedEndDate! > DateTime.Now));

            var filter = Builders<Birth>.Filter.Where(b => (b.PlannedStartDate.ToString("s") == DateTime.Now.ToString("s") ||
                                                            b.PlannedStartDate.ToString("s") == DateTime.Now.AddDays(1).ToString("s") ||
                                                            b.PlannedStartDate.ToString("s") == DateTime.Now.AddDays(2).ToString("s"))
                                                           && b.PlannedEndDate! > DateTime.Now);
            var birthList = _birthClinicPlanningService.Births.Find(filter).ToList();

            foreach (var birth in birthList)
            {
                Console.WriteLine($"\nFødsels id: {birth.BirthId}");
                Console.WriteLine($"Planlagt start tid: { birth.PlannedStartDate}");
                Console.WriteLine($"Planlagt slut tid: {birth.PlannedEndDate}");
            }

            Console.WriteLine();
        }
    }
}
