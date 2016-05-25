using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace CatForum.Models
{
    public class PostType
    {
        [Key]
        public int Id { get; set; }
        public string Name { get; set; }

        [InverseProperty("Type")]
        public virtual ICollection<PostDetail> Posts { get; set; }
    }
}