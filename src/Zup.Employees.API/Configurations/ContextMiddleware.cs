using Zup.Employees.Infra;

namespace Zup.Employees.API.Configurations;

public class ContextMiddleware : IMiddleware
{
    public async Task InvokeAsync(HttpContext context, RequestDelegate next)
    {
        var dbContext = context.RequestServices.GetRequiredService<EmployeeContext>();

        await next.Invoke(context);

        await dbContext.SaveChangesAsync();
    }
}
