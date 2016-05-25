﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace CatForum.Models
{
    public class Report
    {
        [Key]
        public int Id { get; set; }
        public int PostId { get; set; }
        public string Text { get; set; }
        public DateTime Created { get; set; }

        [ForeignKey("PostId")]
        public virtual Post Post { get; set; }

    }
}