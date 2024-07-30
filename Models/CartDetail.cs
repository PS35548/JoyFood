using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ps35548_asm.Models
{
    public class CartDetail
    {
        [Key]
        public int CartDetailId { get; set; }

        public int CartId { get; set; }
        public int ProductId { get; set; }

        [ForeignKey("CartId")]
        public virtual Cart Cart { get; set; }

        [ForeignKey("ProductId")]
        public virtual Product Product { get; set; }

        public int Quantity { get; set; }
    }
}
