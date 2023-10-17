using RestaurantBackendWebsite.Models.Entities;
using RestaurantBackendWebsite.Models.Services;
using RestaurantBackendWebsite.Models.ViewModels;
using RestaurantBackendWebsite.Repositories;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Web.Mvc;

namespace RestaurantBackendWebsite.Controllers
{
	public class MembersController : Controller
    {
		MemberService memberService = new MemberService();
		//private readonly string _connectionString;

		// GET: Members
		[Authorize]
		public ActionResult List()
		{
			var data = memberService.GetMembers();
			return View(data);
		}
		
	}
}