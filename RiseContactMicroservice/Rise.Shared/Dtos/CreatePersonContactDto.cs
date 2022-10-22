using Rise.Shared.Enums;
using System.ComponentModel.DataAnnotations;

namespace Rise.Shared.Dtos;

public class CreatePersonContactDto
{
    [Required]
    public ContactType ContactType { get; set; }
    [Required]
    public string ContactData { get; set; }
    [Required]
    public string PersonId { get; set; }
}