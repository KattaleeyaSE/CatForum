using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace CatForum.Models
{
    public class Cat
    {
        [Key]
        public int Id { get; set; }
        public string Name { get; set; }
        public int Age { get; set; }
        public int LifeStage { get; set; }
        public int Gender { get; set; }
        public int Eyes { get; set; }
        public int Coat { get; set; }
        public int Pattern { get; set; }
        public int Tail { get; set; }
        public int Breed { get; set; }
        public string FoodLike { get; set; }
        public string FoodDislike { get; set; }
        public string Habit { get; set; }
        public string Hate { get; set; }
        public string Vaccine { get; set; }
        public string Description { get; set; }
        public int Status { get; set; }
    }
}