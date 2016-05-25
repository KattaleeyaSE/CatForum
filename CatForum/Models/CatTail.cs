using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace CatForum.Models
{
    public class CatTail
    {
        [Key]
        public int Id { get; set; }
        public string Appearance { get; set; }

        [InverseProperty("Tail")]
        public virtual ICollection<Cat> Cats { get; set; }
    }
}