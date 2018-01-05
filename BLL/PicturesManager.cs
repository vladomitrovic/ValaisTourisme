using DAL;
using DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL
{
    public class PicturesManager
    {

        public static List<Picture> GetPicturesByIdRoom(int IdRoom)
        {
            return PictureDB.GetPicturesByIdRoom(IdRoom);
        }

    }
}
