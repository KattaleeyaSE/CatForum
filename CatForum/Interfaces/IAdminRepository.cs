using CatForum.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CatForum.Interfaces
{
    public interface IAdminRepository
    {
        IEnumerable<Admin> SelectAll();
        Admin SelectById(int id);
        void Add(Admin obj);
        void Update(Admin obj);
        void Delete(int? id);
        void Save();
    }
}
