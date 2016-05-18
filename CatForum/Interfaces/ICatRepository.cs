using CatForum.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CatForum.Interfaces
{
    public interface ICatRepository
    {
        IEnumerable<Cat> SelectAll();
        Cat SelectById(int id);
        void Add(Cat obj);
        void Update(Cat obj);
        void Delete(int? id);
        void Save();
    }
}