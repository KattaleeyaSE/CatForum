using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace CatForum.Models
{
    public class PostDetail
    {
        [Key]
        public int Id { get; set; }
        public int PostId { get; set; }
        public int AddressId { get; set; }
        public int TypeId { get; set; }
        public string Picture{ get; set; }
        public int Status { get; set; }
        public string Condition { get; set; }
        public string Contact { get; set; }
    }
}