namespace EntityFrameworkDemo.Models
{
    public class UpdateContactRequest
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
    }
}
