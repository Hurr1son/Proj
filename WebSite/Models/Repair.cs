using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations.Schema;

namespace WebSite.Models
{
    public class Repair
    {
        public int Id { get; set; }
        [ForeignKey("Car")]
        public int CarId { get; set; }
        public DateTime DateTime { get; set; }
        public string RepairDesc { get; set; }
        public Car Car { get; set; }
    }
}