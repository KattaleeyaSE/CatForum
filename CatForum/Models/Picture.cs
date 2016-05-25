using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CatForum.Models
{
    public class Picture
    {
        [Key]
        public int Id { get; set; }
        public string Filename { get; set; }
        public string ContentType { get; set; }
        public byte[] Content { get; set; }
        public int PostId { get; set; }

        [ForeignKey("PostId")]
        public virtual Post Post { get; set; }

    }
}
