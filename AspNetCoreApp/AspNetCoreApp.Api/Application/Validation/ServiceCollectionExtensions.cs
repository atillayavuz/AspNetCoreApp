using AspNetCoreApp.Api.Dto;
using FluentValidation;
using Microsoft.Extensions.DependencyInjection;

namespace AspNetCoreApp.Api.Application.Validation
{
    public static class ServiceCollectionExtensions
    {
        public static void AddApplicationValidators(this IServiceCollection services)
        { 
            services.AddSingleton<IValidator<TaskDto>, TaskDtoValidator>();
            services.AddSingleton<IValidator<TagDto>, TagDtoValidator>();
        }
    }
}
