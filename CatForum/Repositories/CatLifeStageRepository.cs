using CatForum.Context;
using CatForum.Interfaces;
using CatForum.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CatForum.Repositories
{
    public class CatLifeStageRepository : ICatLifeStageRepository
    {
        private CatContext db { get; set; }
        public CatLifeStageRepository()
        {
            db = new CatContext();
        }
        public CatLifeStageRepository(CatContext db)
        {
            this.db = db;
        }
        public IEnumerable<CatLifeStage> SelectAll()
        {
            return db.LifeStages.ToList();
        }

        public CatLifeStage SelectById(int id)
        {
            CatLifeStage lifeStage = db.LifeStages.Find(id);
            return lifeStage;
        }
    }
}