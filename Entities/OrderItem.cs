using System.ComponentModel.DataAnnotations.Schema;

namespace Entities
{
    public class OrderItem : BaseEntity
    {
        public int OrderID { get; set; }

        [NotMapped]

        public string? ProductName { get; set; }
        public int ProductID { get; set; }

        public virtual Product Product { get; set; } = null!;

        public decimal ItemPrice { get; set; }
        public int Quantity { get; set; }





    }









}
