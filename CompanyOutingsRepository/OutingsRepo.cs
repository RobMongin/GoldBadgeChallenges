using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static CompanyOutingsRepository.Outing;

namespace CompanyOutingsRepository
{
    public class OutingsRepo
    {
        protected readonly List<Outing> _outingDirectory = new List<Outing>();

        //Create
        public bool AddOutingToDirectory(Outing newOuting)
        {
            int startingCount = _outingDirectory.Count();
            _outingDirectory.Add(newOuting);

            bool wasAdded = (_outingDirectory.Count > startingCount) ? true : false;
            return wasAdded;
        }

        //Read
        public List<Outing> GetOutings()
        {
            return _outingDirectory;
        }



        //Thought these would work as helper methods in ProgramUI
        //I was wrong. Or maybe I could have and just did it wrong
        //This works. I'm not touching it.
        public decimal CalculateCostOfAllOutings()
        {
            decimal total = 0;
            foreach (Outing outing in _outingDirectory)
            {
                total += outing.TotalCost;
            }
            return total;
        }

        public decimal CalculateCostByType(EventType eventType)
        {
            decimal total = 0;
            foreach (Outing outing in _outingDirectory)
            {
                if (outing.Type == eventType)
                {
                    total += outing.TotalCost;
                }
            }
            return total;
        }
    }
}
