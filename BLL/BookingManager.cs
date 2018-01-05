using DAL;
using DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL
{
    public class BookingManager
    {
        public static List<Booking> GetAllBooking()
        {
            return BookingDB.GetAllBookings();
        }
        public static void AddBooking(string Firstname, string Lastname, List<Room> Rooms, DateTime BeginDate, DateTime EndDate, int NbPerson)
        {
            BookingDB.AddBooking(Firstname, Lastname, Rooms, BeginDate, EndDate, NbPerson);
        }
        public static List<Booking> GetBooking(string Firstname, string Lastname)
        {
            return BookingDB.GetBooking(Firstname, Lastname);
        }
        public static void DeleteBooking(int IdBooking)
        {
            BookingDB.DeleteBooking(IdBooking);
        }


    }
}
