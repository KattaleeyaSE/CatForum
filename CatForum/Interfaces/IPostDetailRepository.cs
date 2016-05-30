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
        IEnumerable<PostDetail> SelectByType(int type);
        IEnumerable<PostDetail> SelectByOwnerOnType(int ownerid, int typeId);
        IEnumerable<PostDetail> SelectDescDate(int? type, int? offset);
        IEnumerable<PostDetail> Search(Nullable<int> Type, Nullable<int> offset, Nullable<int> Eyes, Nullable<int> Coat, Nullable<int> Pattern, Nullable<int> Tail, Nullable<int> Breed, Nullable<int> Province, Nullable<int> Amphur, Nullable<int> Tumbon);
        IEnumerable<PostDetail> SearchMatch(int userId);
        PostDetail SelectById(int id);
        void Add(PostDetail obj);
        void Update(PostDetail obj);
        void Delete(int? id);
        void Save();
    }
}