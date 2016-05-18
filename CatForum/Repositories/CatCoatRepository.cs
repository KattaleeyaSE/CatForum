using CatForum.Context;
using CatForum.Interfaces;
using CatForum.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CatForum.Repositories
{
    public class CatCoatRepository : ICatCoatRepository
    {
        private CatContext db { get; set; }
        public CatCoatRepository()
        {
            db = new CatContext();
        }
        public CatCoatRepository(CatContext db)
        {
            this.db = db;
        }
        public IEnumerable<CatCoat> SelectAll()
        {
            return db.Coats.ToList();
        }

        public CatCoat SelectById(int id)
        {
            CatCoat coat = db.Coats.Find(id);
            return coat;
        }
    }
}