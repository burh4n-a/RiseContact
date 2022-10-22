using AutoMapper;
using Rise.Contact.Entities;
using Rise.Shared.Dtos;

namespace Rise.Contact;

public class ContactAutoMapperProfile : Profile
{
    public ContactAutoMapperProfile()
    {
        CreateMap<Person, PersonDto>().ReverseMap();
        CreateMap<CreatePersonDto,Person>().ReverseMap();
        CreateMap<Entities.Contact, ContactDto>().ReverseMap();
        CreateMap<CreateContactDto, Entities.Contact>().ReverseMap();
        CreateMap<CreatePersonWithContactDto, Person>();
    }
}