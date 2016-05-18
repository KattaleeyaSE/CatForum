using CatForum.Context;
using CatForum.Interfaces;
using CatForum.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CatForum.Repositories
{
    public class PostTypeRepository : IPostTypeRepository
    {
        private CatContext db { get; set; }
        public PostTypeRepository()
        {
            db = new CatContext();
        }
        public PostTypeRepository(CatContext db)
        {
            this.db = db;
        }
        public IEnumerable<PostType> SelectAll()
        {
            return db.PostTypes.ToList();
        }

        public PostType SelectById(int id)
        {
            PostType post = db.PostTypes.Find(id);
            return post;
        }
    }
}