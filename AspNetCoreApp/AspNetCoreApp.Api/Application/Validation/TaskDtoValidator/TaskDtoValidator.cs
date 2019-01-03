using AspNetCoreApp.Api.Dto;
using FluentValidation;

namespace AspNetCoreApp.Api.Application.Validation
{
    public class TaskDtoValidator : AbstractValidator<TaskDto>
    {
        public TaskDtoValidator()
        {
            RuleFor(task => task.Title).NotEmpty().WithMessage("Title boş geçilemez!");
            RuleFor(task => task.Description).NotEmpty().WithMessage("Description boş geçilemez!");
        }
    }
}
