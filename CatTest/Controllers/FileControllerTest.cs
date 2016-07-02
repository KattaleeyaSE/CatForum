using CatForum.Controllers;
using CatForum.Interfaces;
using CatForum.Models;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace CatTest.Controllers
{
    [TestClass]
    public class FileControllerTest
    {
        [TestMethod]
        public void IndexTest()
        {
            var picture = new Picture();
            picture.Content = new byte[] { 1 };
            picture.ContentType = "image/jpg";

            var repo = new Mock<IPictureRepository>();
            repo.Setup(x => x.SelectById(0)).Returns(picture);

            var controller = new FileController();
            controller.pictures = repo.Object;

            var result = controller.Index(0) as FileContentResult;
            Assert.IsNotNull(result);
            Assert.IsInstanceOfType(result, typeof(FileContentResult));
        }
    }
}
