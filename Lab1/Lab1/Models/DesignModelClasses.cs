using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace Lab1.Models
{

    public class Smartphone {
        public int Id { get; set; }

        [Required, StringLength(50)]
        public string Manufacturer { get; set; }

        [Required, StringLength(50)]
        public string SmartphoneModel { get; set; }

        public DateTime ReleaseDate { get; set; }

        [Required]
        public double ScreenSize { get; set; }
        [Required]
        public int SellPrice { get; set; }

        public Smartphone()
        {
            this.ReleaseDate = DateTime.Now;

        }

    }
   
}