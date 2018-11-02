using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace ClassicGarage.Models
{
    [Table("Notice")]
    public class NoticeModel
    {
        [Key]
        public int ID { get; set; }
        
        public int? CarID { get; set; }
        public string Description { get; set; }
        public bool Active { get; set; }
        [Required]
        public virtual CarModel Car { get; set; }

        //cos tu nie gra XD
    }
}