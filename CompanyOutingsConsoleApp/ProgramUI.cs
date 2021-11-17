using CompanyOutingsRepository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static CompanyOutingsRepository.Outing;

namespace CompanyOutingsConsoleApp
{
    public class ProgramUI
    {
        private readonly OutingsRepo _repo = new OutingsRepo();

        public void Run()
        {
            SeedContent();
            RunMenu();
        }

        private void RunMenu()
        {
            bool continueToRun = true;
            while (continueToRun)
            {
                Console.Clear();
                Console.WriteLine("Select a menu option\n" +
                    "1. Display all outings\n" +
                    "2. Add outing to list\n" +
                    "3. View total cost of all outings and total cost by category\n" +
                    "4. Exit");
                string userInput = Console.ReadLine();
                switch (userInput)
                {
                    case "1":
                        DisplayAllOutings();
                        break;
                    case "2":
                        AddOuting();
                        break;
                    case "3":
                        DisplayAllOutingTotals();
                        break;
                    case "4":
                        continueToRun = false;
                        break;
                    default:
                        Console.WriteLine("Please Enter a Valid Number");
                        AnyKey();
                        break;
                }
            }
        }

        private void DisplayAllOutings()
        {
            Console.Clear();
            List<Outing> outings = _repo.GetOutings();
            //admittedly this is probably confusing to read
            foreach (Outing outing in outings)
            {
                Console.WriteLine($"Outing ID: {outing.OutingId}\n" +
                    $"Outing Type: {outing.Type}\n" +
                    $"Number of Attendees: {outing.Attendees}\n" +
                    $"Date of Event: {outing.Date}\n" +
                    $"Total Cost: ${outing.TotalCost}\n" +
                    $"Total Cost Per Person: ${outing.TotalCostPerPerson}\n");
                Console.WriteLine("\n");
            }
            AnyKey();
            if (outings.Count() == 0)
            {
                Console.WriteLine("There are now Outings recorded in the list");
                AnyKey();
            }
        }

        private void DisplayAllOutingTotals()
        {
            Console.Clear();

            decimal costOfAll = _repo.CalculateCostOfAllOutings();
            Console.WriteLine("Total Cost of ALL Outings: " + costOfAll);
            Console.WriteLine("\n");


            Console.WriteLine("Total Cost of all Golf Outings: " + _repo.CalculateCostByType(EventType.Golf));
            Console.WriteLine("\n");

            Console.WriteLine("Total Cost of all Bowling Outings: " + _repo.CalculateCostByType(EventType.Bowling));
            Console.WriteLine("\n");

            Console.WriteLine("Total Cost of all Amusement Park Outings: " + _repo.CalculateCostByType(EventType.AmusmentPark));
            Console.WriteLine("\n");

            Console.WriteLine("Total Cost of all Concert Outings: " + _repo.CalculateCostByType(EventType.Concert));
            Console.WriteLine("\n");

            AnyKey();
        }

        private void AddOuting()
        {
            Console.Clear();
            Outing newOuting = new Outing();

            //Outing ID
            //user input is a number. checking they dont input something else
            bool checkInputIsNumber = true;
            while (checkInputIsNumber)
            {
                Console.WriteLine("Enter Outing ID Number: ");
                int userInput;
                bool isValidNumber = Int32.TryParse(Console.ReadLine(), out userInput);
                if (isValidNumber == true)
                {
                    newOuting.OutingId = userInput;
                    checkInputIsNumber = false;
                }
                else
                {
                    Console.WriteLine("Please enter a valid number: ");
                    AnyKey();
                }
            }

            //Outing Type
            Console.WriteLine("Input Number of Event Type\n" +
                    "1. Golf\n" +
                    "2. Bowling\n" +
                    "3. Amusment Park\n" +
                    "4. Concert\n");
            string typeInput = Console.ReadLine();
            int typeId = int.Parse(typeInput);
            newOuting.Type = (EventType)typeId;

            //Outing Attendees
            bool inputIsNumber = true;
            while (inputIsNumber)
            {
                Console.WriteLine("Enter Number of Attendees: ");
                int input;
                bool validNumber = Int32.TryParse(Console.ReadLine(), out input);
                if (validNumber == true)
                {
                    newOuting.Attendees = input;
                    inputIsNumber = false;
                }
                else
                {
                    Console.WriteLine("Please enter a number next time.");
                    AnyKey();
                }
            }
            //Outing Date
            Console.WriteLine("Enter date of the outing (yyyy, mm, dd):");
            newOuting.Date = Convert.ToDateTime(Console.ReadLine());

            //Outing Cost
            bool isValidAmount = true;
            while (isValidAmount)
            {
                Console.WriteLine("Enter Total Cost of Outing: ");
                decimal inputAmount;
                bool isValidDecimal = decimal.TryParse(Console.ReadLine(), out inputAmount);
                if (isValidDecimal == true)
                {
                    newOuting.TotalCost = inputAmount;
                    isValidAmount = false;
                }
                else
                {
                    Console.WriteLine("Please enter a valid price: ");
                    AnyKey();
                }
            }

            bool wasAdded = _repo.AddOutingToDirectory(newOuting);
            if (wasAdded == true)
            {
                Console.WriteLine("Outing has been added");
                AnyKey();
            }
            else
            {
                Console.WriteLine("An error occured and the outing could not be added.");
                AnyKey();
            }

        }


        //Helper Methods
        private void AnyKey()
        {
            Console.WriteLine("Press any key to continue....");
            Console.ReadKey();
        }

        private void SeedContent()
        {
            Outing golf = new Outing(1, EventType.Golf, 4, new DateTime(2021, 6, 12), 212.33m);
            Outing bowling = new Outing(2, EventType.Bowling, 8, new DateTime(2021, 6, 24), 415.64m);
            Outing concert = new Outing(3, EventType.Concert, 12, new DateTime(2021, 7, 12), 600.13m);
            Outing golf2 = new Outing(4, EventType.Golf, 6, new DateTime(2021, 8, 12), 344.98m);

            _repo.AddOutingToDirectory(golf);
            _repo.AddOutingToDirectory(bowling);
            _repo.AddOutingToDirectory(concert);
            _repo.AddOutingToDirectory(golf2);

        }
    }
}
