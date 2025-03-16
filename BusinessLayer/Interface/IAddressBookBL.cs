using ModelLayer.DTO;

namespace BusinessLayer.Interface
{
    public interface IAddressBookBL
    {
        Task<IEnumerable<AddressBookDTO>> GetAllContacts();
        Task<AddressBookDTO> GetContactById(int id);
        Task<AddressBookDTO> CreateContact(AddressBookDTO contact);
        Task UpdateContact(int id, AddressBookDTO contact);
        Task DeleteContact(int id);
    }
}
