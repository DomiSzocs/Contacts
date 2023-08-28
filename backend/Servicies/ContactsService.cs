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

        public async Task<List<ContactShortResponseDTO>> GetAllContactsAsync()
        {
            var contacts = await _dbContext.Contacts.ToArrayAsync();
            return contacts.Select(contact => _mapper.Map<ContactShortResponseDTO>(contact)).ToList();
        }

        public async Task<ContacDetailResponseDTO?> GetContactByIdAsync(int id)
        {
            var contact = await _dbContext.Contacts.FirstOrDefaultAsync(x => x.Id == id);

            if (contact == null) throw new ContactNotFoundException(id);

            return _mapper.Map<ContacDetailResponseDTO>(contact);
        }

        public async Task CreateContactAsync(ContactDetailRequestDTO contactRequest)
        {
            var contact = _mapper.Map<Contact>(contactRequest);
            await _dbContext.Contacts.AddAsync(contact);
            await _dbContext.SaveChangesAsync();
        }

        public async Task UpdateContactAsync(int id, ContactDetailRequestDTO contactRequest)
        {
            var contact = await _dbContext.Contacts.FirstOrDefaultAsync(x => x.Id == id);

            if (contact == null) throw new ContactNotFoundException(id);

            _mapper.Map(contactRequest, contact);
            contact.Id = id;
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
