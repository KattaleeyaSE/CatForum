using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CatForum.Models
{
    public class Follow
    {
        [Key]
        public int Id { get; set; }
        public int UserId { get; set; }
        public int PostId { get; set; }

        [ForeignKey("UserId")]
        public virtual User User { get; set; }

        [ForeignKey("PostId")]
        public virtual PostDetail Post { get; set; }
    }
}
