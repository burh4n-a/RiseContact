using AutoMapper;
using MongoDB.Driver;
using Rise.Contact.DataAccess.Abstract;
using Rise.Contact.Entities;
using Rise.Shared.Abstract;
using Rise.Shared.Dtos;

namespace Rise.Contact.DataAccess.Concreate;

public class PersonService : IPersonService
{
    private readonly IMongoCollection<Person> _personCollection;
    private readonly IMongoCollection<Entities.Contact> _contactCollection;
    private readonly IMapper _mapper;
    public PersonService(IMongoDatabaseSettings databaseSettings, IMapper mapper)
    {
        _mapper = mapper;
        var mongoClient = new MongoClient(databaseSettings.ConnectionString);
        var mongoDb = mongoClient.GetDatabase(databaseSettings.DatabaseName);
        _personCollection = mongoDb.GetCollection<Person>(databaseSettings.PersonCollectionName);
        _contactCollection = mongoDb.GetCollection<Entities.Contact>(databaseSettings.ContactCollectionName);
    }

    public async Task<PersonDto> GetPerson(string id)
    {
        var person = await _personCollection.Find(x => x.Id == id).FirstOrDefaultAsync();
        person.Contacts = await _contactCollection.Find(x => x.PersonId == id).ToListAsync();
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
        foreach (var contact in person.Contacts)
        {
            contact.PersonId = person.Id;
        }
        await _contactCollection.InsertManyAsync(person.Contacts);
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
        return _mapper.Map<List<PersonWithoutDetailDto>>(persons);
    }

    public async Task<List<PersonDto>> GetAllWithDetailPersons()
    {
        var persons = await _personCollection.Find(x => true).ToListAsync();
        var personIds = persons.Select(x => x.Id).ToList();
        var contatcData = await _contactCollection.Find(x => personIds.Contains(x.PersonId)).ToListAsync();
        foreach (var person in persons)
        {
            person.Contacts = contatcData.Where(x => x.PersonId == person.Id).ToList();
        }
        return _mapper.Map<List<PersonDto>>(persons);
    }
}