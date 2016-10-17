using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

using System.ComponentModel.DataAnnotations;

namespace Lab2.Models
{

    public class Artist
    {
        public Artist()
        {
            this.Members = new List<Artist>();
            this.Albums = new List<Album>();
            this.Songs = new List<Song>();
            this.BirthDate = DateTime.Now;
        }

        public int Id { get; set; }
        public string ArtistName { get; set; }
        public string ArtistType { get; set; }
        public DateTime BirthDate { get; set; }
        public string genre { get; set; }
        public int? MemberOfId { get; set; }
        public Artist MemberOf { get; set; }
        public ICollection<Artist> Members { get; set; }
        public ICollection<Album> Albums { get; set; }
        public ICollection<Song> Songs { get; set; }
    }

    public class Album
    {
        public Album(){
            this.Songs = new List<Song>();
            this.ReleaseDate = DateTime.Now;
        }

        public int Id { get; set; }
        public string AlbumName { get; set; }
        public DateTime ReleaseDate { get; set; }
        public string genre { get; set; }
        public double MSRP { get; set; }
        [Required]
        public Artist Artist { get; set; }
        public ICollection<Song> Songs { get; set; }

    }

    public class Song
    {
        public Song()
        {
            this.Albums = new List<Album>();
            this.ReleaseDate = DateTime.Now;
        }

        public int Id { get; set; }
        public string SongName { get; set; }
        public string ComposerName { get; set; }
        public DateTime ReleaseDate { get; set; }
        public string genre { get; set; }
        public Artist Artist { get; set; }
        public ICollection<Album> Albums { get; set; }

    }
}