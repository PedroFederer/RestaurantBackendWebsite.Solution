using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace RestaurantBackendWebsite.Models.Criteria
{
	public class OrderSearchCriteria
	{
		public string Name { get; set; }
		public string Phone { get; set; }
		public DateTime? Time { get; set; }
		public int? Iscancel { get; set; }
		public int? Isrefund { get; set; }
		public int? Orderbytime { get; set; }
		public int? Orderbymoney { get; set; }

	}
}