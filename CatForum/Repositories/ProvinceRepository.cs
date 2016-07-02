using CatForum.Context;
using CatForum.Interfaces;
using CatForum.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CatForum.Repositories
{
    public class ProvinceRepository : IProvinceRepository
    {
        private CatContext db { get; set; }
        public ProvinceRepository()
        {
            db = new CatContext();
        }
        public ProvinceRepository(CatContext db)
        {
            this.db = db;
        }
        public IEnumerable<Province> SelectAll()
        {
            return db.Provinces.OrderBy(p => p.Name).ToList();
        }

        public Province SelectById(int id)
        {
            Province province = db.Provinces.Find(id);
            return province;
        }
    }
}