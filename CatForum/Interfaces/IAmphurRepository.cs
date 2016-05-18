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
        Amphur SelectById(int id);
    }
}