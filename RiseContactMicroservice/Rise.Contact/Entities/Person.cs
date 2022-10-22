using Rise.Contact.DataAccess.Abstract;

namespace Rise.Contact.Entities;

public class Person : MongoEntityBase
{
    public string Name { get; set; }
    public string Surname { get; set; }
    public string Company { get; set; }

    public List<Contact> Contacts { get; set; }
}