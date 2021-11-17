using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BadgesRepository
{
    public class Badge
    {
        public Badge() { }
        public Badge(int badgeId, List<string> doorAccess)
        {
            BadgeId = badgeId;
            DoorAccess = doorAccess;
        }
        public int BadgeId { get; set; }
        public List<string> DoorAccess { get; set; }


    }
}

