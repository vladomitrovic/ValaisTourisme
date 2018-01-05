using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DTO;
using System.Data.SqlClient;


namespace DAL
{
    public class HotelDB
    {
        static string connectionString = "Data Source=153.109.124.35;Initial Catalog=Hotel_Mitrovic;Persist Security Info=True;User ID=6231db;Password=Pwd46231.";

        public static List<Hotel> GetAllHotel()
        {
            List<Hotel> results = null;  

            try
            {
                using (SqlConnection cn = new SqlConnection(connectionString))
                {
                    string query = "Select * from Hotel;";
                    SqlCommand cmd = new SqlCommand(query, cn);

                    cn.Open();

                    using (SqlDataReader dr = cmd.ExecuteReader())
                    {
                        while (dr.Read())
                        {
                            if (results == null)
                                results = new List<Hotel>();

                            Hotel hotel = new Hotel();

                             hotel.IdHotel = (int)dr["IdHotel"];
                             hotel.Name = (string)dr["Name"];
                             hotel.Description = (string)dr["Description"];
                             hotel.Location = (string)dr["Location"];
                             hotel.Category = (int)dr["Category"];
                             hotel.HasWifi = (bool)dr["HasWifi"];
                             hotel.HasParking = (bool)dr["HasParking"];
                             hotel.Phone = (string)dr["Phone"];
                             hotel.Email = (string)dr["Email"];
                             hotel.Website = (string)dr["Website"];



                            //hotel.Rooms = GetRooms(hotel.IdHotel);



                            results.Add(hotel);

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

   

        public static Hotel GetHotelfromId(int IdHotel)
        {
            Hotel hotel = new Hotel();

            try
            {
                using (SqlConnection cn = new SqlConnection(connectionString))
                {
                    string query = "Select * from Hotel where idHotel = @IdHotel";
                    SqlCommand cmd = new SqlCommand(query, cn);
                    cmd.Parameters.AddWithValue("@IdHotel", IdHotel);

                    cn.Open();

                    using (SqlDataReader dr = cmd.ExecuteReader())
                    {
                        if (dr.Read())
                        {
                            hotel = new Hotel
                            {
                                IdHotel = (int)dr["IdHotel"]
                            };

                            if (dr["Name"] != null)
                                hotel.Name = (string)dr["Name"];

                            if (dr["Description"] != null)
                                hotel.Description = (string)dr["Description"];

                            if (dr["Location"] != null)
                                hotel.Location = (string)dr["Location"];

                            if (dr["Category"] != null)
                                hotel.Category = (int)dr["Category"];

                            if (dr["HasWifi"] != null)
                                hotel.HasWifi = (bool)dr["HasWifi"];

                            if (dr["HasParking"] != null)
                                hotel.HasParking = (bool)dr["HasParking"];

                            if (dr["Phone"] != null)
                                hotel.Phone = (string)dr["Phone"];

                            if (dr["Email"] != null)
                                hotel.Email = (string)dr["Email"];

                            if (dr["Website"] != null)
                                hotel.Website = (string)dr["Website"];
                        }
                    }
                }
            }
            catch (Exception e)
            {
                throw e;
            }

            return hotel;
        }

        public static List<Hotel> GetHotelsByDateLocationAndNbPerson(DateTime Checkin, DateTime Checkout, string location, int nbPerson, int minCategory, int maxCategory, bool? hasWifi, bool? hasParking)
        {
            List<Hotel> hotels = new List<Hotel>();

            try
            {
                using (SqlConnection cn = new SqlConnection(connectionString))
                {
                    string wifiQuery = "AND HasWifi = @hasWifi ";
                    string parkingQuery = "AND HasParking = @hasParking ";

                    if (hasWifi == null)
                        wifiQuery = "";
                    if (hasParking == null)
                        parkingQuery = "";

                    string query = "SELECT DISTINCT(H.Name), H.* FROM ROOM R " +
                        "LEFT OUTER JOIN Booking_has_Room BR ON R.IdRoom = BR.IdRoom " +
                        "LEFT OUTER JOIN BOOKING B ON B.IdBooking = BR.IdBooking " +
                        "INNER JOIN HOTEL H ON R.idHotel = H.idHotel " +
                        "WHERE H.IdHotel IN " +
                                "(SELECT IdHotel FROM Hotel " +
                                "WHERE Location = @location " +
                                "AND (Category BETWEEN @minCategory AND @maxCategory) " +
                                wifiQuery +
                                parkingQuery + ") " +
                            "AND @nbPerson <= " +
                                "(SELECT SUM(R2.Type) FROM ROOM R2 " +
                                "LEFT OUTER JOIN Booking_has_Room BR2 ON R2.IdRoom = BR2.IdRoom " +
                                "LEFT OUTER JOIN BOOKING B2 ON B2.IdBooking = BR2.IdBooking " +
                                "INNER JOIN HOTEL H2 ON R2.idHotel = H2.idHotel " +
                                "AND H2.idHotel = H.idHotel " +
                                "AND (B2.BeginDate IS NULL " +
                                "OR(@Checkin < B2.Checkin AND @Checkout <= B2.Checkin) " +
                                "OR(@Checkin >= B2.Checkout AND @Checkout > B2.Checkout))) " +
                            "AND(B.Checkin IS NULL " +
                            "OR(@Checkin < B.Checkin AND @Checkout <= B.Checkin) " +
                            "OR(@Checkin >= B.EndDate AND @Checkout > B.Checkout)) " +
                        "ORDER BY H.Name;";
                    SqlCommand cmd = new SqlCommand(query, cn);
                    cmd.Parameters.AddWithValue("@Checkin", Checkin);
                    cmd.Parameters.AddWithValue("@Checkout", Checkout);
                    cmd.Parameters.AddWithValue("@location", location);
                    cmd.Parameters.AddWithValue("@nbPerson", nbPerson);
                    cmd.Parameters.AddWithValue("@minCategory", minCategory);
                    cmd.Parameters.AddWithValue("@maxCategory", maxCategory);
                    if (hasWifi != null)
                        cmd.Parameters.AddWithValue("@hasWifi", hasWifi);
                    if (hasParking != null)
                        cmd.Parameters.AddWithValue("@hasParking", hasParking);

                    cn.Open();

                    using (SqlDataReader dr = cmd.ExecuteReader())
                    {
                        while (dr.Read())
                        {
                            Hotel hotel = new Hotel
                            {
                                IdHotel = (int)dr["IdHotel"]
                            };

                            if (dr["Name"] != null)
                                hotel.Name = (string)dr["Name"];

                            if (dr["Description"] != null)
                                hotel.Description = (string)dr["Description"];

                            if (dr["Location"] != null)
                                hotel.Location = (string)dr["Location"];

                            if (dr["Category"] != null)
                                hotel.Category = (int)dr["Category"];

                            if (dr["HasWifi"] != null)
                                hotel.HasWifi = (bool)dr["HasWifi"];

                            if (dr["HasParking"] != null)
                                hotel.HasParking = (bool)dr["HasParking"];

                            if (dr["Phone"] != null)
                                hotel.Phone = (string)dr["Phone"];

                            if (dr["Email"] != null)
                                hotel.Email = (string)dr["Email"];

                            if (dr["Website"] != null)
                                hotel.Website = (string)dr["Website"];

                            hotels.Add(hotel);
                        }
                    }
                }
            }
            catch (Exception e)
            {
                throw e;
            }

            return hotels;
        }


    }
}
