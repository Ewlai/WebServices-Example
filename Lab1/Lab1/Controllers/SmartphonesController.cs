using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace Lab1.Controllers
{
    public class SmartphonesController : ApiController
    {
        private Manager m = new Manager();

        // GET: api/Smartphones
        public IHttpActionResult Get()
        {
            return Ok(m.AllSmartphones());
        }

        // GET: api/Smartphones/5
        public IHttpActionResult Get(int id)
        {
            var fetchedObject = m.GetSmartphoneById(id);

            if (fetchedObject == null)
            {
                return NotFound();
            }
            else
            {
                return Ok(fetchedObject);
            }
        }

        // POST: api/Smartphones
        public IHttpActionResult Post([FromBody]SmartphoneAdd newItem)
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
                var addedItem = m.AddSmartphone(newItem);

                if (addedItem == null)
                {
                    return BadRequest("Cannot add the object");
                }
                else
                {
                    var uri = Url.Link("DefaultApi", new { id = addedItem.Id });
                    return Created<SmartphoneBase>(uri, addedItem);
                }
            }
            else
            {
                return BadRequest(ModelState);
            }

        }


        /*
        // PUT: api/Smartphones/5
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE: api/Smartphones/5
        public void Delete(int id)
        {
        }
        */
    }
}
