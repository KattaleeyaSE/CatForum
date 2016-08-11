using CatForum.Context;
using CatForum.Interfaces;
using CatForum.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CatForum.Repositories
{
    public class PostAdoptRepository : IPostAdoptRepository
    {
        private CatContext db { get; set; }
        public PostAdoptRepository()
        {
            db = new CatContext();
        }
        public PostAdoptRepository(CatContext db)
        {
            this.db = db;
        }
        public IEnumerable<PostAdopt> SelectAll()
        {
            return db.PostAdopts.ToList();
        }
        public IEnumerable<PostAdopt> SearchByUser(int userId)
        {
            return db.PostAdopts.Where(p => p.Post.Post.UserId == userId && p.Post.TypeId == 5 && p.Post.Cat.Status == 3).ToList();
        }
        public IEnumerable<PostAdopt> SearchByOwner(int userId)
        {
            return db.PostAdopts.Where(p => p.Post.Post.Owner.Id == userId && p.Post.TypeId == 5 && p.Post.Cat.Status == 3).ToList();
        }
        public PostAdopt IsExist(int userId, int postId) {
            return db.PostAdopts.Where(p => p.UserId == userId && p.PostId == postId).FirstOrDefault();
        }
        public PostAdopt SelectById(int id)
        {
            return db.PostAdopts.Find(id);
        }
        public void Add(PostAdopt obj)
        {
            db.PostAdopts.Add(obj);
        }

        public void Update(PostAdopt obj)
        {
            PostAdopt newObj = db.PostAdopts.Find(obj.Id);
            newObj = obj;
            db.SaveChanges();
        }

        public void Delete(int? id)
        {
            PostAdopt PostAdopt = db.PostAdopts.Find(id);
            db.PostAdopts.Remove(PostAdopt);
        }

        public void Save()
        {
            db.SaveChanges();
        }
    }
}
