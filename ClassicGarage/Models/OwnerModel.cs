using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace ClassicGarage.Models
{
    [Table("Owner")]
    public class OwnerModel
    {
        public int ID { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Phone { get; set; }
        [DisplayName("Mail")]
        public string EMail { get; set; }
      
        public virtual ICollection<CarModel> Cars { get; set; }
    }
}
