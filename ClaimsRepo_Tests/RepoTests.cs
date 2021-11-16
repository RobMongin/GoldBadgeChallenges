using ClaimsRepository;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;

namespace ClaimsRepo_Tests
{
    [TestClass]
    public class RepoTests
    {
        //Arrange
        private ClaimRepository _repo;
        private Claim _claim;

        [TestInitialize]
        public void Arrange()
        {
            _claim = new Claim(1, ClaimType.Home, "Hail Damage", 1500.00m, new DateTime(2021, 10, 22), new DateTime(2021, 11, 01));
            _repo = new ClaimRepository();
            _repo.AddNewClaim(_claim);
        }

        [TestMethod]
        public void AddClaim_ShouldReturnTrue()
        {
            bool addResult = _repo.AddNewClaim(_claim);
            Assert.IsTrue(addResult);
        }

        [TestMethod]
        public void GetAllClaims_ShouldReturnTrue()
        {  
            Queue<Claim> repo = _repo.GetAllClaims();
            bool hasClaim = repo.Contains(_claim);
            Assert.IsTrue(hasClaim);
        }

        [TestMethod]
        public void GetNextClaim_ShouldReturnCorrectClaim()
        {
            Claim nextClaim = _repo.GetNextClaim();
            Assert.AreEqual(nextClaim, _claim);
        }

        [TestMethod]
        public void DeleteClaim_ShouldReturnTrue()
        {
            Assert.IsTrue(_repo.DeleteClaimFromQueue());
        }
    }
}
