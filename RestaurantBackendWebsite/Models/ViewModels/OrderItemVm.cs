using RestaurantBackendWebsite.Models.EFModels;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace RestaurantBackendWebsite.Models.ViewModels
{
	public class OrderItemVm
	{
		public int Id { get; set; }
		[Display(Name = "訂單編號")]
		public int OrderId { get; set; }

		[System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
		public virtual ICollection<OrderItemDetail> OrderItemDetails { get; set; }

		public virtual Order Order { get; set; }
	}
}