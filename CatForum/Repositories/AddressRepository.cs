using CatForum.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using CatForum.Models;
using System.Data.Entity;
using CatForum.Context;

namespace CatForum.Repositories
{
    public class AddressRepository : IAddressRepository
    {
        private CatContext db { get; set; }
        public AddressRepository()
        {
            db = new CatContext();
        }
        public AddressRepository(CatContext db)
        {
            this.db = db;
        }
        public IEnumerable<Address> SelectAll()
        {
            return db.Addresses.ToList();
        }

        public Address SelectByAddressId(int id)
        {
            Address address = db.Addresses.Find(id);
            return address;
        }

        public Address SelectByProvinceId(int id)
        {
            Address address = db.Addresses.FirstOrDefault(a => a.ProvinceId == id);
            return address;
        }

        public Address SelectByTumbonId(int id)
        {
            Address address = db.Addresses.FirstOrDefault(a => a.ProvinceId == id);
            return address;
        }
        public void Add(Address obj)
        {
            db.Addresses.Add(obj);
        }

        public void Update(Address obj)
        {
            Address objA = db.Addresses.Find(obj.Id);
            objA = obj;
            //db.Entry(obj).State = EntityState.Modified;
        }

        public void Delete(int? id)
        {
            Address address = db.Addresses.Find(id);
            db.Addresses.Remove(address);
        }

        public void Save()
        {
            db.SaveChanges();
        }

        public Address IsAddressExist(int province, int amphur, int tumbon)
        {
            Address address = db.Addresses.FirstOrDefault(a => a.ProvinceId == province && a.AmphurId == amphur && a.TumbonId == tumbon);
            return address;
        }
    }
}