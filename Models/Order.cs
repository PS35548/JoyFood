using System.ComponentModel.DataAnnotations;

namespace ps35548_asm.Models
{
    public class Order
    {
        [Key]
        public int OrderId { get; set; }

        [Required]
        public string ShippingAddress { get; set; }

        [Required]
        public string PaymentMethod { get; set; }

        public decimal TotalAmount { get; set; }

        public DateTime OrderDate { get; set; } = DateTime.Now;

        public List<OrderItem> OrderItems { get; set; } = new List<OrderItem>();
    }
}
