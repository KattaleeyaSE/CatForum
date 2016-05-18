using CatForum.Context;
using CatForum.Interfaces;
using CatForum.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CatForum.Repositories
{
    public class CatTailRepository : ICatTailRepository
    {
        private CatContext db { get; set; }
        public CatTailRepository()
        {
            db = new CatContext();
        }
        public CatTailRepository(CatContext db)
        {
            this.db = db;
        }
        public IEnumerable<CatTail> SelectAll()
        {
            return db.Tails.ToList();
        }

        public CatTail SelectById(int id)
        {
            CatTail cat = db.Tails.Find(id);
            return cat;
        }
    }
}