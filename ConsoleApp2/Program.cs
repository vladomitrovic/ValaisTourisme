using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DTO;
using BLL;


namespace ConsoleApp2
{
    class Program
    {
        static void Main(string[] args)
        {

            List<Room> rooms = BLL.HotelManager.GetRoomForLocation("Sierre");

            foreach ( Room r in rooms)
            {
                Console.WriteLine(r.IdRoom);

                Console.ReadLine();
            }

        }
    }
}
