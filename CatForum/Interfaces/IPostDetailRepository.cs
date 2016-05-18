using CatForum.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CatForum.Interfaces
{
    public interface IPostDetailRepository
    {
        IEnumerable<PostDetail> SelectAll();
        PostDetail SelectById(int id);
        void Add(PostDetail obj);
        void Update(PostDetail obj);
        void Delete(int? id);
        void Save();
    }
}