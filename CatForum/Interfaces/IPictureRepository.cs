using CatForum.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CatForum.Interfaces
{
    public interface IPictureRepository
    {
        IEnumerable<Picture> SelectAll();
        IEnumerable<Picture> SelectByPost(int id);
        Picture SelectById(int id);
        void Add(Picture obj);
        void Update(Picture obj);
        void Delete(int? id);
        void Save();
    }
}
