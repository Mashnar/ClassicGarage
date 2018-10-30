using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace ClassicGarage.Models
{
    public class NoticeModel
    {
        public int ID { get; set; }
        public int CarID { get; set; }
        public bool Active { get; set; }
        [Required]
        public virtual CarModel Car { get; set; }
    }
}