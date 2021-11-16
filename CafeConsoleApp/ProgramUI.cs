using CafeRepository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CafeConsoleApp
{
    public class ProgramUI
    {
        private readonly MenuRepository _menuRepo = new MenuRepository();
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
                    "1. View All Menu Items\n" +
                    "2. View Menu Item By Meal Number\n" +
                    "3. Create New Menu Items\n" +
                    "4. Delete Existing Meal Item\n" +
                    "5. Exit");
                string input = Console.ReadLine();

                switch (input.ToLower())
                {
                    case "1":
                        ShowAllMenuItems();
                        break;
                    case "2":
                        ShowMenuItemByNumber();
                        break;
                    case "3":
                        CreateNewMenuItem();
                        break;
                    case "4":
                        DeleteItemByNumber();
                        break;
                    case "5":
                    case "exit":
                        continueToRun = false;
                        break;
                    default:
                        Console.WriteLine("Please enter a valid number");
                        break;

                }
                Console.Clear();
            }
        }

        private void ShowAllMenuItems()
        {
            Console.Clear();
            List<Menu> listOfItems = _menuRepo.GetMenu();
            
            foreach(Menu item in listOfItems)
            {
                DisplayContent(item);
            }
            AnyKey();
        }

        public void ShowMenuItemByNumber()
        {
            Console.Clear();
            bool checkInputIsValid = true;
            int desiredSearch = 0;
            while (checkInputIsValid)
            {
                Console.WriteLine("Enter a Meal Number: ");
                int userInput;
                bool isValidInt = Int32.TryParse(Console.ReadLine(), out userInput);
                if(isValidInt == true)
                {
                    desiredSearch = userInput;
                    checkInputIsValid = false;
                }
                else
                {
                    Console.WriteLine("Please enter a valid number: ");
                }
            }

            Menu item = _menuRepo.GetMenuByNumber(desiredSearch);

            if(item != null)
            {
                DisplayContent(item);
            }
            else
            {
                Console.WriteLine("Invalid Number, Item not found. ");
            }
            AnyKey();
        }

        private void CreateNewMenuItem()
        {
            Console.Clear();
            Menu newItem = new Menu();

            bool checkInputIsNumber = true;
            while (checkInputIsNumber)
            {
                Console.WriteLine("Enter the meal number: ");

                //TryParse returns bool if user's input can be assigned as int
                //out is used to pass userInput without initializing it
                //stackoverflow helped me a bit here
                int userInput;
                bool isValidNumber = Int32.TryParse(Console.ReadLine(), out userInput);
                if (isValidNumber == true)
                {
                    newItem.MealNumber = userInput;
                    checkInputIsNumber = false;
                }
                else
                {
                    Console.WriteLine("Please enter a valid number: ");
                }
            }
            Console.WriteLine("Enter the name of the meal: ");
            newItem.MealName = Console.ReadLine();
            Console.WriteLine("Enter a description: ");
            newItem.Description = Console.ReadLine();
            Console.WriteLine("Enter the ingredients: ");
            newItem.Ingredients = Console.ReadLine();

            //repeat above logic for price
            bool isValidPrice = true;
            while (isValidPrice)
            {
                Console.WriteLine("Enter the Price: ");
                decimal inputPrice;
                bool isValidDecimal = decimal.TryParse(Console.ReadLine(), out inputPrice);
                if (isValidDecimal == true)
                {
                    newItem.Price = inputPrice;
                    isValidPrice = false;
                }
                else
                {
                    Console.WriteLine("Please enter a valid price: ");
                }
            }

            //add newItem and display if it was added or not
            if (_menuRepo.AddMenuItem(newItem))
            {
                Console.WriteLine("Menu Item Added!");
                AnyKey();
            }
            else
            {
                Console.WriteLine("Menu Item was unable to be added.");
                AnyKey();
            }
        }

        public void DeleteItemByNumber()
        {
            Console.Clear();
            Console.WriteLine("Enter the Meal Number of the meal you'd like to delete: ");

            bool deleteResult = _menuRepo.DeleteMenuItemByNumber(Convert.ToInt32(Console.ReadLine()));
            if(deleteResult == true)
            {
                Console.WriteLine("Menu item deleted.");
            }
            else
            {
                Console.WriteLine("Menu number could not be found. Nothing deleted.");
            }
        }

        //Helper Methods because i'm lazy and I liked these in the notes
        private void DisplayContent(Menu item)
        {
            Console.WriteLine($"Meal Number: {item.MealNumber}\n" +
                $"Meal Name: {item.MealName}\n" +
                $"Description: {item.Description}\n" +
                $"Ingredients: {item.Ingredients}\n" +
                $"Price: {item.Price}\n");
        }

        private void AnyKey()
        {
            Console.WriteLine("Press any key to continue....");
            Console.ReadKey();
        }

        //seed content for testing 
        private void SeedContent()
        {
            Menu reuben = new Menu(1, "Rob's Famous Reuben", "Sandwhich, Chips, and Drink", " Smoked Cornbeef, Rye Bread, Thousand Island, and Sauerkraut", 11.99m);
            Menu bLT = new Menu(1, "BLT", "Chips and Drink included", "Bacon, Lettuce, and Tomato", 9.99m);

            _menuRepo.AddMenuItem(reuben);
            _menuRepo.AddMenuItem(bLT);
        }
    }
}
