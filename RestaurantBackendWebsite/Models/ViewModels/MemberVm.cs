using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace RestaurantBackendWebsite.Models.ViewModels
{
	public class MemberVm
	{
		public int Id { get; set; }

		[Display(Name = "帳號")]
		[Required(ErrorMessage = "{0}必填")]
		[Remote("ValidateAccount", "Members", ErrorMessage = "{0}沒有通過驗證，請換另一個帳號")]
		public string Account { get; set; }

		[Display(Name = "姓名")]
		[Required]
		[Remote("ValidateName", "Members", AdditionalFields = nameof(Account), HttpMethod = "Post",
			ErrorMessage = "{0}沒有通過驗證，請換另一個帳號或姓名")]
		public string Name { get; set; }

		[Display(Name = "電子郵件")]
		[Required(ErrorMessage = "{0}必填")]
		[EmailAddress(ErrorMessage = "{0}格式有誤，請輸入正確的電子郵件格式")]
		public string Email { get; set; }

		[Display(Name = "電話")]
		[Required(ErrorMessage = "{0}必填")]
		[StringLength(10)]
		public string PhoneNumber { get; set; }


	}
}