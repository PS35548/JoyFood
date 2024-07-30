using System.Collections.Generic;

namespace ps35548_asm.Models
{
    public partial class Product
    {
        public int ProductId { get; set; }

        public string ProductName { get; set; } = null!;

        public string? Description { get; set; }

        public decimal Price { get; set; }

        public int Quantity { get; set; }

        public int? CategoryId { get; set; }

        public string? Img { get; set; }

        public virtual ICollection<CartDetail> CartDetails { get; set; } = new List<CartDetail>();

        public virtual Category? Category { get; set; }

        // Thêm các thuộc tính cho OrderItem để thiết lập mối quan hệ
        public virtual ICollection<OrderItem> OrderItems { get; set; } = new List<OrderItem>();
    }
}
