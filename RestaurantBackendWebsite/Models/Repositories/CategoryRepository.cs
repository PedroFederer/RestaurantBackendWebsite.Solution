using RestaurantBackendWebsite.Models.DTOs;
using RestaurantBackendWebsite.Models.EFModels;
using RestaurantBackendWebsite.Models.Utilities;
using RestaurantBackendWebsite.Models.ViewModels;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Web;

namespace RestaurantBackendWebsite.Repositories
{
	public class CategoryRepository
	{
		//ADO.Net
		public List<CategoryDTO> GetCategory()
		{
			string sql = @"SELECT Id,Name,DisplayOrder,CreatedDate,ModifiedDate
				  FROM Categories";
			var db = new SqlDb();
			using (var conn = db.GetConnection("AppDbContext"))
			{
				conn.Open();
				var command = new SqlCommand(sql, conn);

				var categories = new List<CategoryDTO>();

				using (var reader = command.ExecuteReader())
				{
					while (reader.Read())
					{
						var category = new CategoryDTO
						{
							Id = reader.GetInt32(reader.GetOrdinal("Id")),
							Name = reader.GetString(reader.GetOrdinal("Name")),
							DisplayOrder = reader.GetInt32(reader.GetOrdinal("DisplayOrder")),
							CreatedDate = reader.GetDateTime(reader.GetOrdinal("CreatedDate")),
							ModifiedDate = reader.IsDBNull(reader.GetOrdinal("ModifiedDate")) ? null : (DateTime?)reader.GetDateTime(reader.GetOrdinal("ModifiedDate"))
						};
						categories.Add(category);
					}

				}
				return categories;
			}

		}
		public CategoryDTO GetCategoryByName(string name)
		{
			//驗證Category名稱不可重複
			string sql = @"SELECT*FROM Categories
				  WHERE Name=@Name";
			var db = new SqlDb();
			using (var conn = db.GetConnection("AppDbContext"))
			{
				conn.Open();
				var command = new SqlCommand(sql, conn);

				SqlParameter param = new SqlParameter("@Name", SqlDbType.NVarChar);
				param.Value = name;//name是從前端傳過來的
				command.Parameters.Add(param);
				//command.Parameters.Add(new SqlParameter("@Name", SqlDbType.NVarChar));

				var reader = command.ExecuteReader();
				if (reader.Read())
				{
					var category = new CategoryDTO
					{
						Id = reader.GetInt32(reader.GetOrdinal("Id")),
						Name = reader.GetString(reader.GetOrdinal("Name")),
						DisplayOrder = reader.GetInt32(reader.GetOrdinal("DisplayOrder")),
						CreatedDate = reader.GetDateTime(reader.GetOrdinal("CreatedDate")),
						//ModifiedDate = (DateTime?)reader.GetDateTime(reader.GetOrdinal("ModifiedDate")),
					};
					int modifiedDateIndex = reader.GetOrdinal("ModifiedDate");
					if (!reader.IsDBNull(modifiedDateIndex))
					{
						category.ModifiedDate = reader.GetDateTime(modifiedDateIndex);
					}
					else
					{
						category.ModifiedDate = null;
					}

					return category;
				}
				else
				{
					return null;
				}

			}
		}
		public void CreateCategory(CategoryDTO dto)
		{
			var db = new AppDbContext();
			var category = new Category
			{
				Name = dto.Name,
				DisplayOrder = dto.DisplayOrder,
				CreatedDate = DateTime.Now,
			};
			db.Categories.Add(category);
			db.SaveChanges();
		}
		public void Update(CategoryDTO dto)
		{
			var db = new AppDbContext();
			var category = db.Categories.Find(dto.Id);
			if (category == null)
			{
				// throw new Exception("找不到要修改的類別");
				return;
			}

			category.Name = dto.Name;
			category.DisplayOrder = dto.DisplayOrder;
			category.ModifiedDate = dto.ModifiedDate;

			db.SaveChanges();

		}
	}
}