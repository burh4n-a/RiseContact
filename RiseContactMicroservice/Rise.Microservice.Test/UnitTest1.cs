using Moq;
using Rise.Contact.Controllers;
using Rise.Contact.DataAccess.Abstract;
using Rise.Contact.DataAccess.Concreate;
using Rise.Shared.Dtos;

namespace Rise.Microservice.Test
{
    public class UnitTest1
    {

        private readonly Mock<IPersonContactService> personContactServiceMock;

        public UnitTest1()
        {
            this.personContactServiceMock = new Mock<IPersonContactService>();
        }

        [Fact]
        public async Task PersonNotNullTest()
        {

            var persons = await personContactServiceMock.Object.GetAllWithDetailPersons();

            Assert.NotNull(persons);

        }

       
    }
}