using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PhotographyCapstoneDAL.Models
{
    public class AlbumDO
    {
        public int AlbumID { get; set; }

        public string AlbumName { get; set; }

        public string AlbumType { get; set; }

        public long? UserID { get; set; }

        public int PhotoID { get; set; }

        public string Photo { get; set; }

        public string PhotoName { get; set; }
    }
}
