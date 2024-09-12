using ContactManager.Context;
using ContactManager.Interfaces;
using ContactManager.Model;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace ContactManager.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ContactsController : ControllerBase
    {
        private readonly ContactContext _context;
        private readonly IContactService _contactValidationService;

        public ContactsController(ContactContext context, IContactService contactValidationService)
        {
            _context = context;
            _contactValidationService = contactValidationService;
        }

        // GET: api/Contacts
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Contact>>> GetContact()
        {
            if (_context.Contacts == null)
            {
                return NotFound();
            }
            return await _context.Contacts.ToListAsync();
        }

        // GET: api/Contacts/{id}
        [HttpGet("{id}")]
        public async Task<ActionResult<Contact>> GetContact(Guid id)
        {
            if (_context.Contacts == null)
            {
                return NotFound();
            }
            var contact = await _context.Contacts.FindAsync(id);

            if (contact == null)
            {
                return NotFound();
            }

            return contact;
        }

        // PUT: api/Contacts/{id}
        [HttpPut("{id}")]
        public async Task<IActionResult> PutContact(Guid id, Contact contact)
        {
            if (id != contact.Id)
            {
                return BadRequest();
            }

            _context.Entry(contact).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ContactExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/Contacts
        [HttpPost]
        public async Task<ActionResult<Contact>> PostContact(Contact contact)
        {
            if (_context.Contacts == null)
            {
                return Problem("Entity set 'ContactContext.Contact'  is null.");
            }

            if (!_contactValidationService.ValidateContact(contact))
            {
                return BadRequest();
            }

            _context.Contacts.Add(contact);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetContact", new { id = contact.Id }, contact);
        }

        // DELETE: api/Contacts/{id}
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteContact(Guid id)
        {
            if (_context.Contacts == null)
            {
                return NotFound();
            }
            var contact = await _context.Contacts.FindAsync(id);
            if (contact == null)
            {
                return NotFound();
            }

            _context.Contacts.Remove(contact);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool ContactExists(Guid id)
        {
            return (_context.Contacts?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
