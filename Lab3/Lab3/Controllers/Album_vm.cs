using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
// new...
using System.ComponentModel.DataAnnotations;

namespace Lab3.Controllers
{
    public class AlbumAdd
    {

        public AlbumAdd()
        {
            // set default date time as it is best practice
            DateReleased = DateTime.Now.AddMonths(1);
        }

        [Required, StringLength(100)]
        public string AlbumName { get; set; }
        public DateTime DateReleased { get; set; }

        [Required, StringLength(50)]
        public string Genre { get; set; }
    }

    public class AlbumBase : AlbumAdd
    {
        public int Id { get; set; }

    }

    // Resource model with associated objects
    public class AlbumSelf : AlbumBase
    {
        public ArtistBase Artist { get; set; }
        public ICollection<SongBase> Songs { get; set; }

    }

    // Resource model that handles song-album associations
    public class AlbumSong
    {
        public int Album { get; set; }
        public int Song { get; set; }
    }

}