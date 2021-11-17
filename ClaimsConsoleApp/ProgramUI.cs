using ClaimsRepository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClaimsConsoleApp
{
    public class ProgramUI
    {
        private readonly ClaimRepository _claimRepo = new ClaimRepository();

        public void Run()
        {
            SeedContent();
            RunMenu();
        }

        public void RunMenu()
        {
            bool continueToRun = true;
            while (continueToRun)
            {
                Console.Clear();
                Console.WriteLine("Select a menu option\n" +
                   "1. See all claims\n" +
                   "2. Take care of next claim\n" +
                   "3. Enter a new claim\n" +
                   "4. Exit");

                string userInput = Console.ReadLine();

                switch (userInput)
                {
                    case "1":
                        SeeAllClaims();
                        break;
                    case "2":
                        TakeCareOfClaim();
                        break;
                    case "3":
                        CreateClaim();
                        break;
                    case "4":
                        continueToRun = false;
                        break;
                    default:
                        Console.WriteLine("Please enter a valid number: ");
                        AnyKey();
                        break;
                }
            }
        }

        private void SeeAllClaims()
        {
            Console.Clear();

            Queue<Claim> allClaims = _claimRepo.GetAllClaims();

            foreach (Claim claim in allClaims)
            {
                DisplayContent(claim);
                Console.WriteLine("\n");
            }
            AnyKey();
            if (allClaims.Count() == 0)
            {
                Console.WriteLine("There are no claims in the Queue");
                AnyKey();
            }
        }

        private void TakeCareOfClaim()
        {
            Console.Clear();
            Claim nextClaim = _claimRepo.GetNextClaim();

            if (nextClaim != null)
            {
                Console.WriteLine("Next claim in Queue: ");
                DisplayContent(nextClaim);
                AnyKey();
                Console.WriteLine("Do you want to deal with this claim now?");
                Console.WriteLine("Press 'y' to remove this claim from the Queue.");
                Console.WriteLine("Press 'n' to leave claim in Queue.");
                string userInput = Console.ReadLine().ToLower();
                if (userInput == "y")
                {
                    bool wasRemoved = _claimRepo.DeleteClaimFromQueue();
                    if (wasRemoved == true)
                    {
                        Console.WriteLine("The Claim has been removed from Queue. Good work.");
                        AnyKey();
                    }
                    else
                    {
                        Console.WriteLine("Unable to delete claim from Queue");
                    }
                }
                else if (userInput == "n")
                {
                    Console.WriteLine("The Claim was not removed from the Queue.");
                    AnyKey();
                }
                else
                {
                    Console.WriteLine("please enter either 'y' or 'n'");
                    AnyKey();
                }
            }
            else
            {
                Console.WriteLine("No claims left in Queue");
                AnyKey();
            }
        }

        private void CreateClaim()
        {
            Console.Clear();
            Claim newClaim = new Claim();
            //check that user input is an int like I did in cafeApp
            bool checkInputIsNumber = true;
            while (checkInputIsNumber)
            {
                Console.WriteLine("Please Enter the claim ID: ");
                int userInput;
                bool isValidNumber = Int32.TryParse(Console.ReadLine(), out userInput);
                if (isValidNumber == true)
                {
                    newClaim.ClaimId = userInput;
                    checkInputIsNumber = false;
                }
                else
                {
                    Console.WriteLine("Please enter a valid number: ");
                }
            }

            Console.WriteLine("Select a claim type:\n" +
            "1. Car\n" +
            "2. Home\n" +
            "3. Theft");
            string typeInput = Console.ReadLine();
            int typeId = int.Parse(typeInput);
            newClaim.ClaimType = (ClaimType)typeId;

            Console.WriteLine("Enter a description for the Claim");
            newClaim.Description = Console.ReadLine();

            //again check that user inputs valid decimal 
            bool isValidAmount = true;
            while (isValidAmount)
            {
                Console.WriteLine("Enter the claim amount: ");
                decimal inputAmount;
                bool isValidDecimal = decimal.TryParse(Console.ReadLine(), out inputAmount);
                if (isValidDecimal == true)
                {
                    newClaim.ClaimAmount = inputAmount;
                    isValidAmount = false;
                }
                else
                {
                    Console.WriteLine("Please enter a valid price: ");
                }
            }

            Console.WriteLine("Enter date of the incident (yyyy, mm, dd):");
            newClaim.DateOfIncident = Convert.ToDateTime(Console.ReadLine());

            Console.WriteLine("Enter date of the claim (yyyy, mm, dd):");
            newClaim.DateOfClaim = Convert.ToDateTime(Console.ReadLine());

            Console.WriteLine($"Claim is Valid : {newClaim.IsValid}");

            bool wasAdded = _claimRepo.AddNewClaim(newClaim);
            if (wasAdded == true)
            {
                Console.WriteLine("The claim was added to the queue!");
                AnyKey();
            }
            else
            {
                Console.WriteLine("An error occured. Claim not added. Please Try again.");
                AnyKey();
            }
        }

        //Helper Methods
        private void DisplayContent(Claim claim)
        {
            Console.WriteLine($"Claim ID: {claim.ClaimId}\n" +
                $"Claim Type: {claim.ClaimType}\n" +
                $"Description: {claim.Description}\n" +
                $"Claim Amount: {claim.ClaimAmount}\n" +
                $"Date of Incident: {claim.DateOfIncident}\n" +
                $"Date of Claim: {claim.DateOfClaim}\n" +
                $"Claim Valid: {claim.IsValid}");
        }
        private void AnyKey()
        {
            Console.WriteLine("Press any key to continue....");
            Console.ReadKey();
        }

        private void SeedContent()
        {
            Claim claimOne = new Claim(1, ClaimType.Home, "Hail Damage", 1500.00m, new DateTime(2021, 10, 22), new DateTime(2021, 11, 01));
            Claim claimTwo = new Claim(2, ClaimType.Car, "Fender Bender", 500.00m, new DateTime(2021, 10, 24), new DateTime(2021, 10, 25));

            _claimRepo.AddNewClaim(claimOne);
            _claimRepo.AddNewClaim(claimTwo);
        }
    }
}
