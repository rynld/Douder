using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace Douder.Models
{
    public class DouderContext : IdentityDbContext<ApplicationUser>
    {
        public DouderContext()
            : base("DefaultConnection", throwIfV1Schema: false)
        {
        }

       

        public static DouderContext Create()
        {
            return new DouderContext();
        }

        public DbSet<Message> Message { get; set; }
    }
}