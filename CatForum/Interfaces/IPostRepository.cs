using CatForum.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CatForum.Interfaces
{
    public interface IPostRepository
    {
        IEnumerable<Post> SelectAll();
        Post SelectById(int id);
        void Add(Post obj);
        void Update(Post obj);
        void Delete(int? id);
        void Save();
    }
}