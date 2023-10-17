using Microsoft.Ajax.Utilities;
using RestaurantBackendWebsite.Models.EFModels;
using RestaurantBackendWebsite.Models.Entities;
using RestaurantBackendWebsite.Models.Infra;
using RestaurantBackendWebsite.Models.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using System.Web.UI.WebControls;
using User = RestaurantBackendWebsite.Models.Entities.User;

namespace RestaurantBackendWebsite.Controllers
{
	
	public class UsersController : Controller
    {
		// GET: Users
		[Authorize]
		public ActionResult Index()
        {
            var user=dbHelper.GetUserList();
            return View(user);
        }
		//create要修
		//public ActionResult Create()
		//{
		//	var user = dbHelper.GetUserList();
		//	return View(user);
		//}
		[Authorize]
		public ActionResult Create()
		{
			return View();
		}
		[HttpPost]
		public ActionResult Create(User user)
		{
			dbHelper.CreateUsers(user);
			return RedirectToAction("Index");
		}
		public ActionResult Detail(int Id)
		{
			var user = dbHelper.GetUserById(Id);
			if (user == null)
			{
				return RedirectToAction("Index");
			}
			return View(user);
		}
		public ActionResult Edit(int? Id)
		{
			var user = dbHelper.GetUserById(Id);
			if (user == null)
			{
				return RedirectToAction("Index");
			}
			return View(user);
		}
		[HttpPost]
		public ActionResult Edit(User user)
		{
			dbHelper.UpdateUsers(user);
			var db = new AppDbContext();
			db.SaveChanges();
			return RedirectToAction("Index", "Users", new { id = user.Id });
		}
		public ActionResult Delete(int Id)
		{
			var user = dbHelper.GetUserById(Id);
			if (user == null)
			{
				return RedirectToAction("Index");
			}
			return View(user);
		}
		[HttpPost]
		public ActionResult Delete(User user)
		{
			dbHelper.DeleteUser(user.Id);
			return RedirectToAction("Index");
		}

		public ActionResult Login()
		{
			return View();
		}

		[HttpPost]
		public ActionResult Login(LoginVm vm)
		{
			var db = new AppDbContext();
			var user = db.Users.FirstOrDefault(p => p.Account == vm.Account);
			if (user!=null)
			{
				FormsAuthentication.SetAuthCookie(user.Account, false);
				return RedirectToAction("Index", "Users");
			}

			ModelState.AddModelError("", "帳號或密碼錯誤");

			return View();
		}

		public ActionResult Logout()
		{
			Session.Abandon();
			FormsAuthentication.SignOut();
			return Redirect("/Users/Login");
		}

	}
}