using DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ValaisTourisme.ViewModels
{
    public class BookingVM
    {
        public IEnumerable<Booking> Booking { get; set; }

    }
}