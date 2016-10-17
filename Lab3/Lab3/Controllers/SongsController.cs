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
    public class SongsController : ApiController
    {
        // Reference to the worker object
        private Worker w;
        private Manager m;

        public SongsController()
        {
            w = new Worker();
            m = new Manager(w);
        }

        // Get all use case (DOES NOT include Artist data)
        // GET api/Songs
        public IHttpActionResult Get()
        {
            return Ok(w.Songs.GetAll());
        }

        // Get all use case (includes Artist data)
        // GET: api/Songs/WithArtist
        [Route("api/Songs/withartist")]
        public IHttpActionResult GetWithArtist()
        {
            return Ok(w.Songs.GetAllWithArtist());
        }

        // Get one use case (DOES NOT include associated data)
        // GET api/Songs/5
        public IHttpActionResult Get(int? id)
        {
            // Fetch the object
            var fetchedObject = w.Songs.GetById(id.GetValueOrDefault());

            return (fetchedObject == null) ? NotFound() : (IHttpActionResult)Ok(fetchedObject);
        }

        // Get one use case (includes associated data)
        // GET: api/Songs/5/WithData
        [Route("api/Songs/{id}/withdata")]
        public IHttpActionResult GetWithData(int? id)
        {
            // Fetch the object
            var fetchedObject = w.Songs.GetByIdWithData(id.GetValueOrDefault());

            return (fetchedObject == null) ? NotFound() : (IHttpActionResult)Ok(fetchedObject);
        }

        // Add new use case
        // POST api/Songs
        public IHttpActionResult Post([FromBody]SongAdd newItem)
        {
            // Ensure that the URI is clean (and does not have an id parameter)
            if (Request.GetRouteData().Values["id"] != null) { return BadRequest("Invalid request URI"); }

            // Ensure that a "newItem" is in the entity body
            if (newItem == null) { return BadRequest("Must send an entity body with the request"); }

            // Ensure that we can use the incoming data
            if (!ModelState.IsValid) { return BadRequest(ModelState); }

            // Attempt to add the new object
            var addedItem = w.Songs.Add(newItem);

            // Continue?
            if (addedItem == null) { return BadRequest("Cannot add the object"); }

            // HTTP 201 with the new object in the entity body
            // Notice how to create the URI for the Location header
            var uri = Url.Link("DefaultApi", new { id = addedItem.Id });
            return Created<SongBase>(uri, addedItem);
        }

        /*
        // PUT api/songs/5
        public void Put(int id, [FromBody]string value)
        {
        }
        */

        // Delete one use case
        // DELETE api/Songs/5
        public void Delete(int id)
        {
            w.Songs.DeleteExisting(id);
        }
    }
}
