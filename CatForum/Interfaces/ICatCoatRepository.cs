using CatForum.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CatForum.Interfaces
{
    public interface ICatCoatRepository
    {
        IEnumerable<CatCoat> SelectAll();
        CatCoat SelectById(int id);
    }
}