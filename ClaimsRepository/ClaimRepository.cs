using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClaimsRepository
{
    public class ClaimRepository
    {
        //Had to research Queue quite a bit. 
        //I like queue for this challenge because of the built in methods that allow the user to access claims in a true queue fashion 
        //Queue operates as first in first out and methods provide an easy way to move objects around in the queue (top/bottom of queue)
        //examples in my code Enqueue, Dequeue, and Peek
        //Main Source of information: https://docs.microsoft.com/en-us/dotnet/api/system.collections.generic.queue-1?view=net-5.0 and https://www.go4expert.com/articles/lists-queues-stacks-sets-c-sharp-t30028/
        private Queue<Claim> _claimQueue = new Queue<Claim>();

        //Create
        public bool AddNewClaim(Claim newClaim)
        {
            int startingCount = _claimQueue.Count;
            _claimQueue.Enqueue(newClaim);

            bool wasAdded = (_claimQueue.Count > startingCount) ? true : false;
            return wasAdded;
        }
        //Read
        //Get All
        public Queue<Claim> GetAllClaims()
        {
            return _claimQueue;
        }
        //Get or "Peek" next claim
        public Claim GetNextClaim()
        {
            if (_claimQueue.Count == 0)
            {
                return null;
            }
            else
                return _claimQueue.Peek();
        }

        //user cannot update a claim. only add remove.

        //Delete
        public bool DeleteClaimFromQueue()
        {
            int startingCount = _claimQueue.Count;
            _claimQueue.Dequeue();

            bool wasAdded = (_claimQueue.Count < startingCount) ? true : false;
            return wasAdded;
        }
    }
}
