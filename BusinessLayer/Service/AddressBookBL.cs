using AutoMapper;
using BusinessLayer.Interface;
using Microsoft.EntityFrameworkCore;
using ModelLayer.DTO;
using RepositoryLayer.Context;
using RepositoryLayer.Entity;
using System.Text.Json;

namespace BusinessLayer.Service
{
    public class AddressBookBL : IAddressBookBL
    {
        private readonly AddressBookContext _context;
        private readonly IMapper _mapper;
        private readonly ICacheService _cacheService;
        private const string CacheKey = "AddressBookEntries";

        public AddressBookBL(AddressBookContext context, IMapper mapper, ICacheService cacheService)
        {
            _context = context;
            _mapper = mapper;
            _cacheService = cacheService;
        }

        public async Task<IEnumerable<AddressBookDTO>> GetAllContacts()
        {
            var cachedData = await _cacheService.GetCachedData(CacheKey);
            if (!string.IsNullOrEmpty(cachedData))
            {
                return JsonSerializer.Deserialize<List<AddressBookDTO>>(cachedData);
            }

            var contacts = await _context.Addresses.ToListAsync();
            var mappedContacts = _mapper.Map<List<AddressBookDTO>>(contacts);

            await _cacheService.SetCachedData(CacheKey, JsonSerializer.Serialize(mappedContacts));

            return mappedContacts;
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

            await _cacheService.RemoveCachedData(CacheKey); // Invalidate cache

            return _mapper.Map<AddressBookDTO>(entity);
        }

        public async Task UpdateContact(int id, AddressBookDTO contact)
        {
            var entity = _mapper.Map<AddressBookEntity>(contact);
            entity.Id = id;
            _context.Entry(entity).State = EntityState.Modified;
            await _context.SaveChangesAsync();

            await _cacheService.RemoveCachedData(CacheKey); // Invalidate cache
        }

        public async Task DeleteContact(int id)
        {
            var contact = await _context.Addresses.FindAsync(id);
            if (contact != null)
            {
                _context.Addresses.Remove(contact);
                await _context.SaveChangesAsync();

                await _cacheService.RemoveCachedData(CacheKey); // Invalidate cache
            }
        }
    }
}
