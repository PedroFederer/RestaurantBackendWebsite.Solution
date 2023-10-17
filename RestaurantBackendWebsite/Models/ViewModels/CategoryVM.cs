using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace RestaurantBackendWebsite.Models.ViewModels
{
	public class CategoryVM
	{
		public int Id { get; set; }

		[Display(Name = "分類名稱")]
		[Required]
		[StringLength(30)]
		public string Name { get; set; }


		[Display(Name = "排序")]
		[Required]
		[Range(1, 1000)]
		public int DisplayOrder { get; set; }

		[Display(Name = "建立日期")]
		[DisplayFormat(DataFormatString = "{0:yyyy/MM/dd}", ApplyFormatInEditMode = true)]
		public DateTime CreatedDate { get; set; }

		[Display(Name = "修改日期")]
		[DisplayFormat(DataFormatString = "{0:yyyy/MM/dd}", ApplyFormatInEditMode = true)]
		public DateTime? ModifiedDate { get; set; }
	}
}