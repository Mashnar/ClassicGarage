using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ClassicGarage.Models
{
    public class RepairModel
    {

        public int ID { get; set; }
        public int CarID { get; set; }
        
        public string Name { get; set; }
        public string Description { get; set; }
        public int Cost { get; set; }
        public virtual CarModel Car { get; set; }
        public virtual ICollection<PartsModel> Part { get; set; }
    }
}