using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTO
{
    public class Booking
    {
        public int IdBooking { get; set; }
        public string Firstname { get; set; }
        public string Lastname { get; set; }
        public DateTime CheckIN { get; set; }
        public DateTime CheckOUT { get; set; }
        public int NbPerson { get; set; }

    }
}
