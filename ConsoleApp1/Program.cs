using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAL;
using DTO;



namespace ConsoleApp1
{
    class Program
    {
        static void Main(string[] args)
        {

            List<Room> rooms = DAL.RoomDB.GetAllRoom();

            foreach(Room room in rooms){

                System.Console.WriteLine(room.IdRoom);

                Console.ReadLine();

            }


        }
    }
}
