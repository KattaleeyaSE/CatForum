using CatForum.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CatForum.Interfaces
{
    public interface ICatBreedRepository
    {
        IEnumerable<CatBreed> SelectAll();
        CatBreed SelectById(int id);
    }
}