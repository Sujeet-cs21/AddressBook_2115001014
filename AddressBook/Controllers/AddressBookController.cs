using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using RepositoryLayer.Context;
using RepositoryLayer.Entity;
using System;

namespace AddressBook.Controllers;

[ApiController]
[Route("[controller]")]
public class AddressBookController : ControllerBase
{
    private readonly AddressBookContext _context;

    public AddressBookController(AddressBookContext context)
    {
        _context = context;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<AddressBookEntity>>> GetContacts()
    {
        return await _context.Addresses.ToListAsync();
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<AddressBookEntity>> GetContact(int id)
    {
        var contact = await _context.Addresses.FindAsync(id);
        if (contact == null) return NotFound();
        return contact;
    }

    [HttpPost]
    public async Task<ActionResult<AddressBookEntity>> CreateContact(AddressBookEntity contact)
    {
        _context.Addresses.Add(contact);
        await _context.SaveChangesAsync();
        return CreatedAtAction(nameof(GetContact), new { id = contact.Id }, contact);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateContact(int id, AddressBookEntity contact)
    {
        if (id != contact.Id) return BadRequest();
        _context.Entry(contact).State = EntityState.Modified;
        await _context.SaveChangesAsync();
        return NoContent();
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteContact(int id)
    {
        var contact = await _context.Addresses.FindAsync(id);
        if (contact == null) return NotFound();

        _context.Addresses.Remove(contact);
        await _context.SaveChangesAsync();
        return NoContent();
    }

}
