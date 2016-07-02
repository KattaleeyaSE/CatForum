using CatForum.Interfaces;
using CatForum.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace CatForum.Controllers
{
    public class FileController : Controller
    {
        public IPictureRepository pictures { get; set; }
        public FileController() {
            this.pictures = new PictureRepository();
        }
        // GET: File
        public ActionResult Index(int Id)
        {
            var fileToRetrieve = pictures.SelectById(Id);
            return File(fileToRetrieve.Content, fileToRetrieve.ContentType);
        }
    }
}