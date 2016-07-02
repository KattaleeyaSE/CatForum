using CatForum.Controllers;
using CatForum.Forms;
using CatForum.Interfaces;
using CatForum.Models;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace CatTest.Controllers
{
    [TestClass]
    public class UserControllerTest
    {
        [TestMethod]
        public void EditTest()
        {
            var provinces = new Mock<IProvinceRepository>();
            provinces.Setup(x => x.SelectAll()).Returns<List<Province>>(null);
            var amphur = new Mock<IAmphurRepository>();
            amphur.Setup(x => x.SelectAll()).Returns<List<Amphur>>(null);
            var tumbon = new Mock<ITumbonRepository>();
            amphur.Setup(x => x.SelectAll()).Returns<List<Tumbon>>(null);

            var controller = new UserController();
            controller.prov = provinces.Object;
            controller.amp = amphur.Object;
            controller.tmbn = tumbon.Object;

            var result = controller.Edit() as ViewResult;
            Assert.IsNotNull(result);
            Assert.IsInstanceOfType(result, typeof(ViewResult));
        }
        [TestMethod()]
        public void EditPostTest()
        {
            var post = new UserEditForm();
            post.Username = "user";
            post.Password = "1234";
            post.RePassword = "1234";
            post.Email = "a@a.a";
            post.Province = 1;
            post.Amphur = 1;
            post.Tumbon = 1;

            var user = new User();
            user.Id = 1;
            user.Username = "user";
            user.Password = "1234";
            user.Email = "a@a.a";
            user.Address = new Address();
            user.Address.ProvinceId = 1;
            user.Address.AmphurId = 1;
            user.Address.TumbonId = 1;
            user.DisplayName = "User";
            user.Gender = 1;

            var context = new Mock<HttpContextBase>();
            var session = new Mock<HttpSessionStateBase>();
            context.Setup(x => x.Session).Returns(session.Object);
            context.SetupGet(x => x.Session["User"]).Returns(user);

            var requestContext = new RequestContext(context.Object, new RouteData());

            var repo = new Mock<IUserRepository>();
            repo.Setup(x => x.SelectById(1)).Returns(user);

            var controller = new UserController();
            controller.ControllerContext = new ControllerContext(requestContext, controller);
            controller.repository = repo.Object;

            var result = controller.Edit(post) as RedirectToRouteResult;
            Assert.IsNotNull(result);
            Assert.IsInstanceOfType(result, typeof(RedirectToRouteResult));
            Assert.AreEqual(result.RouteValues.Values.ElementAt(0), "Edit");
        }

        [TestMethod()]
        public void ForumsTest()
        {
            var user = new User();
            user.Id = 1;

            var context = new Mock<HttpContextBase>();
            var session = new Mock<HttpSessionStateBase>();
            context.Setup(x => x.Session).Returns(session.Object);
            context.SetupGet(x => x.Session["User"]).Returns(user);
            var requestContext = new RequestContext(context.Object, new RouteData());

            var details = new Mock<IPostDetailRepository>();
            details.Setup(x => x.SelectAll()).Returns(new List<PostDetail>());
            var types = new Mock<IPostTypeRepository>();
            types.Setup(x => x.SelectAll()).Returns(new List<PostType>());
            var posts = new Mock<IPostRepository>();
            posts.Setup(x => x.SelectAll()).Returns(new List<Post>());

            var controller = new UserController();
            controller.ControllerContext = new ControllerContext(requestContext, controller);
            controller.detailRepository = details.Object;
            controller.typeRepository = types.Object;
            controller.postRepository = posts.Object;

            var result = controller.Forums() as ViewResult;
            Assert.IsNotNull(result);
            Assert.IsInstanceOfType(result, typeof(ViewResult));
            Assert.IsInstanceOfType(result.ViewData["All"], typeof(List<Post>));
            Assert.IsInstanceOfType(result.ViewData["Details"], typeof(IPostDetailRepository));
            Assert.IsInstanceOfType(result.ViewData["PostTypes"], typeof(List<PostType>));
        }
    }
}
