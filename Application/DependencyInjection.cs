using Application.Commands.UserCommands.Register;
using Application.Interfaces.ServiceInterfaces;
using Application.Mappings;
using Application.Services.PasswordEncryption;
using Application.Validation;
using FluentValidation;
using FluentValidation.AspNetCore;
using MediatR;
using Microsoft.Extensions.DependencyInjection;

namespace Application
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddApplication(this IServiceCollection services)
        {
            var assembly = typeof(DependencyInjection).Assembly;

            services.AddMediatR(configuration => configuration.RegisterServicesFromAssembly(assembly));
            services.AddAutoMapper(assembly);
            services.AddScoped<IPasswordEncryptionService, PasswordEncryptionService>();
            services.AddTransient(typeof(IPipelineBehavior<,>), typeof(ValidationBehavior<,>));

            services.AddAutoMapper(typeof(UserMappingProfiles).Assembly);

            services.AddValidatorsFromAssemblyContaining<RegisterCommandValidator>();
            services.AddFluentValidationAutoValidation();
            return services; 
        }
    }
}
