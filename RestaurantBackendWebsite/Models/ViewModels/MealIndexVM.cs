using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace RestaurantBackendWebsite.Models.ViewModels
{
	public class MealIndexVM
	{
		public int Id { get; set; }

		[Display(Name = "類別")]
		public string CategoryName { get; set; }

		[Display(Name = "類別")]
		public int CategoryId { get; set; } = 0;

		[Display(Name = "餐點名稱")]
		[Required]
		[StringLength(30)]
		public string Name { get; set; }

		[Display(Name = "描述")]
		[StringLength(50)]
		public string Description { get; set; }

		[Display(Name = "價格")]
		[Required]
		public int Price { get; set; }

		[Display(Name = "圖片")]
		// [Required]
		public string MealsImage { get; set; }


		[Display(Name = "上下架狀態")]
		public bool Status { get; set; }

		[Display(Name = "建立日期")]
		[DisplayFormat(DataFormatString = "{0:yyyy/MM/dd}", ApplyFormatInEditMode = true)]
		public DateTime CreatedDate { get; set; }

		[Display(Name = "修改日期")]
		[DisplayFormat(DataFormatString = "{0:yyyy/MM/dd}", ApplyFormatInEditMode = true)]
		public DateTime? ModifiedDate { get; set; }
	}
}