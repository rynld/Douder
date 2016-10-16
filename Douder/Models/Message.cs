using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Douder.Models
{
    public class Message
    {
        public int ID { get; set; }
        public string Name { get; set; }

        public string Info { get; set; }

        public float CoordX { get; set; }

        public float Coordy { get; set; }
    }
}