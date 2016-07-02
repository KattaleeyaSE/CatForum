using CatForum.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CatForum.Interfaces
{
    public interface ITumbonRepository
    {
        IEnumerable<Tumbon> SelectAll();
        IEnumerable<Tumbon> SelectByAmphur(int id);
        Tumbon SelectById(int id);
    }
}