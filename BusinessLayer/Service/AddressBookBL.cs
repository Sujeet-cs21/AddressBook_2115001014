using AutoMapper;
using BusinessLayer.Interface;
using Microsoft.EntityFrameworkCore;
using ModelLayer.DTO;
using RepositoryLayer.Context;
using RepositoryLayer.Entity;

namespace BusinessLayer.Service
{
    public class AddressBookBL : IAddressBookBL
    {
        private readonly AddressBookContext _context;
        private readonly IMapper _mapper;

        public AddressBookBL(AddressBookContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<IEnumerable<AddressBookDTO>> GetAllContacts()
        {
            var contacts = await _context.Addresses.ToListAsync();
            return _mapper.Map<List<AddressBookDTO>>(contacts);
        }

        public async Task<AddressBookDTO> GetContactById(int id)
        {
            var contact = await _context.Addresses.FindAsync(id);
            return _mapper.Map<AddressBookDTO>(contact);
        }

        public async Task<AddressBookDTO> CreateContact(AddressBookDTO contact)
        {
            var entity = _mapper.Map<AddressBookEntity>(contact);
            _context.Addresses.Add(entity);
            await _context.SaveChangesAsync();
            return _mapper.Map<AddressBookDTO>(entity);
        }

        public async Task UpdateContact(int id, AddressBookDTO contact)
        {
            var entity = _mapper.Map<AddressBookEntity>(contact);
            entity.Id = id;
            _context.Entry(entity).State = EntityState.Modified;
            await _context.SaveChangesAsync();
        }

        public async Task DeleteContact(int id)
        {
            var contact = await _context.Addresses.FindAsync(id);
            if (contact != null)
            {
                _context.Addresses.Remove(contact);
                await _context.SaveChangesAsync();
            }
        }
    }
}
