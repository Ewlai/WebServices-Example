using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
// new...
using Lab4.Models;
using Lab4.Controllers;
using AutoMapper;

namespace Lab4.ServiceLayer
{
    // Marie - Notice the inheritance; also, notice the constructor

    public class Album_repo : Repository<Album>
    {
        // Constructor
        public Album_repo(ApplicationDbContext ds) : base(ds) { }

        public IEnumerable<AlbumBase> GetAll()
        {
            // Call the base class method
            var fetchedObjects = RGetAll(null);

            // Notice that we can do the sort here if we want to
            return Mapper.Map<IEnumerable<AlbumBase>>(fetchedObjects.OrderBy(a => a.AlbumName));
        }

        public IEnumerable<AlbumFull> GetAllWithData()
        {
            // Call the base class method, ask for associated data
            var fetchedObjects = RGetAll(new[] { "Artist" });

            return Mapper.Map<IEnumerable<AlbumFull>>(fetchedObjects);
        }

        public AlbumBase GetById(int id)
        {
            // Call the base class method
            var fetchedObject = RGetById(i => i.Id == id, null);

            return (fetchedObject == null) ? null : Mapper.Map<AlbumBase>(fetchedObject);
        }

        public AlbumFull GetByIdWithData(int id)
        {
            // Call the base class method, ask for associated data
            var fetchedObject = RGetById(i => i.Id == id, new[] { "Artist" });

            return (fetchedObject == null) ? null : Mapper.Map<AlbumFull>(fetchedObject);
        }

        public AlbumFull Add(AlbumAdd newItem)
        {
            // Marie - We cannot use the base class RAdd() method,
            // because it does not handle the associated object use case
            // Therefore, we will write our own "add" method

            // Ensure that we can continue
            if (newItem == null) { return null; }

            // Must validate the Artist
            var associatedItem = _ds.Artists.Find(newItem.ArtistId);
            if (associatedItem == null) { return null; }

            // Add the new object
            Album addedItem = Mapper.Map<Album>(newItem);
            addedItem.Artist = associatedItem;

            _ds.Albums.Add(addedItem);
            _ds.SaveChanges();

            // Return the object
            return Mapper.Map<AlbumFull>(addedItem);
        }

    }
}
