using CatForum.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CatForum.Interfaces
{
    public interface IPostTypeRepository
    {
        IEnumerable<PostType> SelectAll();
        PostType SelectById(int id);
    }
}