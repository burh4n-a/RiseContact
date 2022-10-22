using Rise.Shared.Dtos;

namespace Rise.Contact.DataAccess.Abstract;

public interface IContactService
{
    Task<ContactDto> AddPersonContact(CreateContactDto contactInput);
    Task<bool> DeletePersonContact(string contactId);
}