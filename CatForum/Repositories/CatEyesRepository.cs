using CatForum.Context;
using CatForum.Interfaces;
using CatForum.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CatForum.Repositories
{
    public class CatEyesRepository : ICatEyesRepository
    {
        private CatContext db { get; set; }
        public CatEyesRepository()
        {
            db = new CatContext();
        }
        public CatEyesRepository(CatContext db)
        {
            this.db = db;
        }
        public IEnumerable<CatEyes> SelectAll()
        {
            return db.Eyes.ToList();
        }

        public CatEyes SelectById(int id)
        {
            CatEyes eyes = db.Eyes.Find(id);
            return eyes;
        }
    }
}