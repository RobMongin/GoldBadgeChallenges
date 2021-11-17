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
        private OutingsRepo _repo;
        [TestInitialize]
        public void Arrange()
        {
            _outing = new Outing(1, EventType.Golf, 4, new DateTime(2021, 6, 12), 212.33m);
            _repo = new OutingsRepo();
            _repo.AddOutingToDirectory(_outing);
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
    }
}
