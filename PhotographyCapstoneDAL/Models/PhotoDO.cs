using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PhotographyCapstoneDAL.Models
{
    public class PhotoDO
    {
        public Int32 PhotoID { get; set; }
        public string PhotoName { get; set; }
        public Int32 Height { get; set; }
        public Int32 Width { get; set; }
        public string ExtensionType { get; set; }
        public int Size { get; set; }
        public DateTime DateCreated { get; set; }
        public string Photo { get; set; }
        public Byte?[] Byte { get; set; }
        public Int32 AlbumID { get; set; }
        public string AlbumName { get; set; }
    }
}
