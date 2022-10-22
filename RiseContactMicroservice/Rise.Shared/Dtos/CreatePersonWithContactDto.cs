namespace Rise.Shared.Dtos;

public class CreatePersonWithContactDto
{
    public string Name { get; set; }
    public string Surname { get; set; }
    public string Company { get; set; }

    public List<CreateContactDto> Contacts { get; set; }
}