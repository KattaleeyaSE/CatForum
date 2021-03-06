﻿using CatForum.Context;
using CatForum.Interfaces;
using CatForum.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CatForum.Repositories
{
    public class TumbonRepository : ITumbonRepository
    {
        private CatContext db { get; set; }
        public TumbonRepository()
        {
            db = new CatContext();
        }
        public TumbonRepository(CatContext db)
        {
            this.db = db;
        }
        public IEnumerable<Tumbon> SelectAll()
        {
            return db.Tumbons.OrderBy(p => p.Name).ToList();
        }
        public IEnumerable<Tumbon> SelectByAmphur(int id)
        {
            var tumbons = db.Tumbons.Where(a => a.AmphurId == id).OrderBy(p => p.Name).ToList();
            return tumbons;
        }
        public Tumbon SelectById(int id)
        {
            Tumbon amphur = db.Tumbons.Find(id);
            return amphur;
        }
    }
}