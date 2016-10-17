using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
// new...
using Lab4.ServiceLayer;

namespace Lab4.Controllers
{
    public class AlbumsController : ApiController
    {
        // Marie - Now-standard controller pattern
        // Private instance variables, and constructor

        // Reference to the worker object
        private Worker w;
        private Manager m;

        public AlbumsController()
        {
            w = new Worker();
            m = new Manager(w);
        }

        // GET: api/Albums
        public IHttpActionResult Get()
        {
            return Ok(w.Albums.GetAll());
        }

        // GET: api/Albums/WithData
        [Route("api/albums/withdata")]
        public IHttpActionResult GetWithData()
        {
            return Ok(w.Albums.GetAllWithData());
        }

        // GET: api/Albums/5
        public IHttpActionResult Get(int? id)
        {
            // Fetch the object
            var fetchedObject = w.Albums.GetById(id.GetValueOrDefault());

            return (fetchedObject == null) ? NotFound() : (IHttpActionResult)Ok(fetchedObject);
        }

        // GET: api/Albums/5/WithData
        [Route("api/albums/{id}/withdata")]
        public IHttpActionResult GetWithData(int? id)
        {
            // Fetch the object
            var fetchedObject = w.Albums.GetByIdWithData(id.GetValueOrDefault());

            return (fetchedObject == null) ? NotFound() : (IHttpActionResult)Ok(fetchedObject);
        }

        // POST: api/Albums
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

        /*
        // PUT: api/Albums/5
        public void Put(int id, [FromBody]string value)
        {
        }
        */

        // DELETE: api/Albums/5
        public void Delete(int id)
        {
            // In a controller 'Delete' method, a void return type will
            // automatically generate a HTTP 204 "No content" response
            w.Albums.DeleteExisting(id);
        }

    }

}
