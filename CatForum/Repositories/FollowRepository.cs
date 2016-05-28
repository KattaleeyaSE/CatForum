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
    public class FollowRepository : IFollowRepository
    {
        private CatContext db { get; set; }
        public FollowRepository()
        {
            db = new CatContext();
        }
        public FollowRepository(CatContext db)
        {
            this.db = db;
        }
        public IEnumerable<Follow> SelectAll()
        {
            return db.Follows.ToList();
        }

        public Follow SelectById(int id)
        {
            return db.Follows.Find(id);
        }
        public Follow SearchByUserAndPost(int userId, int postId) {
            return db.Follows.Where(f => f.UserId == userId && f.PostId == postId).FirstOrDefault();
        }
        public void Add(Follow obj)
        {
            db.Follows.Add(obj);
        }

        public void Update(Follow obj)
        {
            db.Entry(obj).State = EntityState.Modified;
        }

        public void Delete(int? id)
        {
            Follow Follow = db.Follows.Find(id);
            db.Follows.Remove(Follow);
        }

        public void Save()
        {
            db.SaveChanges();
        }
    }
}