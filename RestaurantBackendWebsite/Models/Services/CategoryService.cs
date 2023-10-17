using RestaurantBackendWebsite.Models.DTOs;
using RestaurantBackendWebsite.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace RestaurantBackendWebsite.Models.Services
{
	public class CategoryService
	{
		private CategoryRepository _repo;

		public CategoryService() : this(new CategoryRepository())
		{

		}
		public CategoryService(CategoryRepository repo)
		{
			_repo = repo;
		}

		public void Create(CategoryDTO dto)
		{
			//驗證Category名稱不可重複
			//var repo = new CategoryRepository();
			var category = _repo.GetCategoryByName(dto.Name);

			if (category != null)
			{
				throw new Exception("類別名稱重複");
			};
			_repo.CreateCategory(dto);
		}
		public void Update(CategoryDTO dto)
		{
			//驗證Category名稱不可重複，但要排除自己
			//var repo = new CategoryRepository();
			var category = _repo.GetCategoryByName(dto.Name);

			if (category != null)
			{

				if (category.Id != dto.Id)
				{
					throw new Exception("類別名稱重複");
				}
			}
			//驗證後，更新資料
			dto.ModifiedDate = DateTime.Now;

			_repo.Update(dto);


		}
	}
}