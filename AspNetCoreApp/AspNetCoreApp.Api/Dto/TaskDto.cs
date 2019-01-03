using System;

namespace AspNetCoreApp.Api.Dto
{
    public class TaskDto
    {
        public int Id { get; set; }

        public string Title { get; set; }

        public string Description { get; set; }

        public string CreateDate { get; set; }
    }
}
