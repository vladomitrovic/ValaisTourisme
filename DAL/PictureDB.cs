using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DTO;
using System.Data.SqlClient;

namespace DAL
{
    public class PictureDB
    {

        static string connectionString = "Data Source=153.109.124.35;Initial Catalog=Hotel_Mitrovic;Persist Security Info=True;User ID=6231db;Password=Pwd46231.";

        public static List<Picture> GetPicturesByIdRoom(int IdRoom)
        {
            List<Picture> pictures = new List<Picture>();

            try
            {
                using (SqlConnection cn = new SqlConnection(connectionString))
                {
                    string query = "SELECT * FROM Picture WHERE IdRoom = @IdRoom";
                    SqlCommand cmd = new SqlCommand(query, cn);
                    cmd.Parameters.AddWithValue("@IdRoom", IdRoom);
                    cn.Open();

                    using (SqlDataReader dr = cmd.ExecuteReader())
                    {
                        while (dr.Read())
                        {
                            Picture picture = new Picture();


                            picture.IdPicture = (int)dr["IdPicture"];
                            picture.IdRoom = (int)dr["IdRoom"];
                            picture.Url = (string)dr["Url"];
                            

                            pictures.Add(picture);
                        }
                    }
                }
            }
            catch (Exception e)
            {
                throw e;
            }

            return pictures;
        }
    }
}

