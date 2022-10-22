using Rise.Shared.Abstract;
using Rise.Shared.Enums;
using System.ComponentModel.DataAnnotations;

namespace Rise.Shared.Dtos;

public class ContactDto : EntityBaseDto
{
    public ContactType ContactType { get; set; }
    public string ContactData { get; set; }
}