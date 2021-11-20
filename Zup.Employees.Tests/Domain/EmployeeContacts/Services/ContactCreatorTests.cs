using Zup.Employees.Domain.DTOs;
using Zup.Employees.Domain.EmployeeContacts.Entities;
using Zup.Employees.Domain.EmployeeContacts.Interfaces;
using Zup.Employees.Domain.EmployeeContacts.Services;

namespace Zup.Employees.Tests.Domain.EmployeeContacts.Services;

public class ContactCreatorTests
{
    private readonly IEmployeeContactRepository _employeeContactRepository;
    private readonly Fixture _fixture;

    private readonly ContactCreator _contactCreator;

    public ContactCreatorTests()
    {
        _employeeContactRepository = Substitute.For<IEmployeeContactRepository>();
        _fixture = FixtureHelper.CreateFixture();
        _contactCreator = new ContactCreator(_employeeContactRepository);
    }

    [Fact]
    public async Task Should_Call_Create_From_Repository_With_Created_Entity_Mapped()
    {
        var contactDto = _fixture.Create<ContactDTO>();
        var contact = _fixture.Create<Contact>();
        Contact? mappedEntity = null;

        await _employeeContactRepository.CreateAsync(Arg.Do<Contact>(x => mappedEntity = x));
        _employeeContactRepository.CreateAsync(Arg.Any<Contact>()).Returns(contact);

        var act = await _contactCreator.CreateAsync(contactDto);

        mappedEntity.Should().BeEquivalentTo(contactDto, opt => opt.Excluding(x => x.Id));

        act.Should().Be(contact);
    }
}
