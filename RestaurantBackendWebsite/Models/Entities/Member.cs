using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace RestaurantBackendWebsite.Models.Entities
{
	public class Member
	{
		public int Id { get; set; }
		[Display(Name = "帳號")]
		public string Account { get; set; }
		[Display(Name = "姓名")]
		public string Name { get; set; }
		[Display(Name = "信箱")]
		public string Email { get; set; }
		[Display(Name = "電話")]
		public string PhoneNumber { get; set; }
	}
}