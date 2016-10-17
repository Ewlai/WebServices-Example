using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
// added...
using Lab1.Models;
using AutoMapper;

namespace Lab1.Controllers
{
    public class Manager
    {
        private ApplicationDbContext ds = new ApplicationDbContext();

        public SmartphoneBase AddSmartphone(SmartphoneAdd newItem) {

            if (newItem == null)
            {
                return null;
            }
            else
            {
                Smartphone addedItem = Mapper.Map<Smartphone>(newItem);

                ds.Smartphones.Add(addedItem);
                ds.SaveChanges();

                return Mapper.Map<SmartphoneBase>(addedItem);
            }

        }

        public SmartphoneBase GetSmartphoneById(int id)
        {
            var fetchedObject = ds.Smartphones.Find(id);

            return (fetchedObject == null) ? null : Mapper.Map<SmartphoneBase>(fetchedObject);
        }

        public IEnumerable<SmartphoneBase> AllSmartphones()
        {
            var fetchedObjects = ds.Smartphones.OrderBy(fn => fn.Manufacturer).ThenBy(gn => gn.SmartphoneModel);

            return Mapper.Map<IEnumerable<SmartphoneBase>>(fetchedObjects);

        }

    }
}