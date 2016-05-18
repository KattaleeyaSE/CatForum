using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace CatForum.Models
{
    public class Tumbon
    {
        [Key]
        public int Id { get; set; }
        public int ProvinceId { get; set; }
        public int AmphurId { get; set; }
        public string Name { get; set; }
    }
}