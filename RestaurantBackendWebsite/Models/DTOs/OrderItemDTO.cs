using RestaurantBackendWebsite.Models.EFModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace RestaurantBackendWebsite.Models.DTOs
{
	public class OrderItemDTO
	{
		public int Id { get; set; }

		public int OrderId { get; set; }


		public virtual List<OrderItemDetail> OrderItemDetails { get; set; }
	}
}