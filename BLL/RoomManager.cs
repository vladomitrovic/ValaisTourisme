using DAL;
using DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL
{
    public class RoomManager
    {
        public static List<Room> GetRoomsByDateAndLocation(DateTime beginDate, DateTime endDate, string location)
        {
            return RoomDB.GetRoomAvalaibleLocation(beginDate, endDate, location);
        }
        public static List<Room> GetRoomsByIdHotel(int IdHotel)
        {
            return RoomDB.GetRoomForHotel(IdHotel);
        }
        public static List<Room> GetRoomsByDateHotelAndNbPerson(DateTime beginDate, DateTime endDate, int IdHotel, int NbPerson)
        {
            return RoomDB.GetRoomsByDateHotelAndNbPerson(beginDate, endDate, IdHotel, NbPerson);
        }
        public static List<Room> GetRoomsByIdBooking(int IdBooking)
        {
            return RoomDB.GetRoomsByIdBooking(IdBooking);
        }

    }
}
