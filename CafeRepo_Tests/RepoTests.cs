using CafeRepository;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;

namespace CafeRepo_Tests
{
    [TestClass]
    public class RepoTests
    {
     //Arrange
        private MenuRepository _repo;
        private Menu _meal;

        [TestInitialize]
        public void Arrange()
        {
            _repo = new MenuRepository();
            _meal = new Menu(1, "Rob's Famous Reuben", "Sandwhich, Chips, and Drink", " Smoked Cornbeef, Rye Bread, Thousand Island, and Sauerkraut", 11.99m);
            _repo.AddMenuItem(_meal);
        }

        [TestMethod]
        public void AddMenuItem_ShouldReturnTrue()
        {
            bool addResult = _repo.AddMenuItem(_meal);
            Assert.IsTrue(addResult);
        }

        [TestMethod]
        public void GetMenu_ShouldReturnDirectory()
        {
            //_repo.AddMenuItem(_meal);

            List<Menu> meals = _repo.GetMenu();

            bool menuHasMeals = meals.Contains(_meal);

            Assert.IsTrue(menuHasMeals);
        }

        [TestMethod]
        public void GetByNumber_ShouldGetCorrectMenuItem()
        {
            //_repo.AddMenuItem(_meal);
            Menu foundMeal = _repo.GetMenuByNumber(1);
            Assert.AreEqual(_meal, foundMeal);
        }

        [TestMethod]
        public void UpdateByNumber_ShouldReturnTrue()
        {
            //_repo.AddMenuItem(_meal);
            Menu newItem = new Menu(1, "BLT", "Chips and Drink included", "Bacon, Lettuce, and Tomato", 9.99m);

            bool updateResult = _repo.UdateMenuByNumber(1, newItem);
            Assert.IsTrue(updateResult);
        }

        [TestMethod]
        public void DeleteMenuItem_ShouldReturnTrue()
        {
            Menu meal = _repo.GetMenuByNumber(1);

            bool removeResult = _repo.DeleteMenuItem(meal);

            Assert.IsTrue(removeResult);
        }

        [TestMethod]
        public void DeleteByNumber_ShouldReturnTrue()
        {
            Assert.IsTrue(_repo.DeleteMenuItemByNumber(1));
        }
    }
}
