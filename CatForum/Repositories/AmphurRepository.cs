﻿using CatForum.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using CatForum.Models;
using CatForum.Context;

namespace CatForum.Repositories
{
    public class AmphurRepository : IAmphurRepository
    {
        private CatContext db { get; set; }
        public AmphurRepository()
        {
            db = new CatContext();
        }
        public AmphurRepository(CatContext db)
        {
            this.db = db;
        }
        public IEnumerable<Amphur> SelectAll()
        {
            return db.Amphurs.OrderBy(p => p.Name).ToList();
        }
        public IEnumerable<Amphur> SelectByProvince(int id)
        {
            var amphurs = db.Amphurs.Where(a => a.ProvinceId == id).OrderBy(p => p.Name).ToList();
            return amphurs;
        }
        public Amphur SelectById(int id)
        {
            Amphur amphur = db.Amphurs.Find(id);
            return amphur;
        }
    }
}