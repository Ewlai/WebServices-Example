using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
// more...
using System.ComponentModel.DataAnnotations;

namespace Lab2.Controllers
{
    public class ArtistAdd
    {
        [Required, StringLength(50)]
        public string ArtistName { get; set; }
        public string ArtistType { get; set; }
        public DateTime BirthDate { get; set; }
        public string genre { get; set; }
    }

    public class ArtistBase : ArtistAdd
    {
        public int Id { get; set; }

    }

    public class ArtistWithAssociations : ArtistBase
    {
        public ArtistWithAssociations()
        {
            Members = new List<ArtistBase>();
        }

        public int? MemberOfId { get; set; }
        public ArtistBase MemberOf { get; set; }
        public ICollection<ArtistBase> Members { get; set; }
    }

    public class ArtistMembers
    {
        public int Individual { get; set; }
        public int Group { get; set; }
    }

}