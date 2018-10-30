using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using ClassicGarage.Models;

namespace ClassicGarage.DAL
{
    public class GarageContext:DbContext
    {
        public GarageContext():base("name=ClassicGarage")
        {
        }
        public DbSet<CarModel> Car { get; set; }
        public DbSet<NoticeModel> Notice { get; set; }
        public DbSet<OwnerModel> Owner { get; set; }
        public DbSet<PartsModel> Parts { get; set; }
        public DbSet<RepairModel> Repair { get; set; }
        
                protected override void OnModelCreating(DbModelBuilder modelBuilder)

        {

            modelBuilder.Conventions.Remove<OneToManyCascadeDeleteConvention>();

        }
    }
}
    }
}
