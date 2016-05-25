using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace CatForum.Models
{
    public class Amphur
    {

        [Key]
        public int Id { get; set; }
        public int ProvinceId { get; set; }
        public string Name { get; set; }

        [ForeignKey("ProvinceId")]
        public virtual Province Province { get; set; }

        [InverseProperty("Amphur")]
        public virtual ICollection<Address> Addresses { get; set; }
    }
}