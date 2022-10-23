using Rise.MongoDb.Entity.Abstract;

namespace Rise.MongoDb.Entity.Concreate;

public class Person : MongoEntityBase
{
    public string Name { get; set; }
    public string Surname { get; set; }
    public string Company { get; set; }

    public IList<Contact> Contacts { get; set; }

    public Person()
    {
        this.Contacts = new List<Contact>();   
    }
}