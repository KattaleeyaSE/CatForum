using CatForum.Context;
using CatForum.Interfaces;
using CatForum.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CatForum.Repositories
{
    public class CatPatternRepository : ICatPatternRepository
    {
        private CatContext db { get; set; }
        public CatPatternRepository()
        {
            db = new CatContext();
        }
        public CatPatternRepository(CatContext db)
        {
            this.db = db;
        }
        public IEnumerable<CatPattern> SelectAll()
        {
            return db.Patterns.ToList();
        }

        public CatPattern SelectById(int id)
        {
            CatPattern cat = db.Patterns.Find(id);
            return cat;
        }
    }
}