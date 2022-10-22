using Rise.Shared.Abstract;
using Rise.Shared.Enums;
using System.ComponentModel.DataAnnotations;

namespace Rise.Shared.Dtos;

public class ContactDto
{
    public string Id { get; set; }
    public ContactType ContactType { get; set; }
    public string ContactData { get; set; }
}