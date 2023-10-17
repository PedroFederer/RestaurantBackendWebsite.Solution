using RestaurantBackendWebsite.Models.Criteria;
using RestaurantBackendWebsite.Models.DTOs;
using RestaurantBackendWebsite.Models.EFModels;
using RestaurantBackendWebsite.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace RestaurantBackendWebsite.Models.Services
{
	public class OrderService
	{
		public List<OrderDTO> GetOrders(OrderSearchCriteria criteria)
		{

			var orders = new OrderRepository().GetOrders(criteria);
			return orders.ToList();

		}
		public IEnumerable<OrderItemDTO> GetOrderItems(int? orderid)
		{
			var orderItems = new OrderRepository().GetOrderItems(orderid);
			return orderItems;
		}
	}
}