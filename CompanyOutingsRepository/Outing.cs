using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CompanyOutingsRepository
{
    public class Outing
    {
        public enum EventType { Golf = 1, Bowling, AmusmentPark, Concert }
        public Outing() { }

        public Outing(int outingId, EventType type, int attendees, DateTime date, decimal totalCost) 
        {
            OutingId = outingId;
            Type = type;
            Attendees = attendees;
            Date = date;
            TotalCost = totalCost;
        }

        public int OutingId { get; set; }
        public EventType Type { get; set; }
        public int Attendees { get; set; }
        public DateTime Date { get; set; }
        public decimal TotalCost { get; set; }
        public decimal TotalCostPerPerson
        {
            get
            {
                return TotalCost / Attendees;
            }
        }
    }
}
