using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
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
        public int LifeStageId { get; set; }
        public int Gender { get; set; }
        public int EyesId { get; set; }
        public int CoatId { get; set; }
        public int PatternId { get; set; }
        public int TailId { get; set; }
        public int BreedId { get; set; }
        public string FoodLike { get; set; }
        public string FoodDislike { get; set; }
        public string Habit { get; set; }
        public string Hate { get; set; }
        public string Vaccine { get; set; }
        public string Description { get; set; }
        public int Status { get; set; }

        [ForeignKey("LifeStageId")]
        public virtual CatLifeStage LifeStage { get; set; }

        [ForeignKey("EyesId")]
        public virtual CatEyes Eyes { get; set; }

        [ForeignKey("CoatId")]
        public virtual CatCoat Coat { get; set; }

        [ForeignKey("PatternId")]
        public virtual CatPattern Pattern { get; set; }

        [ForeignKey("TailId")]
        public virtual CatTail Tail { get; set; }

        [ForeignKey("BreedId")]
        public virtual CatBreed Breed { get; set; }
    }
}