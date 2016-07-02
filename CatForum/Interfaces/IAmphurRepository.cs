using CatForum.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CatForum.Interfaces
{
    public interface IAmphurRepository
    {
        IEnumerable<Amphur> SelectAll();
        IEnumerable<Amphur> SelectByProvince(int id);
        Amphur SelectById(int id);
    }
}