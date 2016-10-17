using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
// new...
using System.ComponentModel.DataAnnotations;

namespace Lab4.Controllers
{
    public class AlbumAdd
    {
        public AlbumAdd()
        {
            DateReleased = DateTime.Now.AddMonths(-1);
        }

        [Required, StringLength(100)]
        public string AlbumName { get; set; }

        public DateTime DateReleased { get; set; }

        [Required, StringLength(50)]
        public string Genre { get; set; }

        [Range(1,UInt32.MaxValue)]
        public int ArtistId { get; set; }
    }

    public class AlbumBase : AlbumAdd
    {
        public int Id { get; set; }
    }

    public class AlbumFull : AlbumBase
    {
        // I have decided that the [Required] attribute is not needed here
        // This resource model is for output only, not for 
        public ArtistBase Artist { get; set; }

    }

}