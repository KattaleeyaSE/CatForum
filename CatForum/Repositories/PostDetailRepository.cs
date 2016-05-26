using CatForum.Context;
using CatForum.Interfaces;
using CatForum.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace CatForum.Repositories
{
    public class PostDetailRepository : IPostDetailRepository
    {
        private CatContext db { get; set; }
        public PostDetailRepository()
        {
            db = new CatContext();
        }
        public PostDetailRepository(CatContext db)
        {
            this.db = db;
        }
        public IEnumerable<PostDetail> SelectAll()
        {
            return db.PostDetails.ToList();
        }
        public IEnumerable<PostDetail> SelectByProvince(int id) {
            return db.PostDetails.Where(p => p.Address.ProvinceId == id).ToList();
        }
        public IEnumerable<PostDetail> SelectByOwnerOnType(int ownerid, int typeId) {
            return db.PostDetails.Where(p => p.TypeId == typeId && p.Post.UserId == ownerid).ToList();
        }
        public IEnumerable<PostDetail> SelectDescDate()
        {
            return db.PostDetails.OrderBy(p => p.Post.Updated).ToList();
        }
        public PostDetail SelectById(int id)
        {
            return db.PostDetails.Find(id);
        }
        public void Add(PostDetail obj)
        {
            db.PostDetails.Add(obj);
        }

        public void Update(PostDetail obj)
        {
            db.Entry(obj).State = EntityState.Modified;
        }

        public void Delete(int? id)
        {
            PostDetail post = db.PostDetails.Find(id);
            db.PostDetails.Remove(post);
        }

        public void Save()
        {
            db.SaveChanges();
        }
    }
}