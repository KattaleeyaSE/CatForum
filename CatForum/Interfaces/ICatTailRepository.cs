using CatForum.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CatForum.Interfaces
{
    public interface ICatTailRepository
    {
        IEnumerable<CatTail> SelectAll();
        CatTail SelectById(int id);
    }
}