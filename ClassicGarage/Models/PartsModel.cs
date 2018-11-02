using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace ClassicGarage.Models
{
    [Table("Parts")]
    public class PartsModel
    {
     
        public int ID { get; set; }
       
       // public int? CarID { get; set; }
       
        public int? RepairID { get; set; }
        public string Name { get; set; }
        
        public int Cost_Buy { get; set; }
        public int? Cost_Sell { get; set; }
        public DateTime Buy_Date { get; set; }
        public DateTime? Sell_Date { get; set; }
        
     //   public virtual CarModel Car { get; set; }
        public virtual RepairModel Repair { get;set; }

    }
}