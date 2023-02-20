namespace SmallStoreManagementSystem.Models
{
    public class UserProductHistory
    {        
        public int Id { get; set; }
        public string Name { get; set; }
        public string ProductName { get; set; }
        public int? ProductId { get; set; }
        public DateTime DateViewed { get; set; }
    }
}