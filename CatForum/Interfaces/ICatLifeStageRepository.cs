using CatForum.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CatForum.Interfaces
{
    public interface ICatLifeStageRepository
    {
        IEnumerable<CatLifeStage> SelectAll();
        CatLifeStage SelectById(int id);
    }
}