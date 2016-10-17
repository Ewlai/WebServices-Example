using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
// new...
using System.ComponentModel.DataAnnotations;

namespace Lab3.Controllers
{
    public class SongAdd
    {

        public SongAdd()
        {
            DateWritten = DateTime.Now.AddMonths(-6);
        }

        [Required, StringLength(100)]
        public string SongName { get; set; }
        public DateTime DateWritten { get; set; }

        [Required, StringLength(50)]
        public string Genre { get; set; }
    }

    public class SongBase : SongAdd
    {
        public int Id { get; set; }
    }

    // Resource model with associated objects
    public class SongSelf : SongBase
    {
        public ArtistBase Artist { get; set; }
        public ICollection<AlbumBase> Albums { get; set; }
    }

    // Resource model does not need to be repeated since handled in Album_vm.cs

    /*
    public class SongAlbum
    {
        public int Song { get; set; }
        public int Album { get; set; }
    }
     */
}