namespace Order.Models
{
    public class Provider
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public List<Orders> orders { get; set; }
    }
}
