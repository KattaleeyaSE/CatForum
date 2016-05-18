using CatForum.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CatForum.Interfaces
{
    public interface ICatEyesRepository
    {
        IEnumerable<CatEyes> SelectAll();
        CatEyes SelectById(int id);
    }
}