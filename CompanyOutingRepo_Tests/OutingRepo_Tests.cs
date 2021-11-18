using CompanyOutingsRepository;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using static CompanyOutingsRepository.Outing;

namespace CompanyOutingRepo_Tests
{
    [TestClass]
    public class OutingRepo_Tests
    {
        //Arrange
        private Outing _outing;
        private Outing _outing2;
        private OutingsRepo _repo;
        [TestInitialize]
        public void Arrange()
        {
            _outing = new Outing(1, EventType.Golf, 4, new DateTime(2021, 06, 12), 100.00m);
            _outing2 = new Outing(2, EventType.Golf, 4, new DateTime(2021, 07, 12), 100.00m);
            _repo = new OutingsRepo();
            _repo.AddOutingToDirectory(_outing);
            _repo.AddOutingToDirectory(_outing2);
        }

        [TestMethod]
        public void AddOuting_ShouldReturnTrue()
        {
            bool wasAdded = _repo.AddOutingToDirectory(_outing);
            Assert.IsTrue(wasAdded);
        }

        [TestMethod]
        public void GetAllOutings_ShouldReturnDirectory()
        {
            List<Outing> outings = _repo.GetOutings();
            bool hasOutings = outings.Contains(_outing);
            Assert.IsTrue(hasOutings);
        }

        [TestMethod]
        public void GetOutingByID_ShouldReturnCorrectOuting()
        {
            Outing foundOuting = _repo.GetOutingById(1);
            Assert.AreEqual(_outing, foundOuting);
        }

        [TestMethod]
        public void UpdateOuting_ShouldReturnTrue()
        {
            bool updateResult = _repo.UpdateOuting(1, _outing);
            Assert.IsTrue(updateResult);
        }

        [TestMethod]
        public void CostOfAllOutings_ShouldReturnTotalCost()
        {
            decimal total = _repo.CalculateCostOfAllOutings();
            Assert.AreEqual(total, 200.00m);
        }

        [TestMethod]
        public void CostOfAllOutingsByType_ShouldReturnTotal()
        {
            decimal total = _repo.CalculateCostByType(EventType.Golf);
            Assert.AreEqual(total, 200.00m);
        }
    }
}
