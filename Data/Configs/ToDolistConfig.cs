using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace HelloToAsp.Data.Configs
{
    public class ToDolistConfig : IEntityTypeConfiguration<ToDoList>
    {
        public void Configure(EntityTypeBuilder<ToDoList> builder)
        {
            builder.HasData(
               new ToDoList
               {
                   Id = 1,
                   Task = "finish C#",
                   Duration = 50,
                   UserId = 1,
               },
               new ToDoList
               {
                   Id = 2,
                   Task = "start asp.net core",
                   Duration = 150,
                   UserId = 3,
                   StartDateTime = new DateOnly(2025, 5, 21),
                   EndDateTime = new DateOnly(2025, 5, 26),
               },
               new ToDoList
               {
                   Id = 3,
                   Task = "start git",
                   Duration = 20,
                   UserId = 2,
                   Description = "test"
               }
           );
        }
    }
}
