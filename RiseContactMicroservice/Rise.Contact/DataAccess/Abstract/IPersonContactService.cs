using Rise.Shared.Dtos;

namespace Rise.Contact.DataAccess.Abstract;

public interface IPersonContactService
{
    Task<PersonDto> GetPerson(string id);
    Task<PersonDto> CreatedPerson(CreatePersonDto input);
    Task<PersonDto> CreatePersonWithContact(CreatePersonWithContactDto input);
    Task<bool> DeletePerson(string id);
    Task<List<PersonWithoutDetailDto>> GetAllPersons();
    Task<List<PersonDto>> GetAllWithDetailPersons();
    Task<ContactDto> AddPersonContact(CreatePersonContactDto contactInput);
    Task<bool> DeletePersonContact(string personId,string contactId);
}