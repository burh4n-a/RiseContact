using Rise.Contact.DataAccess.Abstract;
using Rise.Shared.Enums;

namespace Rise.Contact.Entities;

public class Contact : MongoEntityBase
{
    public ContactType ContactType { get; set; }
    public string ContactData { get; set; }
}