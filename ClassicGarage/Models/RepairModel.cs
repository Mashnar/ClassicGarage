using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace ClassicGarage.Models
{
    [Table("Repair")]
    public class RepairModel
    {

        public int ID { get; set; }
        
        public int? CarID { get; set; }
        [DisplayName("Nazwa")]
        public string Name { get; set; }
        [DisplayName("Opis")]
        public string Description { get; set; }
        [DisplayName("Cena")]
        public int Cost { get; set; }
        
        public virtual CarModel Car { get; set; }
        
        public virtual ICollection<PartsModel> Part { get; set; }
    }
}