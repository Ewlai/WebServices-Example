using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
// more...
using Lab2.Models;
using AutoMapper;

namespace Lab2.Controllers
{
    public class Manager
    {
        private ApplicationDbContext ds = new ApplicationDbContext();

        // Create new artist

        public ArtistBase AddArtist(ArtistAdd newItem)
        {
            if (newItem == null)
            {
                return null;
            }
            else
            {
                Artist addedItem = Mapper.Map<Artist>(newItem);

                ds.Artists.Add(addedItem);
                ds.SaveChanges();

                return Mapper.Map<ArtistBase>(addedItem);
            }
        }

        // Configure an artist to be a member of a group

        public void SetMemberOf(ArtistMembers item)
        {
            var individual = ds.Artists.Find(item.Individual);
            if (individual == null) { return; }

            var group = ds.Artists.Find(item.Group);
            if (group == null) { return; }

            individual.MemberOf = group;
            individual.MemberOfId = group.Id;
            ds.SaveChanges();

        }

        // Configure an artist with a new group member

        public void SetMembers(ArtistMembers item)
        {
            var group = ds.Artists.Include("Members").SingleOrDefault(e => e.Id == item.Group);
            if (group == null) { return; }

            var individual = ds.Artists.Find(item.Individual);
            if (individual == null) { return; }

            group.Members.Add(individual);
            ds.SaveChanges();
        }

        // All artists
        public IEnumerable<ArtistBase> GetAllArtists()
        {
            var fetchedObjects = ds.Artists.OrderBy(art => art.ArtistName);

            return Mapper.Map<IEnumerable<ArtistBase>>(fetchedObjects);
        }

        // All artists with group members

        public IEnumerable<ArtistWithAssociations> GetAllArtistsWithMembers()
        {
            var fetchedObjects = ds.Artists.Include("Members").OrderBy(art => art.ArtistName);

            return Mapper.Map<IEnumerable<ArtistWithAssociations>>(fetchedObjects);
        }

        // One Artist, showing group membership (if any)

        public ArtistWithAssociations GetArtistWithMembers(int id)
        {
            var fetchedObject = ds.Artists.Include("Members").SingleOrDefault(i => i.Id == id);

            return (fetchedObject == null) ? null : Mapper.Map<ArtistWithAssociations>(fetchedObject);
        }

        // Delete an artist
        public void DeleteArtist(int id)
        {
            var storedItem = ds.Artists.Find(id);

            if (storedItem == null)
            {

            }
            else
            {
                try
                {
                    ds.Artists.Remove(storedItem);
                    ds.SaveChanges();
                }
                catch (Exception) { }
            }
        }

    }
}