using CatForum.Context;
using CatForum.Interfaces;
using CatForum.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CatForum.Repositories
{
    class AdminRepository : IAdminRepository
    {
        private CatContext db { get; set; }
        public AdminRepository()
        {
            db = new CatContext();
        }
        public AdminRepository(CatContext db)
        {
            this.db = db;
        }
        public IEnumerable<Admin> SelectAll()
        {
            return db.Admins.ToList();
        }

        public Admin SelectById(int id)
        {
            return db.Admins.Find(id);
        }
        
        public void Add(Admin obj)
        {
            db.Admins.Add(obj);
        }

        public void Update(Admin obj)
        {
            //db.Entry(obj).State = EntityState.Modified;
            Admin newObj = db.Admins.Find(obj.Id);
            newObj = obj;
            db.SaveChanges();
        }

        public void Delete(int? id)
        {
            Admin admin = db.Admins.Find(id);
            db.Admins.Remove(admin);
        }

        public void Save()
        {
            db.SaveChanges();
        }
        public Admin Validate(Admin admin)
        {
            Admin exist = db.Admins.FirstOrDefault(u => u.Username == admin.Username);
            if (exist != null)
            {
                if (exist.Password.Equals(admin.Password))
                {
                    return exist;
                }
            }
            return null;
        }
    }
}
