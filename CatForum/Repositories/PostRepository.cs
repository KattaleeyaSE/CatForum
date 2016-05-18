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
    public class PostRepository : IPostRepository
    {
        private CatContext db { get; set; }
        public PostRepository()
        {
            db = new CatContext();
        }
        public PostRepository(CatContext db)
        {
            this.db = db;
        }
        public IEnumerable<Post> SelectAll()
        {
            return db.Posts.ToList();
        }

        public Post SelectById(int id)
        {
            return db.Posts.Find(id);
        }
        public void Add(Post obj)
        {
            db.Posts.Add(obj);
        }

        public void Update(Post obj)
        {
            db.Entry(obj).State = EntityState.Modified;
        }

        public void Delete(int? id)
        {
            Post post = db.Posts.Find(id);
            db.Posts.Remove(post);
        }

        public void Save()
        {
            db.SaveChanges();
        }

    }
}