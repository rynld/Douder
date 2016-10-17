using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Douder.Models
{
    public class Message
    {
        public int ID { get; set; }

        [Required]
        public string Name { get; set; }

        [Required]
        public string Info { get; set; }

        public float CoordX { get; set; }

        public float CoordY { get; set; }

        public ApplicationUser User { get; set; }

        public string UserId { get; set; }
    }

    public class Coordinate
    {
        public float CoordX { get; set;}

        public float CoordY { get; set; }
    }
}
