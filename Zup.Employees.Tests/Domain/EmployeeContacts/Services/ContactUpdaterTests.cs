using Zup.Employees.Domain.DTOs;
using Zup.Employees.Domain.EmployeeContacts.Entities;
using Zup.Employees.Domain.EmployeeContacts.Interfaces;
using Zup.Employees.Domain.EmployeeContacts.Services;

namespace Zup.Employees.Tests.Domain.EmployeeContacts.Services;

public class ContactUpdaterTests
{
    private readonly IEmployeeContactRepository _employeeContactRepository;
    private readonly Fixture _fixture;
    private readonly ContactUpdater _contactUpdater;

    public ContactUpdaterTests()
    {
        _employeeContactRepository = Substitute.For<IEmployeeContactRepository>();
        _fixture = FixtureHelper.CreateFixture();

        _contactUpdater = new ContactUpdater(_employeeContactRepository);
    }

    [Fact]
    public async Task Should_Update_Only_Contact_Number()
    {
        var contactDto = _fixture.Create<ContactDTO>();
        var contact = _fixture.Create<Contact>();

        _employeeContactRepository.GetAsync(contactDto.Id).Returns(contact);

        var act = await _contactUpdater.UpdateAsync(contactDto);

        act.Should().NotBeEquivalentTo(contactDto, opt => opt.Excluding(x => x.Number));
        act.Number.Should().Be(contactDto.Number);
    }

    [Fact]
    public async Task Should_Return_Null_When_Specified_Contact_Is_Not_Found()
    {
        var contactDto = _fixture.Create<ContactDTO>();

        var act = await _contactUpdater.UpdateAsync(contactDto);

        act.Should().BeNull();
    }
}
