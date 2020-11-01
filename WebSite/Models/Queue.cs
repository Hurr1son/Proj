using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;



namespace WebSite.Models
{
    public class Queue
    {
        public int Id { get; set; }
        [ForeignKey("Car")]
        public int CarId { get; set; }
        public string DateTime { get; set; }
        public string Description { get; set; }
        public string Email { get; set; }
        public string CarStatus { get; set; }
        public Car Car { get; set; }

    }
}