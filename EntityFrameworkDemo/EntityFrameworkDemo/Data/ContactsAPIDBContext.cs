using EntityFrameworkDemo.Models;
using Microsoft.EntityFrameworkCore;

namespace EntityFrameworkDemo.Data
{
    public class ContactsAPIDBContext : DbContext
    {
        public ContactsAPIDBContext(DbContextOptions options):base(options) { }

        public DbSet<Contact> ContactList { get; set; }

        
    }

    
}
