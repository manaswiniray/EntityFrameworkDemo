using EntityFrameworkDemo.Data;
using Microsoft.AspNetCore.Mvc;
using EntityFrameworkDemo.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Http.HttpResults;

namespace EntityFrameworkDemo.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ContactsController : Controller
    {
        private readonly ContactsAPIDBContext _contactsAPIDBContext;
        public ContactsController(ContactsAPIDBContext contactsAPIDBContext)
        {
            _contactsAPIDBContext = contactsAPIDBContext;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllContacts()
        {
            return Ok(await _contactsAPIDBContext.ContactList.ToListAsync());

        }

        [HttpGet]
        [Route("{id:guid}")]
        public async Task<IActionResult> GetContacts([FromRoute] Guid id)
        {
            var contact=await _contactsAPIDBContext.ContactList.FindAsync(id);

            if(contact == null)
            {
                return NotFound();
            }

            return Ok(contact);
        }

        [HttpPost]
        public async Task<IActionResult> AddContact(AddContanctRequest addContanctRequest)
        {
            var contact = new Contact()
            {
                Id=Guid.NewGuid(),
                Name=addContanctRequest.Name,
                Email=addContanctRequest.Email,
            };

            await _contactsAPIDBContext.ContactList.AddAsync(contact);
            await _contactsAPIDBContext.SaveChangesAsync();

            return Ok(contact);
        }

        [HttpPut]
        [Route("{id:guid}")]
        public async Task<IActionResult> UpdateContact([FromRoute] Guid id, UpdateContactRequest updateContactRequest)
        {
            var contact=await _contactsAPIDBContext.ContactList.FindAsync(id);

            if (contact != null)
            {
                contact.Name = updateContactRequest.Name;
                contact.Email = updateContactRequest.Email;

                await _contactsAPIDBContext.SaveChangesAsync();

                return Ok(contact);
            }

            return NotFound();
        }


        [HttpDelete]
       [ Route("{id:guid}")]
        public async Task<IActionResult> DeleteContact([FromRoute] Guid id)
        {
            var contact = await _contactsAPIDBContext.ContactList.FindAsync(id);

            if (contact != null)
            {
                _contactsAPIDBContext.Remove(contact);
                await _contactsAPIDBContext.SaveChangesAsync();
                return Ok(contact);
            }

            return NotFound();
        }
        
    }
}
