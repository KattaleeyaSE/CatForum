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
    public class CatRepository : ICatRepository
    {
        private CatContext db { get; set; }
        public CatRepository()
        {
            db = new CatContext();
        }
        public CatRepository(CatContext db)
        {
            this.db = db;
        }
        public IEnumerable<Cat> SelectAll()
        {
            return db.Cats.ToList();
        }

        public Cat SelectById(int id)
        {
            Cat cat = db.Cats.Find(id);
            return cat;
        }

        public void Add(Cat obj)
        {
            db.Cats.Add(obj);
        }

        public void Update(Cat obj)
        {
            db.Entry(obj).State = EntityState.Modified;
        }

        public void Delete(int? id)
        {
            Cat cat = db.Cats.Find(id);
            db.Cats.Remove(cat);
        }

        public void Save()
        {
            db.SaveChanges();
        }
    }
}