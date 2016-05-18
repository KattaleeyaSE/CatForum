using CatForum.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CatForum.Interfaces
{
    public interface IReportRepository
    {
        IEnumerable<Report> SelectAll();
        Report SelectById(int id);
        void Add(Report obj);
        void Update(Report obj);
        void Delete(int? id);
        void Save();
    }
}