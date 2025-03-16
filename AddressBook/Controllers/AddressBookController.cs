using BusinessLayer.Interface;
using Microsoft.AspNetCore.Mvc;
using ModelLayer.DTO;

namespace AddressBook.Controllers;

[ApiController]
[Route("[controller]")]
public class AddressBookController : ControllerBase
{
    private readonly IAddressBookBL _addressBook;

    public AddressBookController(IAddressBookBL addressBook)
    {
        _addressBook = addressBook;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<AddressBookDTO>>> GetContacts()
    {
        var contacts = await _addressBook.GetAllContacts();
        return Ok(contacts);
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<AddressBookDTO>> GetContact(int id)
    {
        var contact = await _addressBook.GetContactById(id);
        if (contact == null) return NotFound();
        return Ok(contact);
    }

    [HttpPost]
    public async Task<ActionResult<AddressBookDTO>> CreateContact(AddressBookDTO contact)
    {
        var newContact = await _addressBook.CreateContact(contact);
        return CreatedAtAction(nameof(GetContact), newContact);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateContact(int id, AddressBookDTO contact)
    {
        if (id <= 0) return BadRequest("Invalid contact ID");
        await _addressBook.UpdateContact(id, contact);
        return NoContent();
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteContact(int id)
    {
        await _addressBook.DeleteContact(id);
        return NoContent();
    }

}
