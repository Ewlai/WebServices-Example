using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace Lab2.Controllers
{
    public class ArtistsController : ApiController
    {
        Manager m = new Manager();

        // GET api/artists
        public IHttpActionResult Get()
        {
            return Ok(m.GetAllArtists());
        }

        // GET api/artistWithMembers
        [Route("api/artistWithMembers")]
        [HttpGet]
        public IHttpActionResult GetWithMembers()
        {
            return Ok(m.GetAllArtistsWithMembers());
        }

        // GET api/artists/5
        public IHttpActionResult Get(int? id)
        {
            if (!id.HasValue) { return NotFound(); }

            var fetchedObject = m.GetArtistWithMembers(id.Value);

            if (fetchedObject == null)
            {
                return NotFound();
            }
            else
            {
                return Ok(fetchedObject);
            }
        }

        // POST api/artists
        public IHttpActionResult Post([FromBody]ArtistAdd newItem)
        {
            if (Request.GetRouteData().Values["id"] != null)
            {
                return BadRequest("Invalid request URI");
            }

            if (newItem == null)
            {
                return BadRequest("Must send an entity body with the request");
            }

            if (ModelState.IsValid)
            {
                var addedItem = m.AddArtist(newItem);

                if (addedItem == null)
                {
                    return BadRequest("Cannot add the object");
                }
                else
                {
                    var uri = Url.Link("DefaultApi", new { id = addedItem.Id });
                    return Created<ArtistBase>(uri, addedItem);

                }
            }
            else
            {
                return BadRequest(ModelState);
            }
        }

        // PUT api/artists/5/setMemberOf
        [Route("api/artists/setMemberOf")]
        public void PutSetMemberOf([FromBody]ArtistMembers item)
        {
            if (item == null) { return; }

            if(ModelState.IsValid)
            {
                m.SetMemberOf(item);
            }
            else
            {
                return;
            }
        }

        // PUT api/artists/5/setMembers
        [Route("api/artists/setMembers")]
        public void PutSetMembers([FromBody]ArtistMembers item)
        {
            if (item == null) { return; }

            if (ModelState.IsValid)
            {
                m.SetMembers(item);
            }
            else
            {
                return;
            }
        }

        // DELETE api/artists/5
        public void Delete(int id)
        {
            m.DeleteArtist(id);
        }
    }
}
