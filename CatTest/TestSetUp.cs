using CatForum.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CatTest
{
    class TestSetUp
    {
        public List<User> users { get; set; }
        public void initUser()
        {
            this.users = new List<User>();
            for (int i = 0; i < 5; i++)
            {
                var user = new User();
                user.Id = i + 1;
                user.Username = "User" + (i + 1);
                user.Password = "Pass" + (i + 1);
                this.users.Add(user);
            }
        }
    }
}
