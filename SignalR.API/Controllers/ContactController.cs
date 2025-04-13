using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SignalR.BusinessLayer.Abstract;
using SignalR.DataTransferObjectsLayer.ContactDto;
using SignalR.EntityLayer.Entities;

namespace SignalR.API.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class ContactController : ControllerBase
	{
		private readonly IContactService _contactService;
		private readonly IMapper _mapper;

		public ContactController(IContactService contactService, IMapper mapper)
		{
			_contactService = contactService;
			_mapper = mapper;
		}

		[HttpGet]
		public IActionResult ContactList()
		{
			var value = _mapper.Map<List<ResultContactDto>>(_contactService.TGetListAll());
			return Ok(value);
		}

		[HttpPost]
		public IActionResult CreateContact(CreateContactDto createContactDto)
		{
			if (!ModelState.IsValid)
			{
				return BadRequest();
			}

			_contactService.TAdd(new Contact()
			{
				Email = createContactDto.Email,
				Location = createContactDto.Location,
				Phone = createContactDto.Phone,
				FooterDescription = createContactDto.FooterDescription
			});

			return Ok("İletişim bilgisi başarılı bir şekilde eklendi.");
		}

		[HttpDelete("{id}")]
		public IActionResult DeleteContact(int id)
		{
			var value = _contactService.TGetById(id);
			_contactService.TDelete(value);
			return Ok("İletişim bilgisi başarılı bir şekilde silindi.");
		}

		[HttpGet("{id}")]
		public IActionResult GetContact(int id)
		{
			var value = _contactService.TGetById(id);

			if (value == null)
			{
				return NotFound();
			}

			var result = _mapper.Map<ResultContactDto>(value);
			return Ok(result);
		}

		[HttpPut]
		public IActionResult UpdateContact(UpdateContactDto updateContactDto)
		{
			var value = _contactService.TGetById(updateContactDto.ContactId);

			if (value == null)
			{
				return NotFound("İletişim bilgisi bulunamadı!");
			}

			value.Email = updateContactDto.Email;
			value.Location = updateContactDto.Location;
			value.Phone = updateContactDto.Phone;
			value.FooterDescription = updateContactDto.FooterDescription;
			value.ContactId = updateContactDto.ContactId;

			_contactService.TUpdate(value);

			return Ok("İletişim bilgisi başarılı bir şekilde güncellendi.");
		}
	}
}
