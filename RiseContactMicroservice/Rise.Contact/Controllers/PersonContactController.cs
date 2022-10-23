using System.Net;
using DotNetCore.CAP;
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
        private readonly IPersonContactService _personContactService;
        private readonly ICapPublisher _capBus;

        public PersonContactController(IPersonContactService personContactService, ICapPublisher capBus)
        {
            _personContactService = personContactService;
            _capBus = capBus;
        }

        [HttpGet("GetAllPersons")]
        [ProducesResponseType(typeof(IEnumerable<PersonWithoutDetailDto>), (int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        public async Task<IActionResult> GetAllPersons()
        {
            var persons = await _personContactService.GetAllPersons();
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
            var persons = await _personContactService.GetAllWithDetailPersons();
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

            var saveResult = await _personContactService.CreatedPerson(person);

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

            var saveResult = await _personContactService.CreatePersonWithContact(person);

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

            var deleteResult = await _personContactService.DeletePerson(personId);
            return Ok(deleteResult);

        }
        [HttpDelete("DeletePersonContact")]
        [ProducesResponseType(typeof(bool), (int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        public async Task<IActionResult> DeletePersonContact(string personId, string contactId)
        {
            if (string.IsNullOrEmpty(contactId))
            {
                return BadRequest();
            }

            var deleteResult = await _personContactService.DeletePersonContact(personId, contactId);
            return Ok(deleteResult);

        }
        [HttpPost("CreatePersonContact")]
        [ProducesResponseType(typeof(PersonDto), (int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        public async Task<IActionResult> CreatePersonContact(CreatePersonContactDto contact)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }

            var saveResult = await _personContactService.AddPersonContact(contact);
            if (saveResult == null)
            {
                return BadRequest();
            }

            return Ok(saveResult);
        }


    }
}
