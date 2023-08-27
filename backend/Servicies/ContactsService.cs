using AutoMapper;
using backend.Data;
using backend.Data.DTOs.Request;
using backend.Data.DTOs.Response;
using backend.Exceptions;
using Microsoft.EntityFrameworkCore;

namespace backend.Servicies
{
    public class ContactsService
    {
        private readonly DataBaseContext _dbContext;
        private readonly IMapper _mapper;

        public ContactsService(DataBaseContext dbContext, IMapper mapper)
        {
            _dbContext = dbContext;
            _mapper = mapper;
        }

        public async Task<List<Contact>> GetAllContactsAsync()
        {
            return await _dbContext.Contacts.ToListAsync();
        }

        public async Task<ContactResponseDTO?> GetContactByIdAsync(int id)
        {
            var contact = await _dbContext.Contacts.FirstOrDefaultAsync(x => x.Id == id);

            if (contact == null) throw new ContactNotFoundException(id);

            return _mapper.Map<ContactResponseDTO>(contact);
        }

        public async Task CreateContactAsync(ContactRequestDTO contactRequest)
        {
            var contact = _mapper.Map<Contact>(contactRequest);
            await _dbContext.Contacts.AddAsync(contact);
            await _dbContext.SaveChangesAsync();
        }

        public async Task UpdateContactAsync(int id, ContactRequestDTO contactRequest)
        {
            var findContact = await _dbContext.Contacts.FirstOrDefaultAsync(x => x.Id == id);

            if (findContact == null) throw new ContactNotFoundException(id);

            var contact = _mapper.Map<Contact>(contactRequest);
            contact.Id = id;
            _dbContext.Contacts.Update(contact);
            await _dbContext.SaveChangesAsync();
        }

        public async Task DeleteContactAsync(int id)
        {
            var contactToDelete = await _dbContext.Contacts.FindAsync(id);

            if (contactToDelete != null)
            {
                _dbContext.Contacts.Remove(contactToDelete);
                await _dbContext.SaveChangesAsync();
            }

            throw new ContactNotFoundException(id);
        }
    }
}
