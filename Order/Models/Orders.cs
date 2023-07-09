using System.ComponentModel.DataAnnotations;

namespace Order.Models
{
    public class Orders
    {
        public int Id { get; set; }
     
        public int Number { get; set; }
       

        public DateTime Date { get; set; } = DateTime.Now;

        public int ProviderId { get; set; }
        public Provider Provider { get; set; }
        public List<OrderItem> OrderItems { get; set; }
    }
}
