using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace CatForum.Models
{
    public class Post
    {
        [Key]
        public int Id { get; set; }
        public int UserId { get; set; }
        public string Name { get; set; }
        public string Content { get; set; }
        public DateTime Created { get; set; }
        public DateTime Updated { get; set; }

        [ForeignKey("UserId")]
        public virtual User Owner { get; set; }

        [InverseProperty("Post")]
        public virtual ICollection<PostDetail> Details { get; set; }

        [InverseProperty("Post")]
        public virtual ICollection<Picture> Pictures { get; set; }

        [InverseProperty("Post")]
        public virtual ICollection<Report> Reports { get; set; }
    }
}