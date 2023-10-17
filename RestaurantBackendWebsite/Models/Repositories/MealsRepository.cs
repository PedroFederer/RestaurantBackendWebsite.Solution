using RestaurantBackendWebsite.Models.EFModels;
using RestaurantBackendWebsite.Models.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace RestaurantBackendWebsite.Repositories
{
	public class MealsRepository
	{
		public List<MealIndexVM> GetMeals()
		{
			var db = new AppDbContext();
			var meals = db.Meals.AsNoTracking().Include("Category").Select(m => new MealIndexVM
			{
				Id = m.Id,
				//CategoryName = m.Category.Name,
				CategoryId = m.CategoryId,
				Name = m.Name,
				Description = m.Description,
				Price = m.Price,
				MealsImage = m.MealsImage,
				Status = m.Status,
				CreatedDate = m.CreatedDate,
				ModifiedDate = m.ModifiedDate
			}).ToList();

			return meals;
		}
		public Paged<MealIndexVM> GetPagedMeals(string criteria, int pageSize, int pageIndex)
		{
			var db = new AppDbContext();
			var query = db.Meals
				.AsNoTracking()
				.Include("Category")
				.Select(m => new MealIndexVM
				{
					Id = m.Id,
					CategoryId = m.CategoryId,
					CategoryName = m.Category.Name,
					Name = m.Name,
					Description = m.Description,
					Price = m.Price,
					MealsImage = m.MealsImage,
					Status = m.Status,
					CreatedDate = m.CreatedDate,
					ModifiedDate = m.ModifiedDate
				});
			query = query.OrderBy(m => m.Id);

			//var allProducts = Enumerable.Range(1, 10000).Select(n => new MealIndexVM { Id = n, Price = n * 10, Name = "Name " + n }).ToList();
			//int skip = pageIndex * pageSize;

			//IEnumerable<MealIndexVM> query = allProducts;
			if (!string.IsNullOrEmpty(criteria))
			{
				query = query.Where(m => m.Name.Contains(criteria));
			}

			int total = query.Count();
			int skip = pageIndex * pageSize;

			var pagedMeals = query.Skip(skip).Take(pageSize).ToList();


			return new Paged<MealIndexVM>(pagedMeals, pageSize, pageIndex, total);
		}

	}
	public class PaginationInfo
	{
		public int Total { get; set; } // 總筆數
		public int PageSize { get; set; } // 每頁筆數
		public int PageIndex { get; set; } // 目前頁數, 0-based
		public PaginationInfo(int pageSize, int pageIndex, int total)
		{
			this.PageSize = pageSize;
			this.PageIndex = pageIndex;
			this.Total = total;
		}
	}
	public class Paged<T>
	{
		public int Total { get; set; } // 總筆數
		public int PageSize { get; set; } // 每頁筆數
		public int PageIndex { get; set; } // 目前頁數, 0-based

		public IEnumerable<T> Data { get; set; } // 單頁資料

		public Paged(IEnumerable<T> data, int pageSize, int pageIndex, int total)
		{
			this.Data = data;
			this.PageSize = pageSize;
			this.Total = total;
			this.PageIndex = pageIndex;
		}
	}
}