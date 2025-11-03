using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace HelloToAsp.Data.Configs
{
    public class UserConfig : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {
            var hasher = new PasswordHasher<IdentityUser>();

            builder.HasData(
                new User
                {
                    Id = 1,
                    FirstName = "Sana",
                    LastName = "Shirzad",
                    PhoneNumber = "09917878501",
                    UserName = "09917878501",
                    NormalizedUserName = "09917878501",
                    PasswordHash = hasher.HashPassword(null, "Test@123")
                },
                new User
                {
                    Id = 2,
                    FirstName = "AmirAli",
                    LastName = "Mahmoodi",
                    PhoneNumber = "09917878502",
                    UserName = "09917878502",
                    NormalizedUserName = "09917878502",
                    PasswordHash = hasher.HashPassword(null, "Test@123")
                },
                new User
                {
                    Id = 3,
                    FirstName = "John",
                    LastName = "Bosch",
                    PhoneNumber = "09917878503",
                    UserName = "09917878503",
                    NormalizedUserName = "09917878503",
                    PasswordHash = hasher.HashPassword(null, "Test@123")
                }
            );
        }
    }
}
