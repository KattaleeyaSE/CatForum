using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace CatForum.Models
{
    public class PostDetail
    {
        [Key]
        public int Id { get; set; }
        public Nullable<int> PostId { get; set; }
        public int CatId { get; set; }
        public int AddressId { get; set; }
        public int TypeId { get; set; }
        public string Picture{ get; set; }
        public int Status { get; set; }
        public string Condition { get; set; }
        public string Contact { get; set; }
        public Nullable<double> Fee { get; set; }
        public string Location { get; set; }

        [ForeignKey("PostId")]
        public virtual Post Post { get; set; }

        [ForeignKey("CatId")]
        public virtual Cat Cat { get; set; }

        [ForeignKey("AddressId")]
        public virtual Address Address { get; set; }

        [ForeignKey("TypeId")]
        public virtual PostType Type { get; set; }
    }
}