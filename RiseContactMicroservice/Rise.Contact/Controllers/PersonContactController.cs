using System.Net;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Rise.Contact.DataAccess.Abstract;
using Rise.Shared.Dtos;

namespace Rise.Contact.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class PersonContactController : ControllerBase
    {
        private readonly IPersonService _personService;
        private readonly IContactService _contactService;

        public PersonContactController(IPersonService personService, IContactService contactService)
        {
            _personService = personService;
            _contactService = contactService;
        }

        [HttpGet("GetAllPersons")]
        [ProducesResponseType(typeof(IEnumerable<PersonWithoutDetailDto>), (int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        public async Task<IActionResult> GetAllPersons()
        {
            var persons = await _personService.GetAllPersons();
            if (persons.Count <= 0)
            {
                return NotFound();
            }
            else
            {
                return Ok(persons);
            }
        }
        [HttpGet("GetAllPersonsWithDetail")]
        [ProducesResponseType(typeof(IEnumerable<PersonDto>), (int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        public async Task<IActionResult> GetAllPersonsWithDetail()
        {
            var persons = await _personService.GetAllWithDetailPersons();
            if (persons.Count <= 0)
            {
                return NotFound();
            }
            else
            {
                return Ok(persons);
            }
        }
        [HttpPost("CreatePerson")]
        [ProducesResponseType(typeof(PersonDto), (int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        public async Task<IActionResult> CreatePerson(CreatePersonDto person)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }

            var saveResult = await _personService.CreatedPerson(person);

            return Ok(saveResult);
        }
        [HttpPost("CreatePersonWithContact")]
        [ProducesResponseType(typeof(PersonDto), (int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        public async Task<IActionResult> CreatePersonWithContact(CreatePersonWithContactDto person)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }

            var saveResult = await _personService.CreatePersonWithContact(person);

            return Ok(saveResult);
        }
        [HttpDelete("DeletePerson")]
        [ProducesResponseType(typeof(bool), (int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        public async Task<IActionResult> DeletePerson(string personId)
        {
            if (string.IsNullOrEmpty(personId))
            {
                return BadRequest();
            }

            var deleteResult = await _personService.DeletePerson(personId);
            return Ok(deleteResult);

        }
        [HttpDelete("DeletePersonContact")]
        [ProducesResponseType(typeof(bool), (int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        public async Task<IActionResult> DeletePersonContact(string contactId)
        {
            if (string.IsNullOrEmpty(contactId))
            {
                return BadRequest();
            }

            var deleteResult = await _contactService.DeletePersonContact(contactId);
            return Ok(deleteResult);

        }
        [HttpPost("CreatePersonContact")]
        [ProducesResponseType(typeof(PersonDto), (int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        public async Task<IActionResult> CreatePersonContact(CreateContactDto contact)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }

            var saveResult = await _contactService.AddPersonContact(contact);
            if (saveResult== null)
            {
                return BadRequest();
            }

            return Ok(saveResult);
        }
    }
}
