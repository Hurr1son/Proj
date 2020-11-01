using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace WebSite.Models
{
    public class Car
    {
        public int Id { get; set; }
        public string UserId { get; set; }
        [Required]
        public string Brand { get; set; }
        [Required]
        public string Model { get; set; }
        [Required]
        public int Year { get; set; }
        [Required]
        public string EngineType { get; set; }
        //[Required]
        //public float EngineAmount { get; set; }
        //[Required]
        //public string GearBoxType { get; set; }
        //[Required]
        //public int TankAmount { get; set; }
        //[Required]
        //public bool IsCarHasDC { get; set; }
    }
}