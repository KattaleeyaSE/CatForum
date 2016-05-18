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
    public class ReportRepository : IReportRepository
    {
        private CatContext db { get; set; }
        public ReportRepository()
        {
            db = new CatContext();
        }
        public ReportRepository(CatContext db)
        {
            this.db = db;
        }
        public IEnumerable<Report> SelectAll()
        {
            return db.Reports.ToList();
        }

        public Report SelectById(int id)
        {
            return db.Reports.Find(id);
        }
        public void Add(Report obj)
        {
            db.Reports.Add(obj);
        }

        public void Update(Report obj)
        {
            db.Entry(obj).State = EntityState.Modified;
        }

        public void Delete(int? id)
        {
            Report post = db.Reports.Find(id);
            db.Reports.Remove(post);
        }

        public void Save()
        {
            db.SaveChanges();
        }
    }
}