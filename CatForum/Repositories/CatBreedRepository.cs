using CatForum.Context;
using CatForum.Interfaces;
using CatForum.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CatForum.Repositories
{
    public class CatBreedRepository : ICatBreedRepository
    {
        private CatContext db { get; set; }
        public CatBreedRepository()
        {
            db = new CatContext();
        }
        public CatBreedRepository(CatContext db)
        {
            this.db = db;
        }
        public IEnumerable<CatBreed> SelectAll()
        {
            return db.Breeds.ToList();
        }

        public CatBreed SelectById(int id)
        {
            CatBreed amphur = db.Breeds.Find(id);
            return amphur;
        }
    }
}