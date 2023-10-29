using Contacts.Application.Service.Contact;
using Contacts.Application.Service.Contact.Interface;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Contacts.Application;

public static class ApplicationDependencies
{
    public static void AddApplicationService(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddScoped<IContactSvc, ContactSvc>();

    }
}