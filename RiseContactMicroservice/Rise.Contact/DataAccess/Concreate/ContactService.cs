using AutoMapper;
using MongoDB.Driver;
using Rise.Contact.DataAccess.Abstract;
using Rise.Contact.Entities;
using Rise.Shared.Abstract;
using Rise.Shared.Dtos;

namespace Rise.Contact.DataAccess.Concreate;

public class ContactService : IContactService
{
    private readonly IMongoCollection<Entities.Contact> _contactCollection;
    private readonly IMapper _mapper;

    public ContactService(IMapper mapper, IMongoDatabaseSettings databaseSettings)
    {
        _mapper = mapper;
        var mongoClient = new MongoClient(databaseSettings.ConnectionString);
        var mongoDb = mongoClient.GetDatabase(databaseSettings.DatabaseName);
        _contactCollection = mongoDb.GetCollection<Entities.Contact>(databaseSettings.ContactCollectionName);
    }

    public async Task<ContactDto> AddPersonContact(CreateContactDto contactInput)
    {
        var contact = _mapper.Map<Entities.Contact>(contactInput);
        await _contactCollection.InsertOneAsync(contact);
        return _mapper.Map<ContactDto>(contact);
    }

    public async Task<bool> DeletePersonContact(string contactId)
    {
        var result = await _contactCollection.DeleteOneAsync(x => x.Id == contactId);
        return result.DeletedCount > 0;
    }
}