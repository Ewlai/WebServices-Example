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
    public class Song_repo : Repository<Song>
    {
        // Constructor 
        public Song_repo(ApplicationDbContext ds) : base(ds) { }

        public IEnumerable<SongBase> GetAll()
        {
            // Call the base class method
            var fetchedObjects = RGetAll(null);

            return Mapper.Map<IEnumerable<SongBase>>(fetchedObjects.OrderBy(a => a.SongName));
        }

        public IEnumerable<SongSelf> GetAllWithArtist()
        {
            // Call the base class method, ask for Artist
            var fetchedObjects = RGetAll(new[] { "Artist" });

            return Mapper.Map<IEnumerable<SongSelf>>(fetchedObjects);
        }

        public SongBase GetById(int id)
        {
            // Call the base class method
            var fetchedObject = RGetById(i => i.Id == id, null);

            return (fetchedObject == null) ? null : Mapper.Map<SongBase>(fetchedObject);
        }

        public SongSelf GetByIdWithData(int id)
        {
            // Call the base class method, ask for associated data
            var fetchedObject = RGetById(i => i.Id == id, new[] { "Artist", "Albums" });

            return (fetchedObject == null) ? null : Mapper.Map<SongSelf>(fetchedObject);
        }

        public SongBase Add(SongAdd newItem)
        {
            // Add the new object
            var addedItem = RAdd(Mapper.Map<Song>(newItem));

            // Return the object
            return Mapper.Map<SongBase>(addedItem);
        }

    }
}