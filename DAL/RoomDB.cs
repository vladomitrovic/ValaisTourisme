using DTO;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL
{
    public class RoomDB
    {

        static string connectionString = "Data Source=153.109.124.35;Initial Catalog=Hotel_Mitrovic;Persist Security Info=True;User ID=6231db;Password=Pwd46231.";

        public static List<Room> GetAllRoom()
        {
            List<Room> results = null;

            try
            {
                using (SqlConnection cn = new SqlConnection(connectionString))
                {
                    string query = "Select * from Room;";
                    SqlCommand cmd = new SqlCommand(query, cn);

                    cn.Open();

                    using (SqlDataReader dr = cmd.ExecuteReader())
                    {
                        while (dr.Read())
                        {
                            if (results == null)
                                results = new List<Room>();

                            Room room = new Room();

                            room.IdRoom = (int)dr["IdRoom"];
                            room.Number = (int)dr["Number"];
                            room.Description = (string)dr["Description"];
                            room.Type = (int)dr["Type"];
                            room.Price = (decimal)dr["Price"];
                            room.HasTV = (bool)dr["HasTV"];
                            room.HasHairDryer = (bool)dr["HasHairDryer"];

                            results.Add(room);

                        }
                    }
                }
            }
            catch (Exception e)
            {
                throw e;
            }

            return results;
        }

        public static List<Room> GetRoomForHotel(int IdHotel)
        {
            List<Room> results = null;

            try
            {
                using (SqlConnection cn = new SqlConnection(connectionString))
                {
                    string query = "select * from room where IdHotel = @IdHotel";
                    SqlCommand cmd = new SqlCommand(query, cn);
                    cmd.Parameters.AddWithValue("@IdHotel", IdHotel);

                    cn.Open();

                    using (SqlDataReader dr = cmd.ExecuteReader())
                    {
                        while (dr.Read())
                        {
                            if (results == null)
                                results = new List<Room>();

                            Room room = new Room();

                            room.IdRoom = (int)dr["IdRoom"];
                            room.Number = (int)dr["Number"];
                            room.Description = (string)dr["Description"];
                            room.Type = (int)dr["Type"];
                            room.Price = (decimal)dr["Price"];
                            room.HasTV = (bool)dr["HasTV"];
                            room.HasHairDryer = (bool)dr["HasHairDryer"];
                            room.IdHotel = HotelDB.GetHotelById((int)dr["IdHotel"]);
                            room.Pictures = PictureDB.GetPicturesByIdRoom((int)dr["IdRoom"]);

                            results.Add(room);

                        }
                    }
                }
            }
            catch (Exception e)
            {
                throw e;
            }

            return results;
        }

        public static List<Room> GetRoomAvalaibleLocation(DateTime CheckIN, DateTime CheckOUT, String Location)
        {
            List<Room> results = new List<Room>();
            List<Picture> pictures = new List<Picture>();

            try
            {
                using (SqlConnection cn = new SqlConnection(connectionString))
                {
                    string query =
                        "SELECT R.* FROM ROOM R " +
                        "LEFT OUTER JOIN Booking_has_Room BR ON R.IdRoom = BR.IdRoom " +
                        "LEFT OUTER JOIN BOOKING B ON B.IdBooking = BR.IdBooking " +
                        "INNER JOIN HOTEL H ON R.idHotel = H.idHotel " +
                        "WHERE @Location = H.Location " +
                        "AND (B.Checkin IS NULL " +
                        "OR(@CheckIN < B.Checkin AND @CheckOUT <= B.Checkin) " +
                        "OR(@CheckIN >= B.Checkout AND @CheckOUT > B.Checkout));";
                    SqlCommand cmd = new SqlCommand(query, cn);
                    cmd.Parameters.AddWithValue("@Location", Location);
                    cmd.Parameters.AddWithValue("@CheckIN", CheckIN);
                    cmd.Parameters.AddWithValue("@CheckOUT", CheckOUT);

                    cn.Open();

                    using (SqlDataReader dr = cmd.ExecuteReader())
                    {
                        while (dr.Read())
                        {
                    

                            Room room = new Room();

                            room.IdRoom = (int)dr["IdRoom"];
                            room.Number = (int)dr["Number"];
                            room.Description = (string)dr["Description"];
                            room.Type = (int)dr["Type"];
                            room.Price = (decimal)dr["Price"];
                            room.HasTV = (bool)dr["HasTV"];
                            room.HasHairDryer = (bool)dr["HasHairDryer"];
                            room.IdHotel = HotelDB.GetHotelById((int)dr["IdHotel"]);
                            room.Pictures = PictureDB.GetPicturesByIdRoom((int)dr["IdRoom"]);



                            results.Add(room);

                        }
                    }
                }
            }
            catch (Exception e)
            {
                throw e;
            }

            return results;
        }
        
        public static List<Room> GetRoomsByIdBooking(int IdBooking)
        {
            List<Room> rooms = new List<Room>();

            try
            {
                using (SqlConnection cn = new SqlConnection(connectionString))
                {
                    string query = "SELECT * FROM Room R " +
                        "INNER JOIN Booking_has_Room BR ON BR.IdRoom = R.IdRoom " +
                        "WHERE IdBooking = @IdBooking; ";
                    SqlCommand cmd = new SqlCommand(query, cn);
                    cmd.Parameters.AddWithValue("@IdBooking", IdBooking);

                    cn.Open();

                    using (SqlDataReader dr = cmd.ExecuteReader())
                    {
                        while (dr.Read())
                        {
                            Room room = new Room();


                            room.IdRoom = (int)dr["IdRoom"];
                            room.Number = (int)dr["Number"];
                            room.Description = (string)dr["Description"];
                            room.Type = (int)dr["Type"];
                            room.Price = (decimal)dr["Price"];
                            room.HasTV = (bool)dr["HasTV"];
                            room.HasHairDryer = (bool)dr["HasHairDryer"];
                            room.IdHotel = HotelDB.GetHotelById((int)dr["IdHotel"]);
                            room.Pictures = PictureDB.GetPicturesByIdRoom((int)dr["IdRoom"]);


                            rooms.Add(room);
                        }
                    }
                }
            }
            catch (Exception e)
            {
                throw e;
            }

            return rooms;
        }

        public static List<Room> GetRoomsByDateHotelAndNbPerson(DateTime checkin, DateTime checkout, int idHotel, int nbPerson)
        {
            List<Room> rooms = new List<Room>();

            try
            {
                using (SqlConnection cn = new SqlConnection(connectionString))
                {
                    string query = "SELECT R.* FROM ROOM R " +
                    "LEFT OUTER JOIN Booking_has_Room BR ON R.IdRoom = BR.IdRoom " +
                    "LEFT OUTER JOIN BOOKING B ON B.IdBooking = BR.IdBooking " +
                    "INNER JOIN HOTEL H ON R.idHotel = H.idHotel " +
                    "WHERE @idHotel = H.IdHotel " +
                        "AND(B.checkin IS NULL " +
                        "OR(@checkin < B.BeginDate AND @checkout <= B.checkin) " +
                        "OR(@checkin >= B.checkout AND @checkout > B.checkout)) " +
                    "ORDER BY R.Type DESC; ";
                    SqlCommand cmd = new SqlCommand(query, cn);
                    cmd.Parameters.AddWithValue("@idHotel", idHotel);
                    cmd.Parameters.AddWithValue("@checkin", checkin);
                    cmd.Parameters.AddWithValue("@checkout", checkout);
                    cmd.Parameters.AddWithValue("@nbPerson", nbPerson);
                    cn.Open();

                    using (SqlDataReader dr = cmd.ExecuteReader())
                    {
                        while (dr.Read())
                        {
                            Room room = new Room();


                                room.IdRoom = (int)dr["IdRoom"];
                                room.Number = (int)dr["Number"];
                            room.Description = (string)dr["Description"];
                            room.Type = (int)dr["Type"];
                            room.HasTV = (bool)dr["HasTV"];
                            room.HasHairDryer = (bool)dr["HasHairDryer"];
                            room.IdHotel = HotelDB.GetHotelfromId((int)dr["IdHotel"]);
                            room.Pictures = PictureDB.GetPicturesByIdRoom((int)dr["IdRoom"]);
                            room.Price = (decimal)dr["Price"];


                           
                       

                            rooms.Add(room);
                        }
                    }
                }
            }
            catch (Exception e)
            {
                throw e;
            }

            return rooms;
        }


    }
}
