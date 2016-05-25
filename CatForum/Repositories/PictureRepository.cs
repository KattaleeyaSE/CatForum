using CatForum.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CatForum.Models;
using CatForum.Context;
using System.Data.Entity;

namespace CatForum.Repositories
{
    public class PictureRepository : IPictureRepository
    {
        private CatContext db { get; set; }
        public PictureRepository()
        {
            db = new CatContext();
        }
        public PictureRepository(CatContext db)
        {
            this.db = db;
        }
        public IEnumerable<Picture> SelectAll()
        {
            return db.Pictures.ToList();
        }
        public IEnumerable<Picture> SelectByPost(int id)
        {
            return db.Pictures.Where(p => p.PostId == id).ToList();
        }
        public Picture SelectById(int id)
        {
            return db.Pictures.Find(id);
        }
        
        public void Add(Picture obj)
        {
            db.Pictures.Add(obj);
        }

        public void Update(Picture obj)
        {
            db.Entry(obj).State = EntityState.Modified;
        }

        public void Delete(int? id)
        {
            Picture Picture = db.Pictures.Find(id);
            db.Pictures.Remove(Picture);
        }

        public void Save()
        {
            db.SaveChanges();
        }
    }
}
