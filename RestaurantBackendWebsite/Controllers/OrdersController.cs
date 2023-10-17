using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Net.NetworkInformation;
using System.Web;
using System.Web.Mvc;
using Microsoft.Ajax.Utilities;
using PagedList;
using RestaurantBackendWebsite.Models.EFModels;
using RestaurantBackendWebsite.Models.Services;
using RestaurantBackendWebsite.Models.ViewModels;
using PagedList.Mvc;
using RestaurantBackendWebsite.Models.DTOs;
using RestaurantBackendWebsite.Models.Criteria;

namespace RestaurantBackendWebsite.Controllers
{
	public class OrdersController : Controller
	{
		private AppDbContext db = new AppDbContext();

		// GET: Orders
		[Authorize]
		public ActionResult Index(string name, string phone, DateTime? time, int? iscancel, int? isrefund, int? orderbytime, int? orderbymoney, int page = 1)
		{
			var criteria = new OrderSearchCriteria
			{
				Name = name,
				Phone = phone,
				Time = time,
				Iscancel = iscancel,
				Isrefund = isrefund,
				Orderbytime = orderbytime,
				Orderbymoney = orderbymoney,
			};
			IEnumerable<OrderDTO> orders = new OrderService().GetOrders(criteria);//會在service驗證


			ViewBag.iscancel = iscancel;
			ViewBag.isrefund = isrefund;
			ViewBag.name = name;
			ViewBag.phone = phone;
			ViewBag.time = time;
			ViewBag.orderbytime = orderbytime;
			ViewBag.orderbymoney = orderbymoney;
			var vms = new List<OrderVm>();
			foreach (var order in orders)
			{
				var vm = new OrderVm
				{
					Id = order.Id,
					MemberId = order.MemberId,
					TableNums = order.TableNums,
					OrderTime = order.OrderTime,
					ReservationTime = order.ReservationTime,
					AdvancePayment = order.AdvancePayment,
					Diners = order.Diners,
					TotalPrice = order.TotalPrice,
					IsRefund = order.IsRefund,
					IsCancel = order.IsCancel,
					PhoneNumber = order.PhoneNumber,
					MemberName = order.Member.Name,

				};
				vms.Add(vm);

			}
			int currentPage = page < 1 ? 1 : page;
			int pageSize = 10;
			var result = vms.ToPagedList(currentPage, pageSize);

			return View(result);
		}





		// GET: Orders/Details/5
		[Authorize]
		public ActionResult OrderItems(int? id)
		{
			if (id == null)
			{
				return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
			}

			IEnumerable<OrderItemDTO> orderItems = new OrderService().GetOrderItems(id);

			var vms = orderItems.Select(o => new OrderItemVm
			{
				Id = o.Id,
				OrderId = o.OrderId,
				OrderItemDetails = o.OrderItemDetails,
			});


			return View(vms.ToList());
		}


	}
}
