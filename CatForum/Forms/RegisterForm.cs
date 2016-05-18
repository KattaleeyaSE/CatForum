using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CatForum.Forms
{
    public class RegisterForm
    {
        public string Username { get; set; }
        public string Password { get; set; }
        public string RePassword { get; set; }
        public string Email { get; set; }
        public string ReEmail { get; set; }
        public int Province { get; set; }
        public int Amphur { get; set; }
        public int Tumbon { get; set; }
    }
}