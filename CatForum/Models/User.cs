using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace CatForum.Models
{
    public class User
    {
        [Key]
        public int Id { get; set; }
        public int AddressId { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public string Email { get; set; }
        public string DisplayName { get; set; }
        public int Gender { get; set; }
        public DateTime BirthDate { get; set; }
        public string Image { get; set; }

        [ForeignKey("AddressId")]
        public virtual Address Address { get; set; }

        [InverseProperty("Owner")]
        public virtual ICollection<Post> Posts { get; set; }

        [InverseProperty("User")]
        public virtual ICollection<Follow> Follows { get; set; }

    }
}