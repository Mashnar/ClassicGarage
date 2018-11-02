using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace ClassicGarage.Models
{
    [Table("Car")]
    public class CarModel
    {
      
         
        public int ID {get;set;}
        public string Brand { get; set; }
        public string Model { get; set; }
        public int Year { get; set; }
        public string VIN { get; set; }
        public int Series { get; set; }
        public string Photo { get; set; }
        public DateTime Buy_Date { get; set; }
        public DateTime? Sell_Date { get; set; }
        public int Buy_Cost { get; set; }
        public int? Sell_Cost { get; set; }
        
       
        public int? OwnerID { get; set; }
        
        public virtual OwnerModel Owner { get; set; }

        //public virtual ICollection<PartsModel> Part { get; set; }
        public virtual NoticeModel Notice { get; set; }
    }
}