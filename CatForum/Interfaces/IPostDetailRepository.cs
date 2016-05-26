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
        IEnumerable<PostDetail> SelectByProvince(int id);
        IEnumerable<PostDetail> SelectByOwnerOnType(int ownerid, int typeId);
        IEnumerable<PostDetail> SelectDescDate();
        PostDetail SelectById(int id);
        void Add(PostDetail obj);
        void Update(PostDetail obj);
        void Delete(int? id);
        void Save();
    }
}