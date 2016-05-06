using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BellRingers
{
    class Member
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string TowerName { get; set; }
        public bool IsCaptain { get; set; }
        public DateTime MemberSince { get; set; }
        public List<String> Methods;
    }
}
