using WebAPI.Models;
using WebAPI.Models.DTO;

namespace WebAPI.Services
{
    public interface IContact
    {
        public Task<IEnumerable<Contact>> GetContacts();
        public Task<Contact> GetContact(Guid id);
        public Task<Contact> CreateContact(ContactDTO contact);
        public Task UpdateContact(Guid id, ContactDTO company);
        public Task DeleteContact(Guid id);
        public Task<Int16> AddContactByProcedure(ContactDTO contact);
        public Task<int> RemoveContactByProcedure(Guid id);
    }
}
