using RestaurantBackendWebsite.Models.Criteria;
using RestaurantBackendWebsite.Models.DTOs;
using RestaurantBackendWebsite.Models.EFModels;
using RestaurantBackendWebsite.Models.Entities;
using RestaurantBackendWebsite.Models.Utilities;
using RestaurantBackendWebsite.Models.ViewModels;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace RestaurantBackendWebsite.Repositories
{
	public class OrderRepository
	{
		public IEnumerable<OrderDTO> GetOrders(OrderSearchCriteria criteria)
		{

			var db = new AppDbContext();
			IQueryable<Order> orders = db.Orders;
			if (!string.IsNullOrEmpty(criteria.Name))
			{
				orders = orders.Where(o => o.Member.Name.ToLower().Contains(criteria.Name.ToLower()));
			}
			if (!string.IsNullOrEmpty(criteria.Phone))
			{
				orders = orders.Where(o => o.PhoneNumber.Contains(criteria.Phone));
			}
			if (criteria.Time.HasValue)
			{
				orders = orders.Where(o => o.ReservationTime.Year == criteria.Time.Value.Year &&
										   o.ReservationTime.Month == criteria.Time.Value.Month &&
										   o.ReservationTime.Day == criteria.Time.Value.Day);
			}
			if (criteria.Iscancel.HasValue)
			{
				if (criteria.Iscancel == 1)
				{
					orders = orders.Where(o => o.IsCancel == false);
				}
				if (criteria.Iscancel == 2)
				{
					orders = orders.Where(o => o.IsCancel == true);

				}
				if (criteria.Isrefund.HasValue)
				{
					if (criteria.Isrefund == 1)
					{
						orders = orders.Where(o => o.IsRefund == false);
					}
					if (criteria.Isrefund == 2)
					{
						orders = orders.Where(o => o.IsRefund == true);
					}
				}

				if (criteria.Orderbymoney == 1)
				{
					orders = orders.OrderByDescending(o => o.TotalPrice);
				}
				if (criteria.Orderbymoney == 2)
				{
					orders = orders.OrderBy(o => o.TotalPrice);
				}
				if (criteria.Orderbytime == 1)
				{
					orders = orders.OrderByDescending(o => o.ReservationTime);
				}
				if (criteria.Orderbytime == 2)
				{
					orders = orders.OrderBy(o => o.ReservationTime);
				}


			}
			var orderDtos = new List<OrderDTO>();
			if (orders != null)
			{
				orderDtos = orders.Select(o => new OrderDTO
				{
					Id = o.Id,
					Member = o.Member,
					MemberId = o.MemberId,
					OrderTime = o.OrderTime,
					ReservationTime = o.ReservationTime,
					AdvancePayment = o.AdvancePayment,
					TotalPrice = o.TotalPrice,
					TableNums = o.TableNums,
					PhoneNumber = o.PhoneNumber,
					Diners = o.Diners,
					IsRefund = o.IsRefund,
					IsCancel = o.IsCancel,
				}).ToList();
			}

			return orderDtos;


		}


		public IEnumerable<OrderItemDTO> GetOrderItems(int? orderid)
		{
			var sql = @"SELECT * FROM OrderItems AS oi INNER JOIN OrderItemDetail AS oid ON oi.Id = oid.OrderItemId WHERE oi.OrderId = @OrderId";
			using (var conn = new SqlDb().GetConnection("AppDbContext"))
			{
				conn.Open();
				using (var command = new SqlCommand(sql, conn))
				{
					SqlParameter p1 = new SqlParameter("@OrderId", orderid);

					Dictionary<int, Models.Entities.OrderItem> orderitems = new Dictionary<int, Models.Entities.OrderItem>();
					command.Parameters.Add(p1);

					using (var reader = command.ExecuteReader())
					{
						while (reader.Read())
						{
							Models.Entities.OrderItem item = new Models.Entities.OrderItem
							{
								Id = reader.GetInt32(reader.GetOrdinal("Id")),
								OrderId = reader.GetInt32(reader.GetOrdinal("OrderId")),
								OrderItemDetails = new List<OrderItemDetail>()
							};
							if (!orderitems.ContainsKey(item.Id))
							{
								orderitems.Add(item.Id, item);
							}
							OrderItemDetail detail = new OrderItemDetail
							{
								OrderItemId = reader.GetInt32(reader.GetOrdinal("OrderItemId")),
								MealId = reader.GetInt32(reader.GetOrdinal("MealId")),
								MealName = reader.GetString(reader.GetOrdinal("MealName")),
								MealPrice = reader.GetInt32(reader.GetOrdinal("MealPrice")),
							};
							orderitems[item.Id].OrderItemDetails.Add(detail);
						}
					}
					var orderitemsDto = orderitems.Values.ToList().Select(o => new OrderItemDTO
					{
						Id = o.Id,
						OrderId = o.OrderId,
						OrderItemDetails = o.OrderItemDetails
					});



					return orderitemsDto.ToList();
				}

			}
		}
	}
}