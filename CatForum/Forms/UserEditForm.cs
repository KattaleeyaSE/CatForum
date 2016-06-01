using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace CatForum.Forms
{
    public class UserEditForm
    {
        public string Username { get; set; }
        public string Password { get; set; }
        public string RePassword { get; set; }
        public string Email { get; set; }
        public int Gender { get; set; }
        public string DisplayName { get; set; }
        public HttpPostedFileBase File { get; set; }
        public int Province { get; set; }
        public int Amphur { get; set; }
        public int Tumbon { get; set; }
    }
}
