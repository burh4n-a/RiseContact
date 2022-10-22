using Rise.Shared.Dtos;

namespace Rise.Contact.DataAccess.Abstract;

public interface IPersonService
{
    Task<PersonDto> GetPerson(string id);
    Task<PersonDto> CreatedPerson(CreatePersonDto input);
    Task<PersonDto> CreatePersonWithContact(CreatePersonWithContactDto input);
    Task<bool> DeletePerson(string id);
    Task<List<PersonWithoutDetailDto>> GetAllPersons();
    Task<List<PersonDto>> GetAllWithDetailPersons();
}