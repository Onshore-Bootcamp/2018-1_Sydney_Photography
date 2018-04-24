using PhotographyCapstoneBLL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PhotographyCapstoneBLL
{
    public class PhotoAlbumBLO
    {
        public int PhotoInMostAlbums(List<PhotoAlbumBO> photoAlbum)
        {
            int commonPhotoID = photoAlbum.GroupBy(pa => pa.PhotoID)
                                .OrderByDescending(pa => pa.Count())
                                .Select(pa => pa.Key)
                                .First();

            return commonPhotoID;
        }
    }
}
