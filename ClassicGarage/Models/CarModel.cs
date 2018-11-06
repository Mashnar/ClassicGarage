using System;
using System.Collections.Generic;
using System.ComponentModel;
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
        [DisplayName("Marka")]
        public string Brand { get; set; }
        [DisplayName("Model")]
        public string Model { get; set; }
        [DisplayName("Rok Produkcji")]
        public int Year { get; set; }

        public string VIN { get; set; }
        [DisplayName("Seria")]
        public int Series { get; set; }
        [DisplayName("Obraz")]
        public string Photo { get; set; }
        [DisplayName("Data zakupu")]
        public DateTime Buy_Date { get; set; }
        [DisplayName("Data sprzedaży")]
        public DateTime? Sell_Date { get; set; }
        [DisplayName("Cena zakupu")]
        public int Buy_Cost { get; set; }
        [DisplayName("Cena sprzedaży")]
        public int? Sell_Cost { get; set; }
        
       
        public int? OwnerID { get; set; }
        
        public virtual OwnerModel Owner { get; set; }

        //public virtual ICollection<PartsModel> Part { get; set; }
        
    }
}