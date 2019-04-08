﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarShop.Model
{
    public class About: BaseEntity
    {
       
        [Display(Name = "Hakkımızda Sayfa Fotoğrafı")]                
        public String AboutPagePhoto { get; set; }
        [Display(Name = "Hakkımızda Sayfa Başlığı")]
        public string AboutPageHeader { get; set; }
        [Display(Name = "Hakkımızda Sayfa Açıklaması")]
        public string AboutPageDescription { get; set; }

    }
}
