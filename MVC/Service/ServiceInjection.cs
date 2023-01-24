using MVC.Interface;

namespace MVC.Service;

public static class ServiceInjection
{
    public static void AdminService(this IServiceCollection services)
    {
        services.AddHttpClient<IAdmin, AdminService>();
        services.AddHttpClient<IClient, ClientService>();

        services.AddHttpClient<ICar, CarService>();
    }
}
