using CatForum.Controllers;
using CatForum.Interfaces;
using CatForum.Models;
using CatForum.Repositories;
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
    [TestClass()]
    public class AdminControllerTest
    {
        [TestMethod()]
        public void IndexTest()
        {
            var controller = new AdminController();
            var result = controller.Index() as ViewResult;
            Assert.IsNotNull(result);
            Assert.IsInstanceOfType(result, typeof(ViewResult));
        }
        [TestMethod()]
        public void MembersTest()
        {
            var setUp = new TestSetUp();
            setUp.initUser();

            var memberRepo = new Mock<IUserRepository>();
            memberRepo.Setup(x => x.SelectAll()).Returns(setUp.users);

            var controller = new AdminController();
            controller.users = memberRepo.Object;

            var result = controller.Members() as ViewResult;
            Assert.IsNotNull(result);
            Assert.IsInstanceOfType(result, typeof(ViewResult));
            Assert.IsInstanceOfType(result.ViewData["Members"], typeof(List<User>));
        }
        [TestMethod()]
        public void ReportsTest()
        {

            var repo = new Mock<IReportRepository>();
            repo.Setup(x => x.SelectAll()).Returns(new List<Report>());

            var controller = new AdminController();
            controller.reports = repo.Object;

            var result = controller.Reports() as ViewResult;
            Assert.IsNotNull(result);
            Assert.IsInstanceOfType(result, typeof(ViewResult));
            Assert.IsInstanceOfType(result.ViewData["Reports"], typeof(List<Report>));
        }

        [TestMethod()]
        public void LoginValidTest()
        {
            var sessionValue = new Admin();
            var context = new Mock<HttpContextBase>();
            var session = new Mock<HttpSessionStateBase>();
            context.Setup(x => x.Session).Returns(session.Object);
            context.SetupSet(x => x.Session["Admin"] = It.IsAny<Admin>()).Callback((string name, object val) =>
            {
                sessionValue = (Admin)val;
                context.SetupGet(x => x.Session["Admin"]).Returns(sessionValue);
            });
            context.SetupGet(x => x.Session["Admin"]).Returns(null);

            var requestContext = new RequestContext(context.Object, new RouteData());

            Admin param1 = new Admin();
            param1.Id = 1;
            param1.Username = "admin";
            param1.Password = "1234";

            var repo = new Mock<IAdminRepository>();
            repo.Setup(x => x.Validate(param1)).Returns(param1);

            var controller = new AdminController();
            controller.ControllerContext = new ControllerContext(requestContext, controller);
            controller.admins = repo.Object;

            var result = controller.Login(param1) as RedirectToRouteResult;
            Assert.IsNotNull(result);
            Assert.IsInstanceOfType(result, typeof(RedirectToRouteResult));
            Assert.AreEqual(result.RouteValues.Values.ElementAt(0), "Reports");
            Assert.AreEqual(sessionValue,param1);
        }
        [TestMethod()]
        public void LoginInvalidTest()
        {
            var context = new Mock<HttpContextBase>();
            var session = new Mock<HttpSessionStateBase>();
            context.Setup(x => x.Session).Returns(session.Object);

            var requestContext = new RequestContext(context.Object, new RouteData());

            var controller = new AdminController();
            controller.ControllerContext = new ControllerContext(requestContext, controller);

            var repo = new Mock<IAdminRepository>();
            repo.Setup(x => x.Validate(null)).Returns<Admin>(null);
            controller.admins = repo.Object;

            var result = controller.Login(null) as RedirectToRouteResult;
            Assert.IsNotNull(result);
            Assert.IsInstanceOfType(result, typeof(RedirectToRouteResult));
            Assert.AreEqual(result.RouteValues.Values.ElementAt(0), "Index");
        }
        [TestMethod()]
        public void LogoutTest()
        {
            var sessionValue = new Admin();
            var context = new Mock<HttpContextBase>();
            var session = new Mock<HttpSessionStateBase>();
            context.Setup(x => x.Session).Returns(session.Object);
            context.SetupSet(x => x.Session["Admin"] = It.IsAny<Admin>()).Callback((string name, object val) =>
            {
                sessionValue = (Admin)val;
                context.SetupGet(x => x.Session["Admin"]).Returns(sessionValue);
            });
            context.SetupGet(x => x.Session["Admin"]).Returns(new Admin());
            var requestContext = new RequestContext(context.Object, new RouteData());

            var controller = new AdminController();
            controller.ControllerContext = new ControllerContext(requestContext, controller);

            var result = controller.Logout() as RedirectToRouteResult;
            Assert.IsNotNull(result);
            Assert.IsInstanceOfType(result, typeof(RedirectToRouteResult));
            Assert.AreEqual(result.RouteValues.Values.ElementAt(0), "Login");
            Assert.AreEqual(sessionValue, null);
        }
    }
}
