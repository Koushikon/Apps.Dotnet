using Microsoft.AspNetCore.Mvc;
using Web.Api.UI.Models;
using Web.Api.UI.Services;

namespace Web.Api.UI.Controllers;

[Route("api/[controller]")]
[ApiController]
public class ContactsController : ControllerBase
{
	private readonly ContactService _contactService;

	public ContactsController(ContactService contactService)
	{
		_contactService = contactService;
	}

	// GET: api/Contacts
	[HttpGet]
	public async Task<List<ContactModel>> GetAll()
	{
		return await _contactService.GetAllContacts();
	}


	// GET api/Contacts/filter?firstname=golem
	[HttpGet("filter")]
	public async Task<ActionResult<List<ContactModel>>> GetByFirstName(string firstname)
	{
		var contact = await _contactService.GetContactByFirstName(firstname);

		if (contact.Count == 0)
		{
			return NotFound();
		}

		return contact;
	}


	// GET api/Contacts/5
	[HttpGet("{id:length(24)}")]
	public async Task<ActionResult<ContactModel>> GetById(string id)
	{
		var contact = await _contactService.GetContactById(id);

		if (contact is null)
		{
			return NotFound();
		}

		return contact;
	}


	// POST api/Contacts
	// Id needs to manually put 24 digit hex number
	[HttpPost]
	public async Task<IActionResult> Post(ContactModel contact)
	{
		await _contactService.CreateContact(contact);
		return CreatedAtAction(nameof(GetById), new { id = contact.Id }, contact);
	}


	// PUT api/Contacts/5
	[HttpPut("{id:length(24)}")]
	public async Task<IActionResult> Put(string id, ContactModel updatedcontact)
	{
		var contact = await _contactService.GetContactById(id);

		if (contact is null)
		{
			return NotFound();
		}

		updatedcontact.Id = contact.Id;
		await _contactService.UpdateContact(id, updatedcontact);
		return NoContent();
	}


	// DELETE api/Contacts/5
	[HttpDelete("{id:length(24)}")]
	public async Task<IActionResult> Delete(string id)
	{
		var contact = await _contactService.GetContactById(id);

		if (contact is null)
		{
			return NotFound();
		}

		await _contactService.RemoveContact(id);
		return NoContent();
	}
}