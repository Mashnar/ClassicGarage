﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
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
        [DisplayName("Auto")]
        public int CarID { get; set; }
        [DisplayName("Opis")]
        public string Description { get; set; }
        [DisplayName("Czy aktywne")]
        public bool Active { get; set; }
        
        public virtual CarModel Car { get; set; }

        //cos tu nie gra XD
    }
}