using HelloToAsp.Data;
using Microsoft.EntityFrameworkCore;

namespace HelloToAsp.DB
{
    public class ToDoListContext : DbContext
    {
        public ToDoListContext(DbContextOptions options) : base(options)
        {
        }

        public DbSet<User> Users { get; set; }
        public DbSet<ToDoList> ToDoLists { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<User>().HasData(
                new User
                {
                    Id = 1,
                    FirstName = "Sana",
                    LastName = "Shirzad",
                    PhoneNumber = "09917878501",
                },
                new User
                {
                    Id = 2,
                    FirstName = "AmirAli",
                    LastName = "Mahmoodi",
                    PhoneNumber = "09917878502",
                },
                new User
                {
                    Id = 3,
                    FirstName = "John",
                    LastName = "Bosch",
                    PhoneNumber = "09917878503",
                }
            );

            modelBuilder.Entity<ToDoList>().HasData(
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
