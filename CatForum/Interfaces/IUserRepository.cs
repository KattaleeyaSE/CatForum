using CatForum.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CatForum.Interfaces
{
    public interface IUserRepository
    {
        IEnumerable<User> SelectAll();
        User SelectById(int id);
        void Add(User obj);
        void Update(User obj);
        void Delete(int? id);
        void Save();
    }
}