using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace CatForum.Models
{
    public class CatEyes
    {

        [Key]
        public int Id { get; set; }
        public string Color { get; set; }

        [InverseProperty("Eyes")]
        public virtual ICollection<Cat> Cats { get; set; }
    }
}