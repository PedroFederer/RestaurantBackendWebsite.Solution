
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace RestaurantBackendWebsite.Models.ViewModels
{
	public class OrderVm
	{
		public int Id { get; set; }

		[Display(Name = "會員編號")]
		public int MemberId { get; set; }

		[Display(Name = "幾人桌")]
		public int TableNums { get; set; }

		[Display(Name = "訂單時間")]
		public DateTime OrderTime { get; set; }

		[Display(Name = "預約時間")]
		public DateTime ReservationTime { get; set; }

		[Display(Name = "訂金")]
		public int AdvancePayment { get; set; }

		[Display(Name = "用餐人數")]
		public int Diners { get; set; }

		[Display(Name = "總金額")]
		public int TotalPrice { get; set; }

		[Display(Name = "是否取消訂單")]
		public bool? IsCancel { get; set; }

		[Display(Name = "是否退訂金")]
		public bool? IsRefund { get; set; }

		[Required]
		[StringLength(10)]
		[Display(Name = "連絡電話")]
		public string PhoneNumber { get; set; }

		[Display(Name = "會員姓名")]
		public string MemberName { get; set; }
	}
}