using Rise.Shared.Enums;

namespace Rise.Shared.Dtos;

public class ContactDto
{
    public ContactType ContactType { get; set; }
    public string ContactData { get; set; }
}