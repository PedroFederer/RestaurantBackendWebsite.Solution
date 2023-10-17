using RestaurantBackendWebsite.Infra.FileValidators;
using RestaurantBackendWebsite.Infra;
using RestaurantBackendWebsite.Models.EFModels;
using RestaurantBackendWebsite.Models.ViewModels;
using RestaurantBackendWebsite.Repositories;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using static RestaurantBackendWebsite.Repositories.MealsRepository;

namespace RestaurantBackendWebsite.Controllers
{
    public class MealsController : Controller
    {
		private AppDbContext db = new AppDbContext();
		// GET: Meals
		[Authorize]
		public ActionResult Index(string CategoryId, string name, int pageIndex = 0)
		{
			//List<MealIndexVM> vms = new MealsRepository().GetMeals();
			int pageSize = 10;
			var pagedProducts = new MealsRepository().GetPagedMeals(name, pageSize, pageIndex);

			ViewBag.PaginationInfo = new PaginationInfo(pagedProducts.PageSize, pagedProducts.PageIndex, pagedProducts.Total);
			ViewBag.criteria = name;

			return View(pagedProducts.Data);
		}


		// GET: Meals/Create
		[Authorize]
		public ActionResult Create()
		{
			ViewBag.CategoryId = new SelectList(db.Categories, "Id", "Name");
			return View();
		}

		// POST: Meals/Create
		[HttpPost]
		[ValidateAntiForgeryToken]
		public ActionResult Create(MealIndexVM vm, HttpPostedFileBase myfile)
		{
			//將上傳檔案到/MyRestaurantImg 資料夾下

			string fileName;
			string path = Server.MapPath("~/MyRestaurantImg/");
			IFileValidator[] validators = new IFileValidator[]
			{
				new FileRequired(),
				new ImageValidator(),
				new FileSizeValidator(4096),
			};
			try
			{
				fileName = UploadFileHelper.Save(myfile, path, validators);

				//copy 一份到前台網站的資料夾下
				string sourceFilePath = Path.Combine(path, fileName);

				string dest = System.Configuration.ConfigurationManager.AppSettings["MealImgPath"];
				string destFilePath = Path.Combine(dest, fileName);

				System.IO.File.Copy(sourceFilePath, destFilePath, true);
			}
			catch (Exception ex)
			{
				ModelState.AddModelError("myfile", ex.Message);

				ViewBag.CategoryId = new SelectList(db.Categories, "Id", "Name", vm.CategoryId);
				return View(vm);
			}

			vm.MealsImage = fileName;



			if (ModelState.IsValid)
			{
				try
				{
					CreateMeals(vm);
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
		private void CreateMeals(MealIndexVM vm)
		{
			var meals = new Meal
			{
				CategoryId = vm.CategoryId,
				Name = vm.Name,
				Description = vm.Description,
				Price = vm.Price,
				MealsImage = vm.MealsImage,
				Status = vm.Status,
				CreatedDate = DateTime.Now,
				ModifiedDate = DateTime.Now,
			};
			db.Meals.Add(meals);
			db.SaveChanges();
		}

		// GET: Meals/Edit/5
		[Authorize]
		public ActionResult Edit(int? id)
		{

			if (id == null)
			{
				return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
			}
			Meal meal = db.Meals.Find(id);
			if (meal == null)
			{
				return HttpNotFound();
			}
			ViewBag.CategoryId = new SelectList(db.Categories, "Id", "Name", meal.CategoryId);

			var vm = new MealIndexVM
			{
				Id = meal.Id,
				CategoryId = meal.CategoryId,
				Name = meal.Name,
				Description = meal.Description,
				Price = meal.Price,
				MealsImage = meal.MealsImage,
				Status = meal.Status,
				CreatedDate = meal.CreatedDate,

			};

			return View(vm);
		}
		//POST: Meals/Edit/5
		[HttpPost]
		[ValidateAntiForgeryToken]
		public ActionResult Edit(MealIndexVM vm, HttpPostedFileBase myfile)
		{

			var mealInDb = db.Meals.Find(vm.Id);
			mealInDb.CategoryId = vm.CategoryId;
			mealInDb.Name = vm.Name;
			mealInDb.Description = vm.Description;
			mealInDb.Price = vm.Price;
			mealInDb.Status = vm.Status;
			mealInDb.ModifiedDate = DateTime.Now;

			//string fileName;
			//string fileName=mealInDb.MealsImage;//原本的圖片

			if (myfile != null && myfile.ContentLength > 0)
			{
				string fileName;
				//使用新上傳的檔案
				string path = Server.MapPath("~/MyRestaurantImg/");
				IFileValidator[] validators = new IFileValidator[]
				{
					//new FileRequired(),
					new ImageValidator(),
					new FileSizeValidator(4096),
				};
				//ViewModels的值要等於新值
				try
				{
					// 刪除舊圖片
					string oldFileName = Path.Combine(path, mealInDb.MealsImage);
					if (System.IO.File.Exists(oldFileName))
					{
						System.IO.File.Delete(oldFileName);
					}
					fileName = UploadFileHelper.Save(myfile, path, validators);


					//copy 一份到前台網站的資料夾下
					string sourceFilePath = Path.Combine(path, fileName);

					string dest = System.Configuration.ConfigurationManager.AppSettings["MealImgPath"];
					string destFilePath = Path.Combine(dest, fileName);

					System.IO.File.Copy(sourceFilePath, destFilePath, true);

					mealInDb.MealsImage = fileName;

				}
				catch (Exception ex)
				{
					ModelState.AddModelError("myfile", ex.Message);
					ViewBag.CategoryId = new SelectList(db.Categories, "Id", "Name", vm.CategoryId);
					return View(vm);
				}
			}
			vm.MealsImage = mealInDb.MealsImage;

			if (ModelState.IsValid)
			{
				db.SaveChanges();
				return RedirectToAction("Index");
			}

			return View(vm);
		}


		//GET: Meals/Delete/5
		[Authorize]
		public ActionResult Delete(int? id)
		{
			if (id == null)
			{
				return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
			}
			Meal meal = db.Meals.Find(id);
			if (meal == null)
			{
				return HttpNotFound();
			}
			var vm = new MealIndexVM
			{
				Id = meal.Id,
				CategoryId = meal.CategoryId,
				Name = meal.Name,
				Description = meal.Description,
				Price = meal.Price,
				MealsImage = meal.MealsImage,
				Status = meal.Status,
				CreatedDate = meal.CreatedDate,
				ModifiedDate = meal.ModifiedDate,
			};
			return View(vm);
		}

		//POST: Meals/Delete/5
		[HttpPost, ActionName("Delete")]
		[ValidateAntiForgeryToken]
		public ActionResult DeleteConfirmed(int id)
		{
			Meal meal = db.Meals.Find(id);
			db.Meals.Remove(meal);
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