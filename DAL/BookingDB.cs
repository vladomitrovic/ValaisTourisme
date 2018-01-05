using DTO;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL
{
   public class BookingDB
    {

        static string connectionString = "Data Source=153.109.124.35;Initial Catalog=Hotel_Mitrovic;Persist Security Info=True;User ID=6231db;Password=Pwd46231.";

        public static List<Booking> GetAllBookings()
        {
            List<Booking> results = new List<Booking>();

            try
            {
                using (SqlConnection cn = new SqlConnection(connectionString))
                {
                    string query = "Select * from Booking";
                    SqlCommand cmd = new SqlCommand(query, cn);

                    cn.Open();

                    using (SqlDataReader dr = cmd.ExecuteReader())
                    {
                        while (dr.Read())
                        {
                            Booking booking = new Booking();

                            if (dr["IdBooking"] != null)
                                booking.IdBooking = (int)dr["IdBooking"];

                            if (dr["Firstname"] != null)
                                booking.Firstname = (string)dr["Firstname"];

                            if (dr["Lastname"] != null)
                                booking.Lastname = (string)dr["Lastname"];

                            if (dr["Checkin"] != null)
                                booking.CheckIN = (DateTime)dr["Checkin"];

                            if (dr["Checkout"] != null)
                                booking.CheckOUT = (DateTime)dr["Checkout"];

                            if (dr["NbPerson"] != null)
                                booking.NbPerson = (int)dr["NbPerson"];

                            results.Add(booking);
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

        public static void AddBooking(string Firstname, string Lastname, List<Room> Rooms, DateTime Checkin, DateTime Checkout, int NbPerson)
        {
            int id = 0;

            try
            {
                using (SqlConnection cn = new SqlConnection(connectionString))
                {
                    string query = "INSERT INTO Booking(Firstname, Lastname, Checkin, Checkout, NbPerson) OUTPUT INSERTED.IdBooking VALUES(@BookNumber, @Firstname, @Lastname, @checkin, @Checkout, @NbPerson)";
                    SqlCommand cmd = new SqlCommand(query, cn);
                    cmd.Parameters.AddWithValue("@Firstname", Firstname);
                    cmd.Parameters.AddWithValue("@Lastname", Lastname);
                    cmd.Parameters.AddWithValue("@Checkin", Checkin);
                    cmd.Parameters.AddWithValue("@Checkout", Checkout);
                    cmd.Parameters.AddWithValue("@NbPerson", NbPerson);

                    cn.Open();

                    id = (int)cmd.ExecuteScalar();

                    cn.Close();

                    foreach (var room in Rooms)
                    {
                        AddRoomBooking(room.IdRoom, id);
                    }
                }
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public static void AddRoomBooking(int IdRoom, int IdBooking)
        {
            try
            {
                using (SqlConnection cn = new SqlConnection(connectionString))
                {
                    string query = "INSERT INTO Booking_has_Room(IdRoom, IdBooking) VALUES(@IdRoom, @IdBooking)";
                    SqlCommand cmd = new SqlCommand(query, cn);
                    cmd.Parameters.AddWithValue("@IdRoom", IdRoom);
                    cmd.Parameters.AddWithValue("@IdBooking", IdBooking);

                    cn.Open();

                    cmd.ExecuteNonQuery();

                    cn.Close();
                }
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public static List<Booking> GetBooking(string Firstname, string Lastname)
        {
            List<Booking> results = new List<Booking>();

            try
            {
                using (SqlConnection cn = new SqlConnection(connectionString))
                {
                    string query = "SELECT * FROM Booking WHERE Firstname = @Firstname AND Lastname = @Lastname";
                    SqlCommand cmd = new SqlCommand(query, cn);
                    cmd.Parameters.AddWithValue("@Firstname", Firstname);
                    cmd.Parameters.AddWithValue("@Lastname", Lastname);

                    cn.Open();

                    using (SqlDataReader dr = cmd.ExecuteReader())
                    {
                        while (dr.Read())
                        {
                            Booking booking = new Booking();

                            if (dr["IdBooking"] != null)
                                booking.IdBooking = (int)dr["IdBooking"];

                            if (dr["Firstname"] != null)
                                booking.Firstname = (string)dr["Firstname"];

                            if (dr["Lastname"] != null)
                                booking.Lastname = (string)dr["Lastname"];

                            if (dr["Checkin"] != null)
                                booking.CheckIN = (DateTime)dr["Checkin"];

                            if (dr["Checkout"] != null)
                                booking.CheckOUT = (DateTime)dr["Checkout"];

                            if (dr["NbPerson"] != null)
                                booking.NbPerson = (int)dr["NbPerson"];

                            results.Add(booking);
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

        public static void DeleteBooking(int IdBooking)
        {
            try
            {
                using (SqlConnection cn = new SqlConnection(connectionString))
                {
                    string query = "DELETE FROM Booking WHERE IdBooking = @IdBooking";
                    SqlCommand cmd = new SqlCommand(query, cn);
                    cmd.Parameters.AddWithValue("@IdBooking", IdBooking);

                    cn.Open();

                    DeleteRoomBookings(IdBooking);

                    cmd.ExecuteNonQuery();

                    cn.Close();
                }
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public static void DeleteRoomBookings(int IdBooking)
        {
            try
            {
                using (SqlConnection cn = new SqlConnection(connectionString))
                {
                    string query = "DELETE FROM Booking_has_Room WHERE IdBooking = @IdBooking";
                    SqlCommand cmd = new SqlCommand(query, cn);
                    cmd.Parameters.AddWithValue("@IdBooking", IdBooking);

                    cn.Open();

                    cmd.ExecuteNonQuery();

                    cn.Close();
                }
            }
            catch (Exception e)
            {
                throw e;
            }
        }

    }
}
