using System;
using BirthClinicPlanningMongo.Services;

namespace BirthClinicPlanningMongo
{
    class Program
    {
        static void Main(string[] args)
        {
            BirthClinicPlanningService birthClinicPlanningService = new BirthClinicPlanningService();
            MongoDbInsertData mongoDbInsert = new MongoDbInsertData(birthClinicPlanningService);
            MongoDbSearch mongoDbSearch = new MongoDbSearch(birthClinicPlanningService);
            MongoDbAlterData mongoDbAlter = new MongoDbAlterData(birthClinicPlanningService);

            bool running = true;

            Console.WriteLine("Velkommen til BirthClinicPlanning!\n");

            while (running)
            {
                Console.WriteLine();
                Console.WriteLine("Vælg en af følgende muligheder: ");
                Console.WriteLine("1. Vis planlagte fødsler");
                Console.WriteLine("2. Vis ledige rum for de kommende fire dage");
                Console.WriteLine("3. Vis information om igangværende fødsler");
                Console.WriteLine("4. Vis information om hvilerum i brug");
                Console.WriteLine("5. Vis reserverede fødselsrum");
                Console.WriteLine("6. Vis klinikere tilkoblet fødsler");
                Console.WriteLine("7. Marker en fødsel som færdig");
                Console.WriteLine("8. Annuller en reservation på et rum");
                Console.WriteLine("9. Opret en fødsel");
                Console.WriteLine("10. Opret en ny reservation");
                Console.WriteLine("11. Luk programmet\n");

                string input = Console.ReadLine();

                switch (int.Parse(input))
                {
                    case 1:
                        mongoDbSearch.ShowPlannedBirths();
                        break;

                    case 2:
                        mongoDbSearch.ShowAvaliableClinciansAndRoomsForNextFiveDays();
                        break;

                    case 3:
                        mongoDbSearch.ShowInfoAboutOngoingBirths();
                        break;

                    case 4:
                        mongoDbSearch.ShowInfoAboutRestRoomsInUse();
                        break;

                    case 5:
                        Console.WriteLine("Indtast fødsels id:");
                        string id1 = Console.ReadLine();
                        Console.WriteLine("");
                        mongoDbSearch.ShowReservedRooms(id1);
                        break;

                    case 6:
                        Console.WriteLine("Indtast fødsels id:");
                        string id2 = Console.ReadLine();
                        Console.WriteLine("");
                        mongoDbSearch.ShowCliniciansAssignedBirths(id2);
                        break;

                    case 7:
                        Console.WriteLine("Indtast fødsels id:");
                        string id3 = Console.ReadLine();
                        mongoDbAlter.EndBirth(id3);
                        break;

                    case 8:
                        Console.WriteLine("Indtast fødsels id:");
                        string id4 = Console.ReadLine();
                        mongoDbAlter.CancelReservation(id4);
                        break;

                    case 9:
                        mongoDbInsert.NewBirth();
                        break;

                    case 10:
                        Console.WriteLine("Lav en reservation i en af disse rumtyper: \n a: Maternityroom \n b: Restroom (4 hours) \n c: Birthroom \n");
                        mongoDbInsert.MakeReservation();
                        break;

                    case 11:
                        running = false;
                        break;

                    default:
                        Console.WriteLine("Forkert input. Vælg et tal mellem 1 og 7.\n");
                        break;
                }
            }

        }
    }
}
