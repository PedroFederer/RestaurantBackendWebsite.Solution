using RestaurantBackendWebsite.Models.EFModels;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace RestaurantBackendWebsite.Models.DTOs
{
	public class OrderDTO
	{
		public int Id { get; set; }

		public int MemberId { get; set; }

		public int TableNums { get; set; }

		public DateTime OrderTime { get; set; }

		public DateTime ReservationTime { get; set; }

		public int AdvancePayment { get; set; }

		public int Diners { get; set; }

		public int TotalPrice { get; set; }

		public bool? IsCancel { get; set; }

		public bool? IsRefund { get; set; }

		[Required]
		[StringLength(10)]
		public string PhoneNumber { get; set; }

		public virtual Member Member { get; set; }


	}
}