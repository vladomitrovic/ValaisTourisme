using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTO
{
    class Booking_has_Room
    {
        public Room idRoom { get; set; }
        public Booking IdBooking { get; set; }
        public Decimal Price { get; set; }

    }
}
