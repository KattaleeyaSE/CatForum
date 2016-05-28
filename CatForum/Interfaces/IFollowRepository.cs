using CatForum.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CatForum.Interfaces
{
    public interface IFollowRepository
    {
        IEnumerable<Follow> SelectAll();
        Follow SelectById(int id);
        Follow SearchByUserAndPost(int userId, int postId);
        void Add(Follow obj);
        void Update(Follow obj);
        void Delete(int? id);
        void Save();
    }
}