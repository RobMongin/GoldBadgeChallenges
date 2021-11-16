using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CafeRepository
{
    public class MenuRepository
    {
        //need a list for the methods to use (C.R.U.D)
        private List<Menu> _menuDirectory = new List<Menu>();

        //Create
        public bool AddMenuItem(Menu newItem)
        {
            int startingCount = _menuDirectory.Count;
            _menuDirectory.Add(newItem);

            bool wasAdded = (_menuDirectory.Count > startingCount) ? true : false;
            return wasAdded;

        }

        //Read
        //Get All
        public List<Menu> GetMenu()
        {
            return _menuDirectory;
        }

        //Get Individual
        public Menu GetMenuByNumber(int mealNumber)
        {
            foreach(Menu meal in _menuDirectory)
            {
                if(meal.MealNumber == mealNumber)
                {
                    return meal;
                }
            }
            return null;
        }

        //Update
        public bool UdateMenuByNumber(int oldNumber, Menu meal)
        {
            Menu oldMeal = GetMenuByNumber(oldNumber);

            if (oldMeal != null)
            {
                oldMeal.MealNumber = meal.MealNumber;
                oldMeal.MealName = meal.MealName;
                oldMeal.Description = meal.Description;
                oldMeal.Ingredients = meal.Ingredients;
                oldMeal.Price = meal.Price;

                return true;
            }
            else
                return false;
        }

        //Delete
        public bool DeleteMenuItem(Menu existingMeal)
        {
            bool deleteResult = _menuDirectory.Remove(existingMeal);
            return deleteResult;
        }

        //Delete by menu number
        public bool DeleteMenuItemByNumber(int mealNumber)
        {
            return DeleteMenuItem(GetMenuByNumber(mealNumber));
        }
    }
}
