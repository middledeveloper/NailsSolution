namespace WebApplication.Models
{
    public class Photo
    {
        public int Id { get; set; }
        public User User { get; set; }
        public string Path { get; set; }
    }
}
