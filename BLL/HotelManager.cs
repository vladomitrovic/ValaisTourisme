using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DTO;
using DAL;

namespace BLL
{
    public class HotelManager
    {
        public static Hotel GetHotelById(int id)
        {
            return HotelDB.GetHotelfromId(id);
        }

        public static List<Hotel> GetAllHotel()
        {
            return HotelDB.GetAllHotel();
        }

        public static List<Hotel> GetHotelsByDateLocationAndNbPerson(DateTime beginDate, DateTime endDate, string location, int nbPerson, int minCategory, int maxCategory, bool? hasWifi = null, bool? hasParking = null)
        {
            return HotelDB.GetHotelsByDateLocationAndNbPerson(beginDate, endDate, location, nbPerson, minCategory, maxCategory, hasWifi, hasParking);
        }

    }
}
