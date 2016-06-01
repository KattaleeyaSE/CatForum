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
    public class UserRepository : IUserRepository
    {
        private CatContext db { get; set; }
        public UserRepository()
        {
            db = new CatContext();
        }
        public UserRepository(CatContext db)
        {
            this.db = db;
        }
        public IEnumerable<User> SelectAll()
        {
            return db.Users.ToList();
        }

        public User SelectById(int id)
        {
            return db.Users.Find(id);
        }
        public User Validate(User user)
        {
            User exist = db.Users.FirstOrDefault(u => u.Username == user.Username);
            if (exist != null)
            {
                if (exist.Password.Equals(user.Password))
                {
                    return exist;
                }
            }
            return null;
        }
        public int Register(User obj)
        {
            User exist = db.Users.FirstOrDefault(u => u.Username == obj.Username);
            if (exist == null)
            {
                db.Users.Add(obj);
                db.SaveChanges();
                return obj.Id;
            }
            return 0;
        }
        public void Add(User obj)
        {
            db.Users.Add(obj);
        }

        public void Update(User obj)
        {
            //db.Entry(obj).State = EntityState.Modified;
            User newObj = db.Users.Find(obj.Id);
            newObj = obj;
            db.SaveChanges();
        }

        public void Delete(int? id)
        {
            User user = db.Users.Find(id);
            db.Users.Remove(user);
        }

        public void Save()
        {
            db.SaveChanges();
        }
    }
}