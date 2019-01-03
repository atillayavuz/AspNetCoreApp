using AspNetCoreApp.Api.Dto;
using Task = AspNetCoreApp.Api.Domain.Task;

namespace AspNetCoreApp.Api.Application.Mapping
{
    public static class TaskMapper
    {
        public static TaskDto MapToTaskDto(this Task entity)
        {
            return new TaskDto()
            {
                Id = entity.Id,
                Title = entity.Title,
                Description = entity.Description,
                CreateDate = entity.CreateDate.ToShortDateString()
            };
        }

        public static Task MapToTask(this TaskDto model)
        {
            return new Task()
            { 
                Id = model.Id,
                Title = model.Title,
                Description = model.Description
            };
        }
    }
}
