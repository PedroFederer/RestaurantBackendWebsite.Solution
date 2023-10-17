namespace RestaurantBackendWebsite.Models.EFModels
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class Order
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Order()
        {
            OrderItems = new HashSet<OrderItem>();
        }

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

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<OrderItem> OrderItems { get; set; }
    }
}
