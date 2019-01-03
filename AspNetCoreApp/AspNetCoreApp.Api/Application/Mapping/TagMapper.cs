using AspNetCoreApp.Api.Domain;
using AspNetCoreApp.Api.Dto;

namespace AspNetCoreApp.Api.Application.Mapping
{
    public static class TagMapper
    {
        public static TagDto MapToTagDto(this Tag entity)
        {
            return new TagDto()
            {
                Id = entity.Id,
                Name = entity.Name, 
                CreateDate = entity.CreateDate.ToShortDateString()
            };
        }

        public static Tag MapToTask(this TagDto model)
        {
            return new Tag()
            {
                Id = model.Id,
                Name = model.Name
            };
        }
    }
}
