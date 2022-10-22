using Rise.Shared.Enums;
using System.ComponentModel.DataAnnotations;

namespace Rise.Shared.Dtos;

public class CreateContactDto
{
    [Required]
    public ContactType ContactType { get; set; }
    [Required]
    public string ContactData { get; set; }
}