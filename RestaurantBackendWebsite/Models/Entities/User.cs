using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace RestaurantBackendWebsite.Models.Entities
{
	public class User
	{
		public int Id { get; set; }

		[Required]
		[StringLength(50)]
		public string Account { get; set; }

		[Required]
		[StringLength(50)]
		public string Password { get; set; }

		[Required]
		[StringLength(50)]
		public string Name { get; set; }

		public bool Enabled { get; set; }
	}
}