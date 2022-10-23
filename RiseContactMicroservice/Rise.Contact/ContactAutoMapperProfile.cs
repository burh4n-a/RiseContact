using AutoMapper;
using Rise.MongoDb.Entity.Concreate;
using Rise.Shared.Dtos;

namespace Rise.Contact;

public class ContactAutoMapperProfile : Profile
{
    public ContactAutoMapperProfile()
    {
        CreateMap<Person, PersonDto>().ReverseMap();
        CreateMap<CreatePersonDto,Person>().ReverseMap();
        CreateMap<MongoDb.Entity.Concreate.Contact, ContactDto>().ReverseMap();
        CreateMap<CreateContactDto, MongoDb.Entity.Concreate.Contact>().ReverseMap();
        CreateMap<CreatePersonWithContactDto, Person>().ReverseMap();
        CreateMap<Person, PersonWithoutDetailDto>().ReverseMap();
        CreateMap<CreatePersonContactDto, MongoDb.Entity.Concreate.Contact>().ReverseMap();
    }
}