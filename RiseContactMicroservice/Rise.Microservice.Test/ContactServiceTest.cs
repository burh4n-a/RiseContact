using DotNetCore.CAP;
using Microsoft.AspNetCore.Mvc;
using Moq;
using Rise.Contact.Controllers;
using Rise.Contact.DataAccess.Abstract;
using Rise.Contact.DataAccess.Concreate;
using Rise.MongoDb.Entity.Concreate;
using Rise.Shared.Dtos;
using Rise.Shared.Enums;

namespace Rise.Microservice.Test
{
    public class ContactServiceTest
    {

        private readonly Mock<IPersonContactService> _personContactServiceMock;
        private readonly Mock<ICapPublisher> _iCapPublisherMock;
        private readonly PersonContactController _personContactController;
        private readonly List<PersonDto> _persons = new List<PersonDto>();

        public ContactServiceTest()
        {
            this._personContactServiceMock = new Mock<IPersonContactService>();
            this._iCapPublisherMock = new Mock<ICapPublisher>();
            this._personContactController = new PersonContactController(_personContactServiceMock.Object, _iCapPublisherMock.Object);

            for (int i = 0; i < 50; i++)
            {
                var person = new PersonDto()
                {
                    Name = Faker.Name.First(),
                    Surname = Faker.Name.Last(),
                    Company = Faker.Company.Name(),
                    Contacts = new List<ContactDto>()
                    {
                        new ContactDto()
                        {
                            ContactData = Faker.Phone.Number(),
                            ContactType = ContactType.Phone
                        },
                        new ContactDto()
                        {
                            ContactData = Faker.Country.Name(),
                            ContactType = ContactType.Location
                        }
                    }
                };
                this._persons.Add(person);

            }
        }

        [Fact]
        public async Task PersonNotNullTest()
        {

             _personContactServiceMock.Setup(x => x.GetAllWithDetailPersons()).ReturnsAsync(this._persons); 

            var persons = await _personContactServiceMock.Object.GetAllWithDetailPersons();

            Assert.NotNull(persons);

        }

        [Fact]
        public async Task PersonCountTest()
        {

            _personContactServiceMock.Setup(x => x.GetAllWithDetailPersons()).ReturnsAsync(this._persons);

            var persons = await _personContactServiceMock.Object.GetAllWithDetailPersons();

            Assert.Equal(50, persons.Count);

        }

        [Fact]
        public async Task PersonAddTest()
        {

            _personContactServiceMock.Setup(x => x.GetAllWithDetailPersons()).ReturnsAsync(this._persons);

            var persons = await _personContactServiceMock.Object.GetAllWithDetailPersons();

           persons.Add(new PersonDto()
           {
               Id = "112121",
               Surname = "sadas",
               Name = "sad",
               Company = "12asdas",
               Contacts = new List<ContactDto>()
           });

            Assert.Equal(51, persons.Count);

        }

        [Fact]
        public async Task GetPersonsWithControllerTest()
        {

            _personContactServiceMock.Setup(x => x.GetAllWithDetailPersons()).ReturnsAsync(this._persons);


            var result = await _personContactController.GetAllPersonsWithDetail();

            Assert.IsType<OkObjectResult>(result);



        }


    }
}