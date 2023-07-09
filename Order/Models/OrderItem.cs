using System.ComponentModel.DataAnnotations;

namespace Order.Models
{
    public class OrderItem
    {
        public int Id { get; set; }
   
       // [Compare(nameof(Orders.Number), ErrorMessage = "Name не может быть равен Number.")]

        public int Name { get; set; }
        public float Quantity { get; set; }
        public int Unit { get; set; }
        public int OrderId { get; set; }
        public Orders Order { get; set; }
    }
}
