using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace CatForum.Forms
{
    public class PostForm
    {
        public string PostTitle { get; set; }
        public int PostType { get; set; }
        public HttpPostedFileBase File { get; set; }
        public int Province { get; set; }
        public int Amphur { get; set; }
        public int Tumbon { get; set; }
        public string PostDetail { get; set; }
        public int PostStatus { get; set; }
        public string Condition { get; set; }
        public string Location { get; set; }
        public string Contact { get; set; }
        public double Fee { get; set; }
        public string CatName { get; set; }
        public int CatAge { get; set; }
        public int LifeStage { get; set; }
        public int Gender { get; set; }
        public int CatEyes { get; set; }
        public int CatCoat { get; set; }
        public int CatPattern { get; set; }
        public int CatTail { get; set; }
        public int CatBreed { get; set; }
        public string FoodLike { get; set; }
        public string FoodDislike { get; set; }
        public string Habit { get; set; }
        public string Hate { get; set; }
        public string Vaccine { get; set; }
        public string CatDescription { get; set; }
        public int CatStatus { get; set; }

        public PostForm()
        {
            this.PostTitle = null;
            this.PostType = 0;
            this.File = null;
            this.Province = 0;
            this.Amphur = 0;
            this.Tumbon = 0;
            this.PostDetail = null;
            this.PostStatus = 0;
            this.Condition = null;
            this.Location = null;
            this.Contact = null;
            this.Fee = 0;
            this.CatName = null;
            this.CatAge = 0;
            this.LifeStage = 0;
            this.Gender = 0;
            this.CatEyes = 0;
            this.CatCoat = 0;
            this.CatPattern = 0;
            this.CatTail = 0;
            this.CatBreed = 0;
            this.FoodLike = null;
            this.FoodDislike = null;
            this.Habit = null;
            this.Hate = null;
            this.Vaccine = null;
            this.CatDescription = null;
            this.CatStatus = 0;
        }
    }
}
