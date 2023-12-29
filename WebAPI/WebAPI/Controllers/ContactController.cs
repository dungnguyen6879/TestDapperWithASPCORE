using Microsoft.AspNetCore.Mvc;
using System.Net;
using WebAPI.Models.DTO;
using WebAPI.Services;

namespace WebAPI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ContactController : ControllerBase
    {
        private readonly IContact _contactRepo;
        public ContactController(IContact contactRepo)
        {
            _contactRepo = contactRepo;
        }
        [HttpGet]
        public async Task<IActionResult> GetContacts()
        {
            try
            {
                var contacts = await _contactRepo.GetContacts();
                return Ok(contacts);
            }
            catch (Exception ex)
            {
                //log error
                return StatusCode(500, ex.Message);
            }
        }
        [HttpGet("{id}", Name = "ContactById")]
        public async Task<IActionResult> GetContacts(Guid id)
        {
            try
            {
                var contact = await _contactRepo.GetContact(id);
                if (contact == null)
                    return NotFound();

                return Ok(contact);
            }
            catch (Exception ex)
            {
                //log error
                return StatusCode(500, ex.Message);
            }
        }
        [HttpPost]
        public async Task<IActionResult> CreateContact(ContactDTO contact)
        {
            try
            {
                var createdContact = await _contactRepo.CreateContact(contact);
                return CreatedAtRoute("ContactById", new { id = createdContact.Id }, createdContact);
            }
            catch (Exception ex)
            {
                //log error
                return StatusCode(500, ex.Message);
            }
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateContact(Guid id, ContactDTO contact)
        {
            try
            {
                var dbContact = await _contactRepo.GetContact(id);
                if (dbContact == null)
                    return NotFound();

                await _contactRepo.UpdateContact(id, contact);
                return NoContent();
            }
            catch (Exception ex)
            {
                //log error
                return StatusCode(500, ex.Message);
            }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteContact(Guid id)
        {
            //Todo: .../Contact/{id}
            try
            {
                var dbContact = await _contactRepo.GetContact(id);
                if (dbContact == null)
                    return NotFound();

                await _contactRepo.DeleteContact(id);
                return NoContent();
            }
            catch (Exception ex)
            {
                //log error
                return StatusCode(500, ex.Message);
            }
        }

        [HttpPost]
        [Route("AddNewContacts")]
        public async Task<IActionResult> AddContact(ContactDTO contact)
        {
            try
            {
                var status = await _contactRepo.AddContactByProcedure(contact);
                if (status == 1)
                {
                    return Ok("New book Added successfully status is " + status);
                }
                else if (status == 2)
                {
                    return Ok("This book is already exist status is " + status);
                }
                else
                {
                    return Ok("Something went wrong");
                }
            }
            catch (Exception ex)
            {
                //log error
                return StatusCode(500, ex.Message);
            }
        }

        [HttpDelete]
        [Route("RemoveContact")]
        public async Task<IActionResult> RemoveContact(Guid id)
        {
            //Todo: .../Contact/RemoveContact?id={id}
            try
            {
                var status = await _contactRepo.RemoveContactByProcedure(id);
                if (status == 1)
                {
                    return Ok("Book Deleted successfully status is " + status);
                }
                else if (status == 2)
                {
                    return Ok("Invalid book id status is " + status);
                }
                else
                {
                    return Ok("Something went wrong");
                }
            }
            catch (Exception ex)
            {
                //log error
                return StatusCode(500, ex.Message);
            }
        }
    }
}
