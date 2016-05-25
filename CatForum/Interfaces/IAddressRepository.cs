using CatForum.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CatForum.Interfaces
{
    public interface IAddressRepository
    {
        IEnumerable<Address> SelectAll();
        Address SelectByProvinceId(int id);
        Address SelectByAddressId(int id);
        Address SelectByTumbonId(int id);
        Address IsAddressExist(int province,int amphur, int tumbon);
        void Add(Address obj);
        void Update(Address obj);
        void Delete(int? id);
        void Save();
    }
}