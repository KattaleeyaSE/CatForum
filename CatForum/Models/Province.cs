using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace CatForum.Models
{
    public class Province
    {

        [Key]
        public int Id { get; set; }
        public string Name { get; set; }

        [JsonIgnore]
        [InverseProperty("Province")]
        public virtual ICollection<Address> Addresses { get; set; }
    }
}