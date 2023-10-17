using RestaurantBackendWebsite.Models.DTOs;
using RestaurantBackendWebsite.Models.EFModels;
using RestaurantBackendWebsite.Models.Services;
using RestaurantBackendWebsite.Models.ViewModels;
using RestaurantBackendWebsite.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;

namespace RestaurantBackendWebsite.Controllers
{
	public class CategoriesController : Controller
	{

		private AppDbContext db = new AppDbContext();
		// GET: Categories
		[Authorize]
		public ActionResult Index()
		{

			List<CategoryDTO> dtos = new CategoryRepository().GetCategory();
			List<CategoryVM> vms = dtos.Select(dto => new CategoryVM
			{
				Id = dto.Id,
				Name = dto.Name,
				DisplayOrder = dto.DisplayOrder,
				CreatedDate = dto.CreatedDate,
				ModifiedDate = dto.ModifiedDate,
			}).ToList();
			return View(vms);
		}

		[Authorize]
		public ActionResult Create()
		{
			return View();
		}


		[HttpPost]
		[ValidateAntiForgeryToken]
		public ActionResult Create(CategoryVM vm)
		{
			if (ModelState.IsValid)
			{
				var service = new CategoryService();
				try
				{
					var dto = new CategoryDTO();

					dto.Name = vm.Name;
					dto.DisplayOrder = vm.DisplayOrder;
					dto.CreatedDate = DateTime.Now;
					dto.ModifiedDate = DateTime.Now;

					service.Create(dto);

					//CreateCategory(vm);
				}
				catch (Exception ex)
				{
					ModelState.AddModelError("", ex.Message);
				}
			}
			if (ModelState.IsValid)
			{
				return RedirectToAction("Index");
			}
			return View(vm);
		}



		// GET: Categories/Edit/5
		[Authorize]
		public ActionResult Edit(int? id)
		{
			if (id == null)
			{
				return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
			}
			Category category = db.Categories.Find(id);
			if (category == null)
			{
				return HttpNotFound();
			}

			var vm = new CategoryVM
			{
				Id = category.Id,
				Name = category.Name,
				DisplayOrder = category.DisplayOrder,
			};
			return View(vm);
		}
		[HttpPost]
		[ValidateAntiForgeryToken]
		public ActionResult Edit(CategoryVM vm)
		{

			if (ModelState.IsValid)
			{
				var service = new CategoryService();
				try
				{
					var dto = new CategoryDTO();

					dto.Id = vm.Id;
					dto.Name = vm.Name;
					dto.DisplayOrder = vm.DisplayOrder;

					service.Update(dto);

					return RedirectToAction("Index");
				}
				catch (Exception ex)
				{
					ModelState.AddModelError("", ex.Message);
				}
			}

			return View(vm);
		}

		// GET: Categories/Delete/5
		[Authorize]
		public ActionResult Delete(int? id)
		{
			if (id == null)
			{
				return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
			}

			Category category = db.Categories.Find(id);
			if (category == null)
			{
				return HttpNotFound();
			}
			var vm = new CategoryVM
			{
				Id = category.Id,
				Name = category.Name,
				DisplayOrder = category.DisplayOrder,
			};

			return View(vm);
		}

		// POST: Categories/Delete/5
		[HttpPost, ActionName("Delete")]
		[ValidateAntiForgeryToken]
		public ActionResult DeleteConfirmed(int id)
		{
			Category category = db.Categories.Find(id);
			db.Categories.Remove(category);
			db.SaveChanges();
			return RedirectToAction("Index");
		}
		protected override void Dispose(bool disposing)
		{
			if (disposing)
			{
				db.Dispose();
			}
			base.Dispose(disposing);
		}
	}
}