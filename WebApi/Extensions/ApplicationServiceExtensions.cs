﻿using Microsoft.AspNetCore.Mvc;
using WebApi.Errors;
using WebApi.Halper;
using WebApi.Interface;
using WebApi.Service;

namespace WebApi.Extensions;

public static class ApplicationServiceExtensions
{
    public static IServiceCollection AddApplicationServices(this IServiceCollection services)
    {
        services.AddScoped<ICountry, CountryService>();
        services.AddScoped<IAdmin, AdminService>();
        services.AddScoped<IAeroport, AeroportService>();
        services.AddScoped<ICar, CarService>();
        services.AddScoped<IClient, ClientService>();

        //Azure
        services.AddScoped<IFileStorageService, AzureStorageService>();

        //For Errors
        services.Configure<ApiBehaviorOptions>(options =>
        {
            options.InvalidModelStateResponseFactory = actionContext =>
            {
                var errors = actionContext.ModelState
                    .Where(e => e.Value.Errors.Count > 0)
                    .SelectMany(x => x.Value.Errors)
                    .Select(x => x.ErrorMessage).ToArray();

                var errorResponse = new ApiValidationErrorResponse
                {
                    Errors = errors
                };

                return new BadRequestObjectResult(errorResponse);
            };
        });

        return services;
    }
}
