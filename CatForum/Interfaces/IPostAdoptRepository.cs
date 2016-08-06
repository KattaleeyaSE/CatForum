using CatForum.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CatForum.Interfaces
{
    public interface IPostAdoptRepository
    {
        IEnumerable<PostAdopt> SelectAll();
        IEnumerable<PostAdopt> SearchByUser(int userId);
        IEnumerable<PostAdopt> SearchByOwner(int userId);
        PostAdopt IsExist(int userId, int postId);
        PostAdopt SelectById(int id);
        void Add(PostAdopt obj);
        void Update(PostAdopt obj);
        void Delete(int? id);
        void Save();
    }
}
