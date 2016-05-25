﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace CatForum.Models
{
    public class CatLifeStage
    {

        [Key]
        public int Id { get; set; }
        public string Stage { get; set; }

        [InverseProperty("LifeStage")]
        public virtual ICollection<Cat> Cats { get; set; }
    }
}