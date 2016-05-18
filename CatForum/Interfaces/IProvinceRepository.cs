using CatForum.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CatForum.Interfaces
{
    public interface IProvinceRepository
    {
        IEnumerable<Province> SelectAll();
        Province SelectById(int id);
    }
}