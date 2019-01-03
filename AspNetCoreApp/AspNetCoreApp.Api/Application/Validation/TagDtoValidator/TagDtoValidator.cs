using AspNetCoreApp.Api.Dto;
using FluentValidation;

namespace AspNetCoreApp.Api.Application.Validation
{
    public class TagDtoValidator : AbstractValidator<TagDto>
    {
        public TagDtoValidator()
        {
            RuleFor(task => task.Name).NotEmpty().WithMessage("Name boş geçilemez!");
        }
    }
}
