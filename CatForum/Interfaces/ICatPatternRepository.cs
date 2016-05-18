using CatForum.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CatForum.Interfaces
{
    public interface ICatPatternRepository
    {
        IEnumerable<CatPattern> SelectAll();
        CatPattern SelectById(int id);
    }
}