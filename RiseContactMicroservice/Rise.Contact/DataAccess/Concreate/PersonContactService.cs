using AutoMapper;
using MongoDB.Driver;
using Rise.Contact.DataAccess.Abstract;
using Rise.Contact.Entities;
using Rise.Shared.Abstract;
using Rise.Shared.Dtos;

namespace Rise.Contact.DataAccess.Concreate;

public class PersonContactService : IPersonContactService
{
    private readonly IMongoCollection<Person> _personCollection;
    private readonly IMongoCollection<Entities.Contact> _contactCollection;
    private readonly IMapper _mapper;
    public PersonContactService(IMongoDatabaseSettings databaseSettings, IMapper mapper)
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
        return _mapper.Map<List<PersonWithoutDetailDto>>(persons);
    }

    public async Task<List<PersonDto>> GetAllWithDetailPersons()
    {
        var persons = await _personCollection.Find(x => true).ToListAsync();
        return _mapper.Map<List<PersonDto>>(persons);
    }

    public async Task<ContactDto> AddPersonContact(CreatePersonContactDto contactInput)
    {
        var person = await _personCollection.Find(x => x.Id == contactInput.PersonId).FirstOrDefaultAsync();
        var contact = _mapper.Map<Entities.Contact>(contactInput);
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