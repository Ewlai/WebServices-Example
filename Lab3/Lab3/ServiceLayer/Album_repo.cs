using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
// new...
using Lab3.Models;
using Lab3.Controllers;
using AutoMapper;


namespace Lab3.ServiceLayer
{
    public class Album_repo : Repository<Album>
    {
        // Constructor 
        public Album_repo(ApplicationDbContext ds) : base(ds) { }

        public IEnumerable<AlbumBase> GetAll()
        {
            // Call the base class method
            var fetchedObjects = RGetAll(null);

            return Mapper.Map<IEnumerable<AlbumBase>>(fetchedObjects.OrderBy(a => a.AlbumName));
        }

        public IEnumerable<AlbumSelf> GetAllWithArtist()
        {
            // Call the base class method, ask for Artist
            var fetchedObjects = RGetAll(new[] { "Artist" });

            return Mapper.Map<IEnumerable<AlbumSelf>>(fetchedObjects);
        }

        public AlbumBase GetById(int id)
        {
            // Call the base class method
            var fetchedObject = RGetById(i => i.Id == id, null);

            return (fetchedObject == null) ? null : Mapper.Map<AlbumBase>(fetchedObject);
        }

        public AlbumSelf GetByIdWithData(int id)
        {
            // Call the base class method, ask for associated data
            var fetchedObject = RGetById(i => i.Id == id, new[] { "Artist", "Songs" });

            return (fetchedObject == null) ? null : Mapper.Map<AlbumSelf>(fetchedObject);
        }

        public AlbumBase Add(AlbumAdd newItem)
        {
            // Add the new object
            var addedItem = RAdd(Mapper.Map<Album>(newItem));

            // Return the object
            return Mapper.Map<AlbumBase>(addedItem);
        }

        // This one method services both PUT use cases
        // Both controller methods call this
        public void SetAlbumSong(AlbumSong item)
        {
            // Get a reference to the Album
            var album = _ds.Albums.Find(item.Album);
            if (album == null) { return; }

            // Get a reference to the Song
            var song = _ds.Songs.Find(item.Song);
            if (song == null) { return; }

            // Make the changes, save, and exit
            album.Songs.Add(song);
            song.Albums.Add(album);
            _ds.SaveChanges();
        }

        // This one method services both PUT use cases
        // Both controller methods call this

        public void RemoveAlbumSong(AlbumSong item)
        {
            // Get a reference to the Album
            var album = _ds.Albums.Find(item.Album);
            if (album == null) { return; }

            // Get a reference to the Song
            var song = _ds.Songs.Find(item.Song);
            if (song == null) { return; }

            // Make the changes, save, and exit
            album.Songs.Remove(song);
            song.Albums.Remove(album);
            _ds.SaveChanges();
        }

    }
}