namespace Zup.Employees.Tests.Utils;

public static class FixtureHelper
{
    public static Fixture CreateFixture()
    {
        var fixture = new Fixture();
        fixture.Behaviors.Remove(new ThrowingRecursionBehavior());
        fixture.Behaviors.Add(new OmitOnRecursionBehavior());
        return fixture;
    }
}
