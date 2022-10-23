using AutoMapper;
using MongoDB.Driver;
using Rise.Contact.DataAccess.Abstract;
using Rise.MongoDb.Entity.Concreate;
using Rise.Shared.Abstract;
using Rise.Shared.Dtos;
using Rise.Shared.Enums;

namespace Rise.Contact.DataAccess.Concreate;

public class PersonContactService : IPersonContactService
{
    private readonly IMongoCollection<Person> _personCollection;
    private readonly IMapper _mapper;
    public PersonContactService(IMongoDatabaseSettings databaseSettings, IMapper mapper)
    {
        _mapper = mapper;
        var mongoClient = new MongoClient(databaseSettings.ConnectionString);
        var mongoDb = mongoClient.GetDatabase(databaseSettings.DatabaseName);
        _personCollection = mongoDb.GetCollection<Person>(databaseSettings.PersonCollectionName);
    }

    public async Task<PersonDto> GetPerson(string id)
    {
        var person = await _personCollection.Find(x => x.Id == id).FirstOrDefaultAsync();
        return _mapper.Map<PersonDto>(person);
    }

    public async Task<PersonDto> CreatedPerson(CreatePersonDto input)
    {
        var person = _mapper.Map<Person>(input);
        await _personCollection.InsertOneAsync(person);
        return _mapper.Map<PersonDto>(person);
    }

    public async Task<PersonDto> CreatePersonWithContact(CreatePersonWithContactDto input)
    {
        var person = _mapper.Map<Person>(input);
        await _personCollection.InsertOneAsync(person);
        return _mapper.Map<PersonDto>(person);
    }

    public async Task<bool> DeletePerson(string id)
    {
        var result = await _personCollection.DeleteOneAsync(x => x.Id == id);
        return result.DeletedCount > 0;
    }

    public async Task<List<PersonWithoutDetailDto>> GetAllPersons()
    {
        var persons = await _personCollection.Find(x => true).ToListAsync();

        await FakePersonData(persons);

        return _mapper.Map<List<PersonWithoutDetailDto>>(persons);
    }

    private async Task FakePersonData(List<Person> persons)
    {
        if (persons.Count <= 0)
        {
            for (int i = 0; i < 200; i++)
            {
                var newPerson = new CreatePersonWithContactDto()
                {
                    Name = Faker.Name.First(),
                    Surname = Faker.Name.Last(),
                    Company = Faker.Company.Name(),
                    Contacts = new List<CreateContactDto>()
                    {
                        new CreateContactDto()
                        {
                            ContactData = Faker.Phone.Number(),
                            ContactType = ContactType.Phone
                        },
                        new CreateContactDto()
                        {
                            ContactData = Faker.Country.Name(),
                            ContactType = ContactType.Location
                        }
                    }
                };


                var person = _mapper.Map<Person>(newPerson);
                await _personCollection.InsertOneAsync(person);
            }
        }
        persons = await _personCollection.Find(x => true).ToListAsync();
    }

    public async Task<List<PersonDto>> GetAllWithDetailPersons()
    {
        var persons = await _personCollection.Find(x => true).ToListAsync();
        await FakePersonData(persons);
        return _mapper.Map<List<PersonDto>>(persons);
    }

    public async Task<ContactDto> AddPersonContact(CreatePersonContactDto contactInput)
    {
        var person = await _personCollection.Find(x => x.Id == contactInput.PersonId).FirstOrDefaultAsync();
        var contact = _mapper.Map<MongoDb.Entity.Concreate.Contact>(contactInput);
        person.Contacts.Add(contact);
        await _personCollection.ReplaceOneAsync(x => x.Id == person.Id, person);

        return _mapper.Map<ContactDto>(contact);
    }

    public async Task<bool> DeletePersonContact(string personId, string contactId)
    {
        var person = await _personCollection.Find(x => x.Id == personId).FirstOrDefaultAsync();

        var contact = person.Contacts.FirstOrDefault(x => x.Id == contactId);
        if (contact != null)
        {
            person.Contacts.Remove(contact);
            var result = await _personCollection.ReplaceOneAsync(x => x.Id == person.Id, person);

            return result.ModifiedCount>0;

        }

        return false;
    }
}