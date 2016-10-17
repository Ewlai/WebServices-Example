using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
// new...
using Lab3.ServiceLayer;

namespace Lab3.Controllers
{
    public class AlbumsController : ApiController
    {
        // Reference to the worker object
        private Worker w;
        private Manager m;

        public AlbumsController()
        {
            w = new Worker();
            m = new Manager(w);
        }

        // Get all use case (DOES NOT include Artist data)
        // GET api/Albums
        public IHttpActionResult Get()
        {
            return Ok(w.Albums.GetAll());
        }

        // Get all use case (includes Artist data)
        // GET: api/Albums/WithArtist
        [Route("api/Albums/withartist")]
        public IHttpActionResult GetWithArtist()
        {
            return Ok(w.Albums.GetAllWithArtist());
        }

        // Get one use case (DOES NOT include associated data)
        // GET api/Albums/5
        public IHttpActionResult Get(int? id)
        {
            // Fetch the object
            var fetchedObject = w.Albums.GetById(id.GetValueOrDefault());

            return (fetchedObject == null) ? NotFound() : (IHttpActionResult)Ok(fetchedObject);
        }

        // Get one use case (includes associated data)
        // GET: api/Albums/5/WithData
        [Route("api/Albums/{id}/withdata")]
        public IHttpActionResult GetWithData(int? id)
        {
            // Fetch the object
            var fetchedObject = w.Albums.GetByIdWithData(id.GetValueOrDefault());

            return (fetchedObject == null) ? NotFound() : (IHttpActionResult)Ok(fetchedObject);
        }

        // Add new use case
        // POST api/Albums
        public IHttpActionResult Post([FromBody]AlbumAdd newItem)
        {
            // Ensure that the URI is clean (and does not have an id parameter)
            if (Request.GetRouteData().Values["id"] != null) { return BadRequest("Invalid request URI"); }

            // Ensure that a "newItem" is in the entity body
            if (newItem == null) { return BadRequest("Must send an entity body with the request"); }

            // Ensure that we can use the incoming data
            if (!ModelState.IsValid) { return BadRequest(ModelState); }

            // Attempt to add the new object
            var addedItem = w.Albums.Add(newItem);

            // Continue?
            if (addedItem == null) { return BadRequest("Cannot add the object"); }

            // HTTP 201 with the new object in the entity body
            // Notice how to create the URI for the Location header
            var uri = Url.Link("DefaultApi", new { id = addedItem.Id });
            return Created<AlbumBase>(uri, addedItem);
        }
        
        // Configure a song-album association use case
        // PUT: api/Albums/5/SetAlbumSong
        [Route("api/Albums/{id}/setalbumsong")]
        public void PutSetAlbumSong(int id, [FromBody]AlbumSong item)
        {
            // Ensure that an "editedItem" is in the entity body
            if (item == null) { return; }
            
            // Ensure that we can use the incoming data
            if (ModelState.IsValid) { w.Albums.SetAlbumSong(item); }
        }

        // Clear (remove) a song-album association use case
        // PUT: api/Albums/5/ClearAlbumSong
        [Route("api/Albums/{id}/clearalbumsong")]
        public void PutClearAlbumSong(int id, [FromBody]AlbumSong item)
        {
            // Ensure that an "editedItem" is in the entity body
            if (item == null) { return; }

            // Ensure that we can use the incoming data
            if (ModelState.IsValid) { w.Albums.RemoveAlbumSong(item); }
        }

        /*
        // PUT api/albums/5
        public void Put(int id, [FromBody]string value)
        {
        }
        */

        // Delete one use case
        // DELETE api/Albums/5
        public void Delete(int id)
        {
            w.Albums.DeleteExisting(id);
        }
    }
}
