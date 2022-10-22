using System.ComponentModel.DataAnnotations;

namespace Rise.Shared.Dtos;

public class CreatePersonWithContactDto
{
    [Required]
    public string Name { get; set; }
    [Required]
    public string Surname { get; set; }
    [Required]
    public string Company { get; set; }
    [Required]
    public List<CreateContactDto> Contacts { get; set; }
}